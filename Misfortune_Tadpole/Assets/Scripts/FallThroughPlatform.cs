using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FallThroughPlatform : MonoBehaviour
{
    PlatformEffector2D platformEffector;
    SpriteShapeRenderer spriteShapeRenderer;
    Color alphaColor;
    Color currentColor;

    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
        currentColor = spriteShapeRenderer.color;
        alphaColor = currentColor;
        alphaColor.a = 0.5f;
    }

    void Update()
    {
        if (Input.GetButton("Jump") || TouchInput.instance.TouchContinue())
        {
            platformEffector.useColliderMask = true;
            spriteShapeRenderer.color = alphaColor;
        }
        else
        {
            platformEffector.useColliderMask = false;
            spriteShapeRenderer.color = currentColor;
        }


    }
}
