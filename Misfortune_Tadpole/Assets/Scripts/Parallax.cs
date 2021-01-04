using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX, startPosY;
    private float lengthX, lengthY;
    public GameObject mainCamera;
    [Range(0.8f, 1.25f)] public float parallax;

    private float previousCameraY = 0;
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y * 1.25f;
        previousCameraY = mainCamera.transform.position.y;

        MoveToTarget();
    }

    void LateUpdate()
    {
        float tempX = mainCamera.transform.position.x * (1 - parallax);
        float tempY = startPosY - mainCamera.transform.position.y;
        

        float distX = (startPosX - mainCamera.transform.position.x) * parallax * -1;
        float distY = (startPosY - mainCamera.transform.position.y) * parallax * -1;

        //Debug.Log(startPosX);
        //Debug.Log("TempX " + tempX);


        float heightDiff = (mainCamera.transform.position.y - previousCameraY);
        previousCameraY = mainCamera.transform.position.y;

        //InfiniteXScrolling(tempX);
        
        if (tempY > startPosY + lengthY * parallax)
        {
            transform.position = new Vector3(startPosX + distX,
                                            transform.position.y + heightDiff,
                                            transform.position.z);
            Debug.Log("Too far down");
            
        }
        /*
        else if (tempY < startPosY - lengthY * parallax)
        {
            transform.position = new Vector3(startPosX + distX,
                                            transform.position.y + heightDiff * Time.deltaTime,
                                            transform.position.z);

            //Debug.Log("Too far down");
        }
        */
        else
        {
            //transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
            Vector3 newPosition = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 50f * Time.deltaTime);
            
        }

    }

    void InfiniteXScrolling(float tempX)
    {
        //*((1 - parallax) + 1)
        if (tempX > startPosX + lengthX)
        {
            Debug.Log("right");
            startPosX += lengthX;
        }
        else if (tempX < startPosX - lengthX)
        {
            Debug.Log("left");
            startPosX -= lengthX;
        }
    }

    void MoveToTarget()
    {
        float distX = (startPosX - mainCamera.transform.position.x) * parallax * -1;
        float distY = (startPosY - mainCamera.transform.position.y) * parallax * -1;
        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

    }
}
