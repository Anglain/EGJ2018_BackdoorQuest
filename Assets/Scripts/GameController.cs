using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController gc;
	public GameObject coinPrefab;
	public GameObject balloon1Prefab;
	public GameObject durianPrefab;

	public GameObject giftSpawnParticlesPrefab;

	private bool gotCoin = false;

	void Awake()
	{
		if (gc == null) gc = this;
		if (gc != this) Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		NoCoin();
	}

	public void NoCoin() { gotCoin = false; }
	public void PickedUpCoin() { gotCoin = true; }
	public bool HasCoin() { return gotCoin; }
}
