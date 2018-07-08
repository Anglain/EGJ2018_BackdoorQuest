using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonEnemy : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;

	public float speed = 6f;

	private bool movingToA = false;

	void Awake()
	{
		if (pointA == null) Debug.LogError("No pointA Transform assigned to this gameObject! [BALLOON_ENEMY.CS]");
		if (pointB == null) Debug.LogError("No pointB Transform assigned to this gameObject! [BALLOON_ENEMY.CS]");
	}

	void Start()
	{
		movingToA = false;
	}

	void FixedUpdate()
	{

	}
}
