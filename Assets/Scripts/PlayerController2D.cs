using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    public RaycastGroundCheck rgc;

    bool playerIsNearWaifu;

    [SerializeField]
    private float runSpeed = 3f;

    [SerializeField]
    private float jumpSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        MoveHorizontally();
        Jump();
    }

    private void MoveHorizontally()
    {
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if(rgc.IsGrounded())
                animator.Play("Player_run");

            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if(rgc.IsGrounded())
                animator.Play("Player_run");
            
            spriteRenderer.flipX = true;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            if(rgc.IsGrounded())
                animator.Play("Player_idle");
        }
    }

    private void Jump()
    {
        // Debug.Log("IsGrounded bool: " + rgc.IsGrounded());
        if((Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")) && rgc.IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gift"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Waifu"))
        {
            playerIsNearWaifu = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Waifu"))
        {
            playerIsNearWaifu = false;
        }
    }

    private void OnGUI()
    {
        if(playerIsNearWaifu)
        {
            GUI.Box(new Rect(1730, 685, 150, 25), "Press F to converse");
        }
    }
}

// TODO:
    // *Add collectibles - CHECK
    // *Add respawn - CHECK
    // *Add a main menu
    // *Don't let player exit level forever (Add box collider for edges or is there a better practice/way to do this?) - BOX COLLIDER - CHECK
    // Add Cinemachine (really hot kid on the block mwahmwahmwah - diff cameras in levels ie. look down/ahead) - CHECK!
        // Camera re-follow after respawn - CHECK
    // Add animations! (Star eyes for jump, arrow eyes for running) - CHECK
    // *Add dialogue bubbles or new scenes for each one? - NAH
        // OR OVERLAY CANVAS ON TOP OF NORMAL SCENE - CHECK
            // Great text box resources: https://cloudnovel.net/search/resource?q=text&pageNumber=1
        // 1. Walk up to waifu (mwahmwahmwah)
        // 2. Give her a prompt trigger when player is near - Press F to start dialogue - CHECK
        // 3. Overlay the Canvas - CHECK
        // 4. Disable player movement inputs
        // 5. Add uwu owo dialogue choices (and should also say uwu owo occasionally/randomly as you play)
        // 6. Hide Canvas after dialogue - CHECK
        // https://www.youtube.com/watch?v=YJLcanHcJxo
        // https://www.youtube.com/watch?v=WGWubpzz2pw
    // Add level progression
    // Add sound effects and music
    // Modify groundcheck for rotated player - CHECK!!!
    // Make wall collision less janky? Player is sticking to walls which is also kinda cool like Spiderman - CHECK
    // Player P'mis and Uwu Lirge - teehee
    // Eventually rename project one day - https://support.unity.com/hc/en-us/articles/115000086383-How-do-I-change-my-project-s-name- - CHECK
    // Add overlapping box colliders for each side (5) to show better standing positions - CHECK, with polygon colliders!
    // Look into always spinning while moving? - research