using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGates : MonoBehaviour {

	void OnTriggerEnter2D () {
		LevelLoader ll = GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>();

		if (ll == null) Debug.LogError("Could not find levelLoader in the scene! [EXIT_GATES.CS]");

		ll.LoadNextScene();
	}
}
