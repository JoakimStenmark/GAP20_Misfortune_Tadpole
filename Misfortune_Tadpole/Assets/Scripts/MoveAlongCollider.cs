using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongCollider : MonoBehaviour
{
    
    public Transform[] pathPoints;
    [SerializeField] int currentPoint = 0;
    private GameObject player;
    public bool travel = false;
    Rigidbody2D rb2d;

    [SerializeField] float speed = 1f; 

    void Start()
    {
        pathPoints = GetComponentsInChildren<Transform>();
        
    }
    private void Update()
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
                }
                currentPoint = Mathf.Clamp(currentPoint, 0, pathPoints.Length - 1);
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.transform.position += new Vector3(pathPoints[currentPoint].x, pathPoints[currentPoint].y, 0);
            //other.gameObject.GetComponent<StickToSurface>().moveAlong(pathPoints[currentPoint]);
            //if (other.gameObject.GetComponent<StickToSurface>().moveAlong(pathPoints[currentPoint]))
            //{
            //    currentPoint = Mathf.Clamp(currentPoint++, 0, pathPoints.Length);
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //currentPoint = 0;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            rb2d = player.GetComponent<Rigidbody2D>();
            travel = true;           
            rb2d.velocity = Vector3.zero;
            rb2d.isKinematic = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //currentPoint = 0;
    }

    public bool moveAlong(Transform point)
    {
        Debug.Log("Moving");

        Vector2 pointPosition = new Vector2(point.position.x, point.position.y);
        Vector2 movement = Vector2.MoveTowards(rb2d.position, pointPosition, speed * Time.deltaTime);
        Debug.Log(movement);

        rb2d.MovePosition(movement);
        //player.transform.position = new Vector3(movement.x, movement.y, 0f);
        if (pointPosition == rb2d.position)
        {
            Debug.Log("point reached");
            return true;
        }
        else
        {
            return false;
        }
    }



}
