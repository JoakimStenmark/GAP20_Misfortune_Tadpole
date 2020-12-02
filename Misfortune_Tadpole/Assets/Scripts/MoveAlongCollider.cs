﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MoveAlongCollider : MonoBehaviour
{
    public bool drawLinesInEditor;
    public Transform[] pathPoints;
    [SerializeField] int currentPoint = 0;
    private GameObject player;
    public bool travel = false;
    Rigidbody2D rb2d;
    public float totalDistance;
    public float timer;
    public AnimationClip speedOverTime;

    [SerializeField] float timeToFinish = 1f; 

    void Start()
    {
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(pathPoints[i].position, pathPoints[i + 1].position);
        }
    }


    void OnDrawGizmos()
    {
        if (drawLinesInEditor)
        {
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                Handles.DrawLine(pathPoints[i].position, pathPoints[(i + 1)].position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (travel)
        {
            if (moveAlong(pathPoints[currentPoint]))
            {
                currentPoint++;
                if (currentPoint >= pathPoints.Length)
                {
                    travel = false;
                    rb2d.isKinematic = false;
                    currentPoint = 0;
                }
                currentPoint = Mathf.Clamp(currentPoint, 0, pathPoints.Length - 1);
            }

        }

        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            Debug.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            rb2d = player.GetComponent<Rigidbody2D>();
            travel = true;           
            rb2d.velocity = Vector3.zero;
            rb2d.isKinematic = true;
            
        }
    }

    public bool moveAlong(Transform point)
    {
        timer += Time.deltaTime; 
        Vector2 pointPosition = new Vector2(point.position.x, point.position.y);
        Vector2 movement = Vector2.MoveTowards(rb2d.position, pointPosition, totalDistance / timeToFinish * Time.deltaTime);     
        rb2d.MovePosition(movement);
        if (pointPosition == rb2d.position)
        {
  
            return true;
        }
        else
        {
            return false;
        }
    }



}
