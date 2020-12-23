using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
    private float startPosX, startPosY, startPosZ;
    private float lengthX, lengthY;
    public GameObject mainCamera;
    public float parallax;

    private float previousCameraY = 0;
    private Vector3 previousCameraPos;

    public float smoothing;
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startPosZ = transform.position.z;
        parallax = startPosZ;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        previousCameraPos = mainCamera.transform.position;


    }

    void LateUpdate()
    {
        float distX = mainCamera.transform.position.x * parallax;
        float distY = mainCamera.transform.position.y * parallax;
        float tempX = mainCamera.transform.position.x * (1 - parallax);
        float tempY = mainCamera.transform.position.y - startPosY;

        //InfiniteXScrolling(tempX);

        Vector3 posDiff = (previousCameraPos - mainCamera.transform.position) * parallax;

        Vector3 targetPos = new Vector3(posDiff.x, posDiff.y, startPosZ);
        
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
        
        previousCameraPos = mainCamera.transform.position;

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
