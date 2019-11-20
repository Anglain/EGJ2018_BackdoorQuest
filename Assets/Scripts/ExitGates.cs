using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGates : MonoBehaviour {

	private LevelLoader ll;

	void Start ()
	{
		ll = GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>();
		if (ll == null) Debug.LogError("Could not find LevelLoader in the scene! [EXIT_GATES.CS]");
	}

	void OnCollisionEnter2D () {

		if (GameController.gc.HasCoin())
		{
			NextScene();
		}
	}

	public void NextScene()
	{
		ll.LoadNextScene();
	}
}
