using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour {

	public void Restart()
	{
		LevelLoader ll = GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>();
		ll.LoadScene(0);
	}
}
