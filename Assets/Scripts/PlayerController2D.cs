using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;

    [SerializeField]
    Transform groundCheck = null;
    [SerializeField]
    Transform groundCheckLeft = null;
    [SerializeField]
    Transform groundCheckRight = null;

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
        CheckIfGrounded();

        MoveHorizontally();
        Jump();
    }

    private void MoveHorizontally()
    {
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if(isGrounded)
                animator.Play("Player_run");

            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if(isGrounded)
                animator.Play("Player_run");
            
            spriteRenderer.flipX = true;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            if(isGrounded)
                animator.Play("Player_idle");
        }
    }

    private void Jump()
    {
        if((Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_jump");
        }
    }

    private void CheckIfGrounded()
    {
        if(
            Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"))
        )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.Play("Player_jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gift"))
        {
            Destroy(collision.gameObject);
        }
    }
}

// TODO:
    // *Add collectibles - CHECK
    // *Add respawn - CHECK
    // *Add a main menu
    // *Don't let player exit level forever (Add box collider for edges or is there a better practice/way to do this?) - BOX COLLIDER - CHECK
    // *Do we want a camera follow? Must for decent 2D platformers lol? ->
        // Either make own camera follow script Cinemachine (really hot kid on the block mwahmwahmwah - diff cameras in levels ie. look down/ahead)
        // Camera re-follow after respawn: https://youtu.be/icKfFnnHHIY?t=438
    // Add animations! (Star eyes for jump, arrow eyes for running) - CHECK
    // *Add dialogue bubbles or new scenes for each one? - NAH
        // OR OVERLAY CANVAS ON TOP OF NORMAL SCENE - https://youtu.be/YJLcanHcJxo?t=609
        // Scripts for comparison: https://drive.google.com/drive/folders/1UaZPu8hQp11xprABSteBAphIZSJpenHw
        // 1. Walk up to waifu (mwahmwahmwah)
        // 2. Give her a trigger function - Press F to start dialogue
        // 3. Overlay the Canvas and disable inputs
        // 4. Add uwu owo dialogue choices (and should also say uwu owo occasionally/randomly as you play)
        // 5. Hide Canvas after dialogue
        // https://www.youtube.com/watch?v=YJLcanHcJxo
        // https://www.youtube.com/watch?v=WGWubpzz2pw
    // Add level progression
    // Add sound effects and music
    // Look into linecasting in multiple directions for rotated player? Like Super Bunny Man https://youtu.be/hMNERppkHOk?t=47
        // Global position or condition for being on ground tied to collision of ground - ATTEMPTED - but lets them escape death by jumping on walls forever while falling
        // Or various groundchecks on different sides and disable other sides depending on which side is in contact
        // https://www.youtube.com/watch?v=wi-RL4sUayo
    // Make wall collision less janky? Player is sticking to walls which is also kinda cool like Spiderman - CHECK
    // Player P'mis and Uwu Lirge
    // Eventually rename project one day - https://support.unity.com/hc/en-us/articles/115000086383-How-do-I-change-my-project-s-name-