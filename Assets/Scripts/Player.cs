using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
	public float speedForce = 10f;

	[Space(5)]
	[Header("Hit colliders")]
	public Collider2D upperRightHitCol;
	public Collider2D upperLeftHitCol;
	public Collider2D downRightHitCol;
	public Collider2D downLeftHitCol;
	public LayerMask whatToHit;

	private Vector3 direction;
	private bool wasMovingDown;
	private bool wasMovingRight;
	private bool moving;

	private Animator animController;
	private Rigidbody2D rb2D;

	private ContactFilter2D filter2D;

	void Awake ()
	{
		animController = GetComponent<Animator>();
		if (animController == null) Debug.LogError("No Animator component found attached to this gameObject! [PLAYER.CS]");

		rb2D = GetComponent<Rigidbody2D>();
		if (rb2D == null) Debug.LogError("No Rigidbody2D component found attached to this gameObject! [PLAYER.CS]");

		upperRightHitCol.enabled = false;
		upperLeftHitCol.enabled = false;
		downRightHitCol.enabled = false;
		downLeftHitCol.enabled = false;
	}

	void Start()
	{
		wasMovingDown = true;
		wasMovingRight = true;
		moving = false;

		filter2D.SetLayerMask(whatToHit);
	}

	void Update ()
	{
		direction = Vector3.zero;

		if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)))
		{
			direction = (Vector3.up + Vector3.right) / Mathf.Sqrt (2);
			wasMovingDown = false;
			wasMovingRight = true;
		}
		else if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)))
		{
			direction = (Vector3.up + Vector3.left) / Mathf.Sqrt (2);
			wasMovingDown = false;
			wasMovingRight = false;
		}
		else if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)))
		{
			direction = (Vector3.down + Vector3.right) / Mathf.Sqrt (2);
			wasMovingDown = true;
			wasMovingRight = true;
		}
		else if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)))
		{
			direction = (Vector3.down + Vector3.left) / Mathf.Sqrt (2);
			wasMovingDown = true;
			wasMovingRight = false;
		}
		else if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow))
		{
			direction = Vector3.up;
			wasMovingDown = false;
		}
		else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
		{
			direction = Vector3.down;
			wasMovingDown = true;
		}
		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))
		{
			direction = Vector3.left;
			wasMovingRight = false;
		}
		else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
		{
			direction = Vector3.right;
			wasMovingRight = true;
		}
		else
		{
			direction = Vector3.zero;
		}

		direction = direction.normalized;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			animController.SetBool("movingDown", wasMovingDown);
			animController.SetBool("movingRight", wasMovingRight);
			animController.SetTrigger("attack");

			RaycastHit2D[] hitObjects = new RaycastHit2D[40];
			if (wasMovingDown && wasMovingRight)
			{
				downRightHitCol.enabled = true;
				downRightHitCol.Cast(Vector2.zero, filter2D, hitObjects);
				downRightHitCol.enabled = false;
			}
			else if (wasMovingDown && !wasMovingRight)
			{
				downLeftHitCol.enabled = true;
				downLeftHitCol.Cast(Vector2.zero, filter2D, hitObjects);
				downLeftHitCol.enabled = false;
			}
			else if (!wasMovingDown && wasMovingRight)
			{
				upperRightHitCol.enabled = true;
				upperRightHitCol.Cast(Vector2.zero, filter2D, hitObjects);
				upperRightHitCol.enabled = false;
			}
			else
			{
				upperLeftHitCol.enabled = true;
				upperLeftHitCol.Cast(Vector2.zero, filter2D, hitObjects);
				upperRightHitCol.enabled = false;
			}

			if (hitObjects.Length != 0)
			{
				foreach(RaycastHit2D hitObj in hitObjects)
				{
					if (hitObj.transform != null && hitObj.transform.gameObject.tag == "Gift")
					{
						Gift gift = hitObj.transform.gameObject.GetComponent<Gift>();

						if (gift != null)
						{
							gift.Pop();
						}
					}
				}
			}
		}
	}

	void FixedUpdate ()
	{
		if (direction != Vector3.zero)
		{
			rb2D.velocity = direction * speedForce;
			moving = true;
		}
		else
		{
			moving = false;
		}

		animController.SetBool("movingDown", wasMovingDown);
		animController.SetBool("movingRight", wasMovingRight);
		animController.SetBool("moving", moving);
	}
}
