﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleController : MonoBehaviour
{
    GameObject player;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private StickToSurface stickToSurface;
    private bool isStuck;
    private float rotatorSpeed;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = player.GetComponent<Rigidbody2D>();
        stickToSurface = player.GetComponent<StickToSurface>();
        animator = GetComponent<Animator>();
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

        animator.SetFloat("Velocity", rb2d.velocity.SqrMagnitude());

    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        
        
        if (rb2d.velocity.x > 0.5 || rotatorSpeed < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb2d.velocity.x < 0.5 || rotatorSpeed > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
