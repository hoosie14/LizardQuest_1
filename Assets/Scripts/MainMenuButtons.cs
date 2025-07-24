using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public string Scene;

	public void GoToOptions ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

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
