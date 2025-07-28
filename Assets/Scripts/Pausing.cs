using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{
    public bool IsPaused = false;

    public GameObject PauseMenu;

    public PlayerMovement player; 
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.active = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused == false)
            {
                //IsPaused = true;
                Pause();
            }
            else
            {
                //IsPaused = false;
                UnPause();
            }
        }

        
    }

    public void UnPause()
    {
        IsPaused = false;
        PauseMenu.active = false;
        player.enablePlayerControls = true;
    }

    public void Pause()
    {
        IsPaused = true;
        PauseMenu.active = true; ;
        player.enablePlayerControls = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void Reload()
    {
        SceneManager.LoadScene("Joshua'sOpenWorldScene");
    }
}
