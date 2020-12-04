using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX, startPosY;
    private float lengthX, lengthY;
    public GameObject mainCamera;
    [Range(0.5f, 1.0f)] public float parallax;

    private float previousCameraY = 0;
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    void FixedUpdate()
    {
        float distX = mainCamera.transform.position.x * parallax;
        float distY = mainCamera.transform.position.y * parallax;
        float tempX = mainCamera.transform.position.x * (1 - parallax);
        float tempY = mainCamera.transform.position.y - startPosY;

        InfiniteXScrolling(tempX);

        float heightDiff = (mainCamera.transform.position.y - previousCameraY) * Time.deltaTime;
        previousCameraY = mainCamera.transform.position.y;


        if (tempY > startPosY + lengthY * (1 - parallax))
        {
            transform.position = new Vector3(startPosX + distX,
                                            transform.position.y + heightDiff * Time.deltaTime,
                                            transform.position.z);
        }
        else if (tempY < startPosY - lengthY * (1 - parallax))
        {
            transform.position = new Vector3(startPosX + distX,
                                            transform.position.y + heightDiff * Time.deltaTime,
                                            transform.position.z);
        }
        else
            transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

    }

    void InfiniteXScrolling(float tempX)
    {
        if (tempX > startPosX + lengthX)
        {
            startPosX += lengthX;
        }
        else if (tempX < startPosX - lengthX)
        {
            startPosX -= lengthX;
        }
    }
}
