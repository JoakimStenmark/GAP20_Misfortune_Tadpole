using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float secondChanceTimer;

    private Rigidbody2D rb2d;
    bool grounded = false;
    private bool secondChance = true;
    public LayerMask layerMask;

    private float neutralRotationTimeCount;
    private float groundedRotationTimeCount;
    Vector3 startPos;
    
    [SerializeField] float jumpForce;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown("r"))
        {
            ResetToLastCheckpoint();
        }

        if (Input.GetButtonDown("Jump") && (secondChance || grounded))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void FixedUpdate()
    {

        if (grounded)
        {
            neutralRotationTimeCount = 0;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, neutralRotationTimeCount);
            neutralRotationTimeCount += Time.deltaTime * 0.5f;
        }

    }

    public void ResetToLastCheckpoint()
    {
        rb2d.position = startPos;
        rb2d.velocity = new Vector3();
    }

    private void SecondChance()
    {
        secondChance = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CancelInvoke("SecondChance");
            grounded = true;
            secondChance = true;
            RotateBasedOnGroundNormal();
        }
        else if (collision.gameObject.CompareTag("Wall") && grounded)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        RotateBasedOnGroundNormal();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            Invoke("SecondChance", secondChanceTimer);
            

        }
        else if (collision.gameObject.CompareTag("Wall") && grounded)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            RotateBasedOnGroundNormal();

        }
        else if (collision.gameObject.CompareTag("Wall") && grounded)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void RotateBasedOnGroundNormal()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.red, 1);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1f, layerMask);
        if (hit)
        {
            //transform.rotation = new Quaternion(hit.normal.x, hit.normal.y, 0, 0);            
            //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, 0, -hit.normal.x, hit.normal.y), groundedRotationTimeCount);
            transform.up = Vector3.Slerp(transform.up ,new Vector3(hit.normal.x, hit.normal.y, 0), groundedRotationTimeCount);            
            groundedRotationTimeCount += Time.deltaTime;
            //Debug.Log("rotate like " + hit.collider.gameObject);

        }
        else
        {
            groundedRotationTimeCount = 0;
            transform.rotation = Quaternion.identity;
        }


    }
}
