using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[Space(5)]
	[Header("Hit colliders")]
	public Transform upperRightHitPos;
	public Transform upperLeftHitPos;
	public Transform downRightHitPos;
	public Transform downLeftHitPos;
	public float attackRange;
	public LayerMask whatIsEnemy;
	public LayerMask whatIsGift;

	[Space(5)]
	[Header("Player stats")]
	public float attackDamage = 5f;
	public float maxHealthPoints = 20f;
	public float pushbackAmount = 200f;
	public float speedForce = 10f;
	public float maxGloryPoints = 10f;

	[Space(5)]
	[Header("UI elements")]
	public Image hpImage;
	public Image gloryImage;
	public Text gloryText;

	private float _gloryPoints;
	private float gloryPoints
	{
		get { return _gloryPoints; }
		set
		{
			_gloryPoints = value;
			gloryImage.fillAmount = 1f * (maxGloryPoints / _gloryPoints);
			_gloryPoints = Mathf.Clamp(_gloryPoints, 0, maxGloryPoints);
			if (gloryPoints == maxGloryPoints) gloryText.text = "Glory level achieved!";
		}
	}
	private float _healthPoints;
	private float healthPoints
	{
		get { return _healthPoints; }
		set
		{
			_healthPoints = value;
			hpImage.fillAmount = maxHealthPoints / _healthPoints;
			_healthPoints = Mathf.Clamp(_healthPoints, 0, maxHealthPoints);
			if (_healthPoints == 0) Die();
		}
	}

	private Vector3 direction;
	private bool wasMovingDown;
	private bool wasMovingRight;
	private bool moving;
	private bool dead = false;

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
		dead = false;
		Regenerate();
		GetGlorious(-1000f);
	}

	public void GetGlorious(float gloryBonus)
	{
		gloryPoints += gloryBonus;
	}

	public void GetHit(float damage)
	{
		healthPoints -= damage;
	}

	public void Regenerate()
	{
		healthPoints = maxHealthPoints;
	}

	void Die()
	{
		animController.SetTrigger("death");
		GameController.gc.RespawnScreen();
		dead = true;
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

		if (!dead)
			direction = direction.normalized;
		else
			direction = Vector3.zero;

		if (Input.GetKeyDown(KeyCode.Space) && !dead)
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

			Debug.Log(enemies.Length + " " + gifts.Length);
			foreach(Collider2D gift in gifts)
			{
				Debug.Log("GIFT!!!!");
				gift.GetComponent<Gift>().Pop();
			}

			foreach(Collider2D enemy in enemies)
			{
				Debug.Log("ENEMY!!!!");
				enemy.GetComponent<Enemy>().GetHit(attackDamage);
				Vector3 forceVector = Vector3.Normalize(transform.position - enemy.transform.position);
				enemy.GetComponent<Rigidbody2D>().AddForce(forceVector* (-1f) * pushbackAmount);
			}
		}

		animController.SetBool("movingDown", wasMovingDown);
		animController.SetBool("movingRight", wasMovingRight);
		animController.SetBool("moving", moving);
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
	}
}
