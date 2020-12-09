using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleController : MonoBehaviour
{
    GameObject player;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;

        if (rb2d.velocity.x > 0.5)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb2d.velocity.x < 0.5)
        {
            spriteRenderer.flipX = false;
        }
    }
}
