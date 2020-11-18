using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    bool grounded = false;
    Vector3 startPos;
    
    [SerializeField] float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("hello");
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }


        if (Input.GetKeyDown("r"))
        {
            rb2d.position = startPos;
            rb2d.velocity = new Vector3();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }
}
