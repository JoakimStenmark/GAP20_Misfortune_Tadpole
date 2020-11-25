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
    [SerializeField] float jumpForce;
    
    public LayerMask layerMask;

    private float neutralRotationTimeCount;
    private float groundedRotationTimeCount;
    
    Vector3 startPos;

    public float startWaterAmount;
    [SerializeField]
    private float waterAmount;
    public float WaterAmount { get => waterAmount;}
    bool damageable = true;
    float damageTimeCount = 0;
    public float velocity;

    void Start()
    {
        waterAmount = startWaterAmount;
        rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        setSizeBasedOnWaterAmount();

    }

    void Update()
    {

        if (Input.GetKeyDown("r"))
        {
            ResetToLastCheckpoint();
        }

        if (Input.GetButtonDown("Jump") && (secondChance || grounded))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        setSizeBasedOnWaterAmount();

        velocity = rb2d.velocity.magnitude;

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
        waterAmount = startWaterAmount;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            Invoke(nameof(SecondChance), secondChanceTimer);
            

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


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            transform.up = collision.GetContact(0).normal;

        }
    }

    public float maxMass;
    public float maxDrag;
    void setSizeBasedOnWaterAmount()
    {
        float newSize = waterAmount / 50;
        
        newSize = 0.49f + waterAmount * 0.005f;
        transform.localScale = new Vector3(newSize, newSize, newSize);

        rb2d.mass = Mathf.Lerp(maxMass * 0.5f, maxMass, waterAmount * 0.01f);
        rb2d.drag = Mathf.Lerp(maxDrag, 0, waterAmount * 0.01f);
    }


    public void ChangeWaterAmount(int amount)
    {
        waterAmount += amount;
        waterAmount = Mathf.Clamp(waterAmount, 1f, 100f);

    }

    public void ChangeWaterAmount(int amount, float damageInterval)
    {

        if (damageable)
        {
            damageable = false;
            waterAmount += amount;
            Invoke(nameof(SetDamageable), damageInterval);

            if (waterAmount < 1)
            {
                ResetToLastCheckpoint();
            }
        }
        else
        {
            Debug.Log("Player is Invulnerable");
        }
           
    }

    private void SetDamageable()
    {
        damageable = true;
    }
}
