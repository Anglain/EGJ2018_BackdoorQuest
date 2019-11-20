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

	public void LoadNextScene ()
	{
		if (SceneManager.GetActiveScene().buildIndex  < 2)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			SceneManager.LoadScene(0);
		}
		GameController.gc.SetLevel();
	}
}
