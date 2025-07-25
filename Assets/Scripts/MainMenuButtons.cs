using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public string Scene;

	public void PlayGame()
	{
		SceneManager.LoadScene(Scene);
	}


	public void QuitGame ()
	{
		Debug.Log("Quit!");	
		Application.Quit();
	}
}
