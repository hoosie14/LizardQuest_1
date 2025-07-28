using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public bool enablePlayerControls = true;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Visual")]
    public Animator playerAnimator;

    [Header("PlayerStats")]
    public float Score;
    public float Level;

    public TMP_Text LevelDisplayer;
    public TMP_Text XPDisplayer;

    public BattleScript1 battleScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {
        if (enablePlayerControls)
        {
            moveSpeed = 5f;
            // Get raw input for 4-directional movement
            movement.x = Input.GetAxisRaw("Horizontal"); // -1 (left) to 1 (right)
            movement.y = Input.GetAxisRaw("Vertical");   // -1 (down) to 1 (up)

            // Normalize so diagonal speed isn't faster
            movement = movement.normalized;
        }
        else
        {
            moveSpeed = 0f;
        }

        playerAnimator.SetInteger("MoveX", (int)movement.x);
        playerAnimator.SetInteger("MoveY", (int)movement.y);

        if (movement.x == 0 && movement.y == 0)
            playerAnimator.enabled = false;
        else
            playerAnimator.enabled = true;

        if (Score >= 4000)
        {
            print("LevelUp!");
            Level += 1;
            Score = 0;
            battleScript.MaxPlayerHP += 5;
            battleScript.PlayerDamage += 1;
        }

        LevelDisplayer.SetText(Level.ToString());
        XPDisplayer.SetText(Score.ToString());
    }

    void FixedUpdate()
    {
        if (enablePlayerControls)
        rb.velocity = movement * moveSpeed;
    }
}
