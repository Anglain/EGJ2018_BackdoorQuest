using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {

	protected override void GetHit(Collider2D collider)
	{
		Debug.Log("Coin collected.");
		DestroyObject();
	}
}
