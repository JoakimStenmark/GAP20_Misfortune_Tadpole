using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubbleTest : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D rb2d;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
 
        }
    }

    private void SetScaleToNormal()
    {

    }
}
