using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager instance { get; private set; }
    private GameData gameData;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance");

        }
        instance = this;

    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("No Data Found, Initializing new game");
            NewGame();
        }
    }

    public void SaveGame()
    {
        //Pass to other scripts so they can update it
        //Save to file with data handler
    }
}
