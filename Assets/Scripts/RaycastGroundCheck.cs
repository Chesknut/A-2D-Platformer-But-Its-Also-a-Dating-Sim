using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;

    public bool IsGrounded() {
        Animator animator = GetComponentInParent<Animator>();

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.6f;

        Debug.DrawRay(position, direction, Color.blue);
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            // Debug.Log("Touched object");
            return true;
        }
        else
        {
            // Debug.Log("No touch");
            animator.Play("Player_jump");
            return false;
        }
    }
}
