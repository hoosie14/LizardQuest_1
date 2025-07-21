using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCScript1 : MonoBehaviour
{
    [HideInInspector]
    public float _presses = 0;

    public bool _checkInteractions = true;

    [HideInInspector]
    public Collider2D collider;

    public Collider2D playerCollision;

    public GameObject _dialogueBox;
    public TMP_Text _Text;

    [HideInInspector]
    public bool QueDialogue = false; 

    public PlayerMovement playerMovement;

    [Header("dialogue")]

    
    public float _lines; 

    public string Line1;
    public string Line2;
    public string Line3;
    public string Line4;
    public string Line5;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (QueDialogue == false)
        {
            if (_checkInteractions)
                CheckInteract();
        }

        //

        if (Input.GetButtonDown("Interact"))
            _presses += 1;

        if (_presses > _lines)
        {
            _dialogueBox.active = false;
            playerMovement.enablePlayerControls = true;

            _presses = 0;

            QueDialogue = false;
        }

        if (_presses == 1)
            _Text.text = Line1;
        else if (_presses == 2)
            _Text.text = Line2;
        else if (_presses == 3)
            _Text.text = Line3;
        else if (_presses == 4)
            _Text.text = Line4;
        else if (_presses == 5)
            _Text.text = Line5;

        //

        

    }

    public void CheckInteract()
    {
        if (collider.IsTouching(playerCollision))
        {
            //playerMovement = playerCollision.GetComponent<PlayerMovement>();

            if (Input.GetButtonDown("Interact"))
            {
                _dialogueBox.active = true;

                playerMovement.enablePlayerControls = false;

                QueDialogue = true;

                //Dialogue();
            }
        }
    }
}
