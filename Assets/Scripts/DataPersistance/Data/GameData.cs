using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Dictionary<string, int> Items;
    public Dictionary<string, bool> EnemiesDefeated;
    public int LizardHealth; 
    public Vector3 playerPosition;
    public GameData()
    {
        this.Items = new Dictionary<string, int>();
        this.LizardHealth = 20;
        this.playerPosition = Vector3.zero;
        this.EnemiesDefeated = new Dictionary<string, bool>();
    }
}
