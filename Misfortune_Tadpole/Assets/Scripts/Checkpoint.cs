using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckPoint;
    public static bool checkpoint;

    private void Start()
    {
        checkpoint = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpoint = true;
            lastCheckPoint = transform.position;
        }
    }
}
