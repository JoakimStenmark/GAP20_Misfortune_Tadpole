using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX, startPosY;
    private float length;
    public GameObject mainCamera;
    [Range(0.0f, 1.0f)] public float parallax;


    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

    }

    void FixedUpdate()
    {
        float distX = mainCamera.transform.position.x * parallax;
        float distY = mainCamera.transform.position.y * parallax;

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
    }
}
