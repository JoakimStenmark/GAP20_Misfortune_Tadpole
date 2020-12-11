using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] float secondChanceTimer = 0;

    private Animator anim;
    private int damageTakenHash = Animator.StringToHash("tookDamage");

    private bool grounded = false;
    private bool secondChance = false;
    [SerializeField] float jumpForce = 0;

    private float neutralRotationTimeCount;
    private float groundedRotationTimeCount;

    public Vector2 upwards;
    public LayerMask mask;
    private StickToSurface stickToSurface;


    public Rigidbody2D rb2d;
    public float startWaterAmount;
    [SerializeField]
    private float waterAmount;
    public float WaterAmount { get => waterAmount; }
    private bool waterRemovable = true;
    private bool lifeRemovable = true;
    public float velocity;

    public WaterBar waterBar;

    public LifeManager lifeManager;
    public float lifeLossTimer = 2f;

    private PlayerSoundControl playerSound;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        waterAmount = startWaterAmount;
        rb2d = GetComponent<Rigidbody2D>();
        SetSizeBasedOnWaterAmount();
        stickToSurface = GetComponent<StickToSurface>();

        waterBar.SetMaxWater(100);
        waterBar.SetWater(waterAmount);

        playerSound = GetComponentInChildren<PlayerSoundControl>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && (secondChance || grounded))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            playerSound.PlayJumpSound();
        }

        SetSizeBasedOnWaterAmount();

        velocity = rb2d.velocity.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.5f, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -transform.up, 1.5f, mask);

        if (hit.collider != null && hit2.collider != null)
        {
            transform.up = (hit.normal + hit2.normal) / 2;
        }

        if (stickToSurface.stuck)
        {
            transform.up = transform.position - transform.parent.position;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 1.5f, Color.red);
    }
#endif

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

    private void SecondChance()
    {
        secondChance = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            CancelInvoke(nameof(SecondChance));
            grounded = true;
            secondChance = true;
            playerSound.PlayLandSound();
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

    public float maxMass;
    public float maxDrag;
    public float minDrag;
    private void SetSizeBasedOnWaterAmount()
    {
        float newSize = 0.49f + waterAmount * 0.005f;
        transform.localScale = new Vector3(newSize, newSize, newSize);

        rb2d.mass = Mathf.Lerp(maxMass * 0.4f, maxMass, waterAmount * 0.01f);
        rb2d.drag = Mathf.Lerp(maxDrag, minDrag, waterAmount * 0.01f);
    }


    public void ChangeWaterAmount(int amount)
    {
        waterAmount += amount;
        waterAmount = Mathf.Clamp(waterAmount, 0f, 100f);
        waterBar.SetWater(waterAmount);
        playerSound.PlayWaterPickupSound();
    }

    public void ChangeWaterAmount(int amount, float damageInterval)
    {
        if (waterRemovable)
        {
            waterRemovable = false;

            if (waterAmount < 1)
            {
                ChangeLifeAmount(false);
            }
            ChangeWaterAmount(amount);
            Invoke(nameof(SetWaterRemovable), damageInterval);
        }
    }

    public void ChangeLifeAmount(bool increase)
    {
        if (increase)
        {
            lifeManager.GainLife();
        }
        else
        {
            if (lifeRemovable)
            {
                lifeRemovable = false;
                lifeManager.LooseLife();
                anim.SetTrigger(damageTakenHash);
                Invoke(nameof(SetLifeRemovable), lifeLossTimer);
                playerSound.PlayHurtSound();
            }
        }
    }

    private void SetWaterRemovable()
    {
        waterRemovable = true;
    }

    private void SetLifeRemovable()
    {
        lifeRemovable = true;
    }
}
