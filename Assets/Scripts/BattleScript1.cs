using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class BattleScript1 : MonoBehaviour
{
    //This Gameobject.
    public GameObject BattleWindow;
    public GameObject VictoryScreen;
    public TMP_Text ScoreText;
    public GameObject GameOverScreen;
    public Image EnemySprite;
    public GameObject Fader;
    public PlayerMovement player; 


    [Header("Buttons")]
    public GameObject MainButtons;
    public GameObject BagButtons;
    public GameObject AttackButtons;

    [Header("Animations")]
    public Animator enemyAnimator;
    //Animation Parameters. 
    public bool pngDamage = false;
    public bool pngAttack = false;

    [Header("Score Keeping")]
    [HideInInspector]
    public float Score;
    [HideInInspector]
    public float Turns = 1;

    //Console Lines.
    [HideInInspector]
    public string OnAppearance;
    [HideInInspector]
    public string OnAttack;
    [HideInInspector]
    public string OnDeath;

    [Header("Stats")]
    public float MaxPlayerHP;
    [HideInInspector]
    public float PlayerHP = 20;
    public float PlayerDamage = 8;

    //Enemy Stats: these will need to differ from enemy to enemy.
    public float EnemyHP = 20;
    public float MaxEnemyHP = 20;
    public float EnemyDamage = 5;

    [Header("Items")]
    public float _cookies = 2;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip OverworldMusic;
    public AudioClip BattleMusic;

    [Header("Misc")]
    public TMP_Text HP;
    public TMP_Text ConsoleText;
    public Slider enemyHealthbar;
    public GameObject sliderFill;
    public TMP_Text CookieText; 

    [HideInInspector]
    public string Message;

    [HideInInspector]
    public bool _enemyTurn = false;

    [HideInInspector]
    public bool Win = false;



    // Start is called before the first frame update
    void Start()
    {
        BagButtons.active = false;
        AttackButtons.active = false;
        MainButtons.active = true;

        VictoryScreen.active = false;
        GameOverScreen.active = false;

        Turns = 1;

        _enemyTurn = false;

        PlayerHP = MaxPlayerHP;

        Message = (OnAppearance);
    }

    private void OnEnable()
    {
        

        Win = false;
        sliderFill.active = true;


        EnemyHP = MaxEnemyHP;

        enemyHealthbar.value = 1f;

        audioSource.clip = BattleMusic;
        audioSource.Play();

        BagButtons.active = false;
        AttackButtons.active = false;
        MainButtons.active = true;

        VictoryScreen.active = false;
        GameOverScreen.active = false;

        Turns = 1;

        _enemyTurn = false;

        Message = (OnAppearance);
    }
    private void Update()
    {

        enemyAnimator.SetBool("Damaged", pngDamage);
        enemyAnimator.SetBool("Attack", pngAttack);
        enemyAnimator.SetBool("Win", Win);

        ConsoleText.SetText(Message);
        HP.SetText(PlayerHP.ToString());

        enemyHealthbar.value = (EnemyHP / MaxEnemyHP);
        //change color

        CookieText.SetText(_cookies.ToString());

        //Winning&Losing
        if (PlayerHP <= 0)
        {
            Win = false;
            StartCoroutine(Lost());
        }
        else if (EnemyHP <= 0)
        {
            if (Win == false)
            {
                StartCoroutine(Won());
                Win = true;
            }
        }

        Score = (2000 - (Turns * 250)) + ((PlayerHP / MaxPlayerHP) * 2000) + 23;
        ScoreText.SetText(Score.ToString());
    }

    public void TongueAttack()
    {
        Message = ("Your Lizard whips his toungue!");
        EnemyHP -= PlayerDamage;

        if (EnemyHP > 0)
        {
            pngDamage = true;
        }
        else
        {
            //Win = true;
        }
        
        

        _enemyTurn = true;

        StartCoroutine(ActionPause());
    }

    public void Cookie()
    {
        if (_cookies > 0)
        {
            PlayerHP += 10;

            if (PlayerHP > MaxPlayerHP)
            {
                PlayerHP = MaxPlayerHP;
            }

            Message = ("The Chocolate-Chips give you a sugar rush!");

            _cookies -= 1;

            StartCoroutine(ActionPause());
        }
        else if (_cookies <= 0)
        {
            Message = ("None left, cuh. Yo big self ate all the damn cookies. Shiiii");

            StartCoroutine(ActionPause());
        }
        
    }

    public void Retreat()
    {
        StartCoroutine(CloseWindow());
    }

    public void EnemyTurn()
    {
        PlayerHP -= EnemyDamage;
        Message = (OnAttack);

        pngAttack = true;

        _enemyTurn = false;

        Turns += 1;

        StartCoroutine(ActionPause());
    }

    IEnumerator ActionPause()
    {
        BagButtons.active = false;
        AttackButtons.active = false;
        MainButtons.active = false;

        yield return new WaitForSeconds(3);

        pngDamage = false;
        pngAttack = false;

        if (_enemyTurn)
        {
            if (Win == false)
            EnemyTurn();
        }
        else
        {
            //
            Message = ("The .png bounces intimidatingly...");
            MainButtons.active = true;
        }


    }

    public void Close()
    {
        StartCoroutine(CloseWindow());
    }


    IEnumerator Won()
    {
        BagButtons.active = false;
        AttackButtons.active = false;
        MainButtons.active = false;

        sliderFill.active = false;

        Message = (OnDeath);

        yield return new WaitForSeconds(3);

        VictoryScreen.active = true;

        audioSource.clip = OverworldMusic;

        player.Score += Score; 
        
    }

    IEnumerator Lost()
    {
        BagButtons.active = false;
        AttackButtons.active = false;
        MainButtons.active = false;

        ConsoleText.enabled = false;

        yield return new WaitForSeconds(1.5f);

        GameOverScreen.active = true; 
    }

    IEnumerator CloseWindow()
    {
        Fader.active = true;

        

        yield return new WaitForSeconds(1);
        
        player.enablePlayerControls = true;
        audioSource.clip = OverworldMusic;
        audioSource.Play();
        Fader.active = false;
        BattleWindow.active = false;

        yield return new WaitForSeconds(1);

       
    }

}
