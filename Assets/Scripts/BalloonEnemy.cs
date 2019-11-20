using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonEnemy : Enemy {

	public Transform pointA;
	public Transform pointB;
	public float speed = 3f;
	public float maxHealthPoints = 15f;
	public float pushbackAmount = 200f;

	private float _healthPoints;
	private float healthPoints
	{
		get { return _healthPoints; }
		set
		{
			_healthPoints = value;
			_healthPoints = Mathf.Clamp(_healthPoints, 0, maxHealthPoints);
			if (_healthPoints == 0) Die();
		}
	}
	private bool movingToA;
	private Vector3 moveVector;
	private Vector3 currentMoveVector;
	private Animator animController;

	void Awake()
	{
		animController = GetComponent<Animator>();
		if (animController == null) Debug.LogError("No Animator component found assigned to this gameObject! [BALLOON_ENEMY.CS]");
		if (pointA == null) Debug.LogError("No pointA Transform assigned to this gameObject! [BALLOON_ENEMY.CS]");
		if (pointB == null) Debug.LogError("No pointB Transform assigned to this gameObject! [BALLOON_ENEMY.CS]");

		moveVector = (pointB.position - pointA.position) * speed * 0.004f;
	}

	void Start()
	{
		movingToA = false;
		healthPoints = maxHealthPoints;
	}

	void FixedUpdate()
	{
		Vector3 target = (movingToA) ? pointA.position : pointB.position;
		transform.position = Vector3.MoveTowards(transform.position, target, speed * 0.004f);

		if (Vector3.Distance(transform.position, target) < 0.02f)
		{
			movingToA = !movingToA;
			moveVector *= -1;
		}
	}

	public override void GetHit(float damage)
	{
		healthPoints -= damage;
		Debug.Log(healthPoints + "HP (-" + damage + ")");
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			Vector3 forceVector = Vector3.Normalize(transform.position - collision.collider.transform.position);
			collision.collider.GetComponent<Rigidbody2D>().AddForce(forceVector * pushbackAmount);
		}
	}

	void Die()
	{
		animController.SetTrigger("death");
		speed = 0;
		Destroy(this.gameObject, 0.44f);
	}
}
