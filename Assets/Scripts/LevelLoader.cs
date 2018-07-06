using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	void Awake ()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void LoadScene (int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
}
