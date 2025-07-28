using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    //Save stuff

    [SerializeField] private string id;
    [ContextMenu("Generate Guid for ID")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public bool defeated = false;

    [Header("REMOVE")]
    public bool timeForBoss = false;

    public Sprite sprite; 

    public BattleScript1 battleScript;
    [HideInInspector]
    public PlayerMovement cc;

    public GameObject Fader;

    public GameObject BattleWindow;

    //EnemyStats
    public float enemyHP = 20;
    public float maxEnemyHP = 20;
    public float enemyDamage = 5;

    public string OnAppearance;
    public string OnAttack;
    public string OnDeath;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip OverworldMusic;
    public AudioClip BattleMusic; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeForBoss)
            BattleQued();
    }
    public void LoadData(GameData data)
    {
        data.EnemiesDefeated.TryGetValue(id, out defeated);
        if (defeated)
        {
            this.transform.parent.gameObject.SetActive(false);
        }

    }
    public void SaveData(ref GameData data)
    {
        if (data.EnemiesDefeated.ContainsKey(id))
        {
            data.EnemiesDefeated.Remove(id);
        }
        data.EnemiesDefeated.Add(id, defeated);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cc = collision.GetComponent<PlayerMovement>();

        if (cc)
        {
            cc.enablePlayerControls = false;

            battleScript.BattleMusic = BattleMusic;
            StartCoroutine(BattleQued());

            battleScript.EnemyHP = enemyHP;
            battleScript.MaxEnemyHP = maxEnemyHP;
            battleScript.EnemyDamage = enemyDamage;

            battleScript.OnAppearance = OnAppearance;
            battleScript.OnAttack = OnAttack;
            battleScript.OnDeath = OnDeath;

            battleScript.EnemySprite.sprite = sprite;
            collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

    }

    IEnumerator BattleQued()
    {
        Fader.active = true;
        //audioSource.clip = BattleMusic; 


        yield return new WaitForSeconds(1);

        BattleWindow.active = true;

        yield return new WaitForSeconds(1);

        Fader.active = false;

        this.gameObject.active = false; 
    }


}
