using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public float speed = 10f;

	private Transform target;

	void Awake ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void LateUpdate ()
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, target.position + new Vector3(0,0,-10), speed * Time.deltaTime);
	}
}
