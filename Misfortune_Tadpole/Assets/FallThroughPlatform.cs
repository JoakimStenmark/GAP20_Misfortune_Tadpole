using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThroughPlatform : MonoBehaviour
{
    PlatformEffector2D platformEffector;
    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            platformEffector.useColliderMask = true;
        }
        else
            platformEffector.useColliderMask = false;

    }
}
