using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
	public float moveSpeed = 10f;

	private Vector3 direction;
	private bool wasMovingDown;
	private bool wasMovingRight;
	private bool moving;

	private Animator animController;

	void Awake ()
	{
		animController = GetComponent<Animator>();

		if (animController == null) Debug.LogError("No Animator component found attached to this gameObject! [PLAYER.CS]");
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

		if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))) {
			direction = (Vector3.up + Vector3.right) / Mathf.Sqrt (2);
			wasMovingDown = false;
			wasMovingRight = true;
		} else if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))) {
			direction = (Vector3.up + Vector3.left) / Mathf.Sqrt (2);
			wasMovingDown = false;
			wasMovingRight = false;
		} else if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))) {
			direction = (Vector3.down + Vector3.right) / Mathf.Sqrt (2);
			wasMovingDown = true;
			wasMovingRight = true;
		} else if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))) {
			direction = (Vector3.down + Vector3.left) / Mathf.Sqrt (2);
			wasMovingDown = true;
			wasMovingRight = false;
		} else if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			direction = Vector3.up;
			wasMovingDown = false;
		} else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			direction = Vector3.down;
			wasMovingDown = true;
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			direction = Vector3.left;
			wasMovingRight = false;
		} else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction = Vector3.right;
			wasMovingRight = true;
		} else {
			direction = Vector3.zero;
		}

		direction = direction.normalized;
	}

	void FixedUpdate ()
	{
		if (direction != Vector3.zero)
		{
			transform.Translate (moveSpeed * direction * Time.deltaTime);
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
