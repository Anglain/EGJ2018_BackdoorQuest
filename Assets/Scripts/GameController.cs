using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private bool gotCoin = false;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		gotCoin = false;
	}

	public void NoCoin() { gotCoin = false; }
	public void PickedUpCoin() { gotCoin = true; }
	public bool HasCoin() { return gotCoin; }
}
