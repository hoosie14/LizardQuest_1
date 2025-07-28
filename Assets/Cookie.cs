using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cookie : MonoBehaviour
{
    [HideInInspector]
    public bool collected = false; 

    public PlayerMovement cc;
    public BattleScript1 bs;

    public GameObject DialogueBox;
    private GameObject InstantiatedBox;
    public GameObject _dialougeBox_Parent;

    public GameObject Visual;

    [HideInInspector] public TMP_Text _Text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cc = collision.GetComponent<PlayerMovement>();

        if (cc)
        {
            bs._cookies += 1;
            cc.moveSpeed = 0f;
            cc.enablePlayerControls = false;
            

            InstantiatedBox = Instantiate(DialogueBox,
                new Vector3(840, 400, 0),
                Quaternion.identity,
                _dialougeBox_Parent.transform
                ) as GameObject;
            _Text = InstantiatedBox.GetComponentInChildren<TMP_Text>();
            _Text.text = ("<YOU GOT (1) COOKIES!>");

            Visual.active = false; 

            collected = true; 
        }

        
    }

    private void Update()
    {
        if (collected)
        {
            if (Input.GetButtonDown("Interact"))
            {
                cc.enablePlayerControls = true;
                cc.moveSpeed = 5f;
                Destroy(InstantiatedBox);

                Destroy(this.gameObject);
            }
        }
    }
}
