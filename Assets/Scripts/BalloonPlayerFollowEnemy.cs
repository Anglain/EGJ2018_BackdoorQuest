using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPlayerFollowEnemy : Enemy {

	public float maxHealthPoints = 15f;
	public float pushbackAmount = 200f;
	public float attackDamage = 5f;
	public float speed = 10f;
	public LayerMask whatToHit;

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
	private Transform target = null;
	private Animator animController;

	void Awake()
	{
		animController = GetComponent<Animator>();
		if (animController == null) Debug.LogError("No Animator component found attached to this gameObject! [BALLOON_PLAYER_FOLLOW_ENEMY.CS]");
	}

	void Start()
	{
		healthPoints = maxHealthPoints;
	}

	void Update()
	{
		if (GameController.gc.player == null) GameController.gc.player = GameObject.FindWithTag("Player");
		RaycastHit2D hit = Physics2D.Raycast(transform.position, (transform.position - GameController.gc.player.transform.position) * 100f, 100f, whatToHit);
		if (hit)
		{
			if (hit.collider.tag == "Player")
			{
				target = hit.transform;
			}
			else
			{
				target = null;
			}
		}
		else
		{
			target = null;
		}

		if (target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * 0.01f);
			Debug.DrawLine(transform.position, target.position, Color.red);
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
			collision.collider.GetComponent<Player>().GetHit(attackDamage);
		}
	}

	void Die()
	{
		GameController.gc.player.GetComponent<Player>().GetGlorious(100);
		animController.SetTrigger("death");
		Destroy(this.gameObject, 0.44f);
	}
}
