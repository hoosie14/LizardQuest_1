using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NPCTest_Updated : MonoBehaviour
{    
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
    public List<string> _DialougeList;
    public int _Current_Index;
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
        if (!IsInstantiated & _IsInteractable & Input.GetButtonDown("Interact"))
        {
            //Instantiate
            _dialougeBox_Instantiated = Instantiate(
                _dialougeBox,
                new Vector3(250, 78, 0),
                Quaternion.identity,
                _dialougeBox_Parent.transform
                ) as GameObject;

            //Disable Movement and set needed variables
            playerMovement.enablePlayerControls = false;
            IsInstantiated = true;
            _Text = _dialougeBox_Instantiated.GetComponentInChildren<TMP_Text>();
            _Text.text = _DialougeList[0];
            _Current_Index = 0;
        } else if (IsInstantiated & Input.GetButtonDown("Interact"))
        {
            _Current_Index += 1;
            if (_Current_Index >= _DialougeList.Count)
            {
                Destroy(_dialougeBox_Instantiated);
                _Current_Index = 0;
                playerMovement.enablePlayerControls = true;
                IsInstantiated = false;
                
            }
            else
            {
                _Text.text = _DialougeList[_Current_Index];   
            }
        }     
    }
}
