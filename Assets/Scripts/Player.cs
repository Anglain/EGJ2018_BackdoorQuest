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

	private bool _moving;
	private bool moving
	{
		get {return _moving;}
		set
		{
			_moving = value;
			animController.SetBool("moving", _moving);
		}
	}
	private AnimatorController animController;

	void Awake ()
	{
		animController = GetComponent<AnimatorController>();

		if (animController == null) Debug.LogError("No AnimatorController component found attached to this gameObject! [PLAYER.CS]");
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
		} else if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))) {
			direction = (Vector3.up + Vector3.left) / Mathf.Sqrt (2);
		} else if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))) {
			direction = (Vector3.down + Vector3.right) / Mathf.Sqrt (2);
		} else if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))) {
			direction = (Vector3.down + Vector3.left) / Mathf.Sqrt (2);
		} else if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			direction = Vector3.up;
		} else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			direction = Vector3.down;
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			direction = Vector3.left;
		} else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction = Vector3.right;
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
	}
}
