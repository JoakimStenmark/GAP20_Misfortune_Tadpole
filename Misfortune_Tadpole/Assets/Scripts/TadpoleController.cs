using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleController : MonoBehaviour
{
    GameObject player;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private StickToSurface stickToSurface;
    private PlayerController playerController;
    private bool isStuck;
    public float rotatorSpeed;
    private Animator animator;

    public float fallTime;
    private string[] triggers;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = player.GetComponent<PlayerController>();
        rb2d = player.GetComponent<Rigidbody2D>();
        stickToSurface = player.GetComponent<StickToSurface>();
        animator = GetComponent<Animator>();
        triggers = new string[] { "TurnRight", "TurnLeft", "Grounded" };
    }

    private void Update()
    {
        isStuck = stickToSurface.stuck;
        if (isStuck && player.transform.parent.tag == "Rotator")
        {
            rotatorSpeed = player.GetComponentInParent<RotatorRotation>().rotationSpeed;
        }
        else
        {
            rotatorSpeed = 0;
        }

        if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("Jumping", false);
        }

        fallTime += Time.deltaTime;
        
        if (isStuck || playerController.Grounded)
        { 
            fallTime = 0f;
            SetGrounded();
        }
        else
            animator.SetBool("Grounded", false);

        SetFalltime(fallTime);
        animator.SetFloat("Xvelocity", Mathf.Abs(rb2d.velocity.x));
        
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;

        if (rb2d.velocity.x > 0.5 || rotatorSpeed < 0)
        {

            if (spriteRenderer.flipX == false && rb2d.velocity.x > 1.5)
            {
                animator.SetTrigger("TurnLeft");
            }
            spriteRenderer.flipX = true;
        }
        
        if (rb2d.velocity.x < -0.5 || rotatorSpeed > 0)
        {
            if (spriteRenderer.flipX == true && rb2d.velocity.x < -1.5)
            {
                animator.SetTrigger("TurnLeft");
            }
            spriteRenderer.flipX = false;
        }

    }

    public void ResetTriggers()
    {
        foreach (string trigger in triggers)
        {
            animator.ResetTrigger(trigger);
        }
    }

    public void Jump()
    {
        ResetTriggers();
        //animator.SetBool("Grounded", false);
        animator.SetBool("Jumping", true);
    }
    public void Hurt()
    {
        ResetTriggers();
        animator.SetTrigger("Hurt");
    }
    public void SetGrounded()
    {
        ResetTriggers();
        animator.SetBool("Jumping", false);
        animator.SetBool("Grounded", true);

    }

    public void SetFalltime(float time)
    {
        animator.SetFloat("FallTime", time);
    }


    public void FlipSprite()
    {
  
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    


}
