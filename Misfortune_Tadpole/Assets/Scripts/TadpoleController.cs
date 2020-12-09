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
    private bool isStuck;
    private float rotatorSpeed;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = player.GetComponent<Rigidbody2D>();
        stickToSurface = player.GetComponent<StickToSurface>();

    }

    private void Update()
    {
        isStuck = stickToSurface.stuck;
        if (isStuck)
        {
            rotatorSpeed = player.GetComponentInParent<RotatorRotation>().rotationSpeed;
        }
        else
        {
            rotatorSpeed = 0;
        }
        Debug.Log(isStuck);
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
