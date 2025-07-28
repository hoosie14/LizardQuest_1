using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid for ID")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
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
            battleScript.EnemyId = id; 
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
