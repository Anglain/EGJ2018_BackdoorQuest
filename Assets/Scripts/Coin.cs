using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	void OnTriggerStay2D(Collider2D collider)
	{
		Destroy(this.gameObject);
	}
}
