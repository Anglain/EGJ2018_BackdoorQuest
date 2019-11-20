using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GiftResult
{
	Nothing, Coin, Balloon1, Balloon2, Balloon3
}

public class Gift : MonoBehaviour {

	public GiftResult spawn;

	public void Pop()
	{
		Debug.Log("Gift pop!");
		Spawn();
		Destroy(this.gameObject);
	}

	void Spawn()
	{
		Instantiate(GameController.gc.giftSpawnParticlesPrefab, transform.position, Quaternion.identity);
		if (spawn == GiftResult.Coin)
		{
			Instantiate(GameController.gc.coinPrefab, transform.position, Quaternion.identity);
		}
		else if (spawn == GiftResult.Balloon1)
		{
			Instantiate(GameController.gc.balloon1Prefab, transform.position, Quaternion.identity);
		}
	}
}
