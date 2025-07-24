using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool enablePlayerControls = true;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Visual")]
    public Animator playerAnimator; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {
        if (enablePlayerControls)
        {
            // Get raw input for 4-directional movement
            movement.x = Input.GetAxisRaw("Horizontal"); // -1 (left) to 1 (right)
            movement.y = Input.GetAxisRaw("Vertical");   // -1 (down) to 1 (up)

            // Normalize so diagonal speed isn't faster
            movement = movement.normalized;
        }

        playerAnimator.SetInteger("MoveX", (int)movement.x);
        playerAnimator.SetInteger("MoveY", (int)movement.y);

        if (movement.x == 0 && movement.y == 0)
            playerAnimator.enabled = false;
        else
            playerAnimator.enabled = true; 
    }

    void FixedUpdate()
    {
        if (enablePlayerControls)
        rb.velocity = movement * moveSpeed;
    }
}
