using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorRotation : MonoBehaviour
{
    Rigidbody2D playerRb2d;
    Rigidbody2D rotatorRb2d;
    float rotationSpeed = 0;


    private void Start()
    {
        rotatorRb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
      /*  rotationSpeed = rotationSpeed + 0.00000000001f;
        rotatorRb2d.MoveRotation(rotationSpeed);

        if (rotationSpeed == 360)
        {
            rotationSpeed = 0;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
            playerRb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb2d.isKinematic = true;
            playerRb2d.gravityScale = 0;
        }
    }
}
