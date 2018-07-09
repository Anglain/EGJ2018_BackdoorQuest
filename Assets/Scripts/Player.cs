using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speedForce = 10f;

	[Space(5)]
	[Header("Hit colliders")]
	public Transform upperRightHitPos;
	public Transform upperLeftHitPos;
	public Transform downRightHitPos;
	public Transform downLeftHitPos;
	public float attackRange;
	public LayerMask whatIsEnemy;
	public LayerMask whatIsGift;

	private Vector3 direction;
	private bool wasMovingDown;
	private bool wasMovingRight;
	private bool moving;

	private Animator animController;
	private Rigidbody2D rb2D;

	void Awake ()
	{
		animController = GetComponent<Animator>();
		if (animController == null) Debug.LogError("No Animator component found attached to this gameObject! [PLAYER.CS]");

		rb2D = GetComponent<Rigidbody2D>();
		if (rb2D == null) Debug.LogError("No Rigidbody2D component found attached to this gameObject! [PLAYER.CS]");

		if (upperRightHitPos == null) Debug.LogError("No upperRightHitPos transform found assigned to this gameObject! [PLAYER.CS]");
		if (upperLeftHitPos == null) Debug.LogError("No upperLeftHitPos transform found assigned to this gameObject! [PLAYER.CS]");
		if (downRightHitPos == null) Debug.LogError("No downRightHitPos transform found assigned to this gameObject! [PLAYER.CS]");
		if (downLeftHitPos == null) Debug.LogError("No downLeftHitPos transform found assigned to this gameObject! [PLAYER.CS]");
	}

	void Start()
	{
		wasMovingDown = true;
		wasMovingRight = true;
		moving = false;
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

			Vector3 attackPos;
			if (wasMovingDown && wasMovingRight)
				attackPos = downRightHitPos.position;
			else if (wasMovingDown && !wasMovingRight)
				attackPos = downLeftHitPos.position;
			else if (!wasMovingDown && wasMovingRight)
				attackPos = upperRightHitPos.position;
			else
				attackPos = upperLeftHitPos.position;

			Debug.Log(attackPos);
			Debug.DrawLine(transform.position, attackPos, Color.green);
			Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos, attackRange, whatIsEnemy);
			Collider2D[] gifts = Physics2D.OverlapCircleAll(attackPos, attackRange, whatIsGift);

			foreach(Collider2D gift in gifts)
			{
				gift.GetComponent<Gift>().Pop();
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(upperLeftHitPos.position, attackRange);
		Gizmos.DrawWireSphere(upperRightHitPos.position, attackRange);
		Gizmos.DrawWireSphere(downLeftHitPos.position, attackRange);
		Gizmos.DrawWireSphere(downRightHitPos.position, attackRange);
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
