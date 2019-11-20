using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durian : Collectable {

	protected override void GetHit(Collider2D collider)
	{
		GameController.gc.player.GetComponent<Player>().Regenerate();
		Instantiate(GameController.gc.durianPrefab, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
