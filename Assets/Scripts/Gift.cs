using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : Collectable {

	protected override void GetHit(Collider2D collider)
	{
		Debug.Log("Gift collected.");
		DestroyObject();
	}
}
