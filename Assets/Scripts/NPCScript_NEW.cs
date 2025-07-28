using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NPCTest_Updated : MonoBehaviour
{

    [Header("Remove")]
    public GameObject BattleWindow;
    public BattleScript1 battleScript;
    public Sprite enemySprite;
    public bool isEnemy;

    [Header("FinalBoss")]
    public bool isBoss = false;

    //Dialouge Variables
    public bool _IsInteractable = true;
    public GameObject _dialougeBox;
    [HideInInspector] public float _DialougeIndex = 0;
    [HideInInspector] public GameObject _dialougeBox_Instantiated;
    public GameObject _dialougeBox_Parent;
    bool IsInstantiated = false;

    //Text Variables
    [HideInInspector] public TMP_Text _Text;

    //Player Variables
    public GameObject player;
    [HideInInspector] public PlayerMovement playerMovement;

    [Header("Dialouge")]
    [HideInInspector]
    public List<string> _CurrentDialougeList;
    public List<string> _DialougeList;
    public List<string> _SecondaryDialougeList;
    public int _Current_Index;

    [Header("IfEnemy")]
    //EnemyStats
    public float enemyHP = 20;
    public float maxEnemyHP = 20;
    public float enemyDamage = 5;

    public string OnAppearance;
    public string OnAttack;
    public string OnDeath;

    public AudioSource audioSource;
    public AudioClip OverworldMusic;
    public AudioClip BattleMusic;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enablePlayerControls = true;
        _IsInteractable = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _IsInteractable = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            _IsInteractable = false;
        }
    }
    void Update()
    {
        if (isBoss)
        {
            if (playerMovement.Level >= 6)
            {
                isEnemy = true;
                _CurrentDialougeList = _SecondaryDialougeList;
            }
            else
            {
                isEnemy = false;
                _CurrentDialougeList = _DialougeList;
            }
        }
        else
            _CurrentDialougeList = _DialougeList;

        if (!IsInstantiated & _IsInteractable & Input.GetButtonDown("Interact"))
        {
            //Instantiate
            _dialougeBox_Instantiated = Instantiate(
                _dialougeBox,
                new Vector3(840, 400, 0),
                Quaternion.identity,
                _dialougeBox_Parent.transform
                ) as GameObject;

            //Disable Movement and set needed variables
            playerMovement.enablePlayerControls = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            IsInstantiated = true;
            _Text = _dialougeBox_Instantiated.GetComponentInChildren<TMP_Text>();
            
                _Text.text = _CurrentDialougeList[0];
                _Current_Index = 0;
            
            
        } else if (IsInstantiated & Input.GetButtonDown("Interact"))
        {
            _Current_Index += 1;
            if (_Current_Index >= _CurrentDialougeList.Count)
            {
                Destroy(_dialougeBox_Instantiated);
                _Current_Index = 0;
                playerMovement.enablePlayerControls = true;
                IsInstantiated = false;

                if (isEnemy)
                {

                    battleScript.EnemyHP = enemyHP;
                    battleScript.MaxEnemyHP = maxEnemyHP;
                    battleScript.EnemyDamage = enemyDamage;

                    battleScript.OnAppearance = OnAppearance;
                    battleScript.OnAttack = OnAttack;
                    battleScript.OnDeath = OnDeath;

                    battleScript.BattleMusic = BattleMusic;

                    battleScript.EnemySprite.sprite = enemySprite;
                    //collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                    BattleWindow.active = true;

                    this.gameObject.active = false; 
                }

            }
            else
            {
                _Text.text = _CurrentDialougeList[_Current_Index];   
            }
        }

        
    }
}
