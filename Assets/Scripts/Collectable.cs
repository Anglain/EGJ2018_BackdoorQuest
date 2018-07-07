using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	protected virtual void GetHit(Collider2D collider){}

	void OnTriggerStay2D(Collider2D collider)
	{
		GetHit(collider);
	}

	protected void DestroyObject()
	{
		Destroy(this.gameObject);
	}
}
