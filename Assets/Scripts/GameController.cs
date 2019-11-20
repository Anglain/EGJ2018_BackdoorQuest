using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public static GameController gc;
	public GameObject coinPrefab;
	public GameObject balloon1Prefab;
	public GameObject durianPrefab;
	public GameObject player;

	public GameObject giftSpawnParticlesPrefab;
	public GameObject durianParticles;

	public GameObject playerUICanvas;
	public GameObject restartCanvas;

	private Vector3 spawnPoint;
	private bool gotCoin = false;

	void Awake()
	{
		if (gc == null) gc = this;
		if (gc != this) Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);

		if (coinPrefab == null) Debug.LogError("No coinPrefab gameObject found attached to this gameObject! [GAME_CONTROLLER.CS]");
	}
	
	public void NoCoin() { gotCoin = false; }
	public void PickedUpCoin() { gotCoin = true; }
	public bool HasCoin() { return gotCoin; }
	public void RespawnScreen()
	{
		Instantiate(GameController.gc.restartCanvas, transform.position, Quaternion.identity);
	}

	void SetSpawnPoint() { spawnPoint = player.transform.position; }
	// public void Respawn()
	// {
	// 	player.transform.position = spawnPoint;
	// }

	public void SetLevel()
	{
		NoCoin();
		if (player == null) player = GameObject.FindWithTag("Player");
		if (player == null) Debug.LogError("No Player found in the scene! [GAME_CONTROLLER.CS]");
	}
}
