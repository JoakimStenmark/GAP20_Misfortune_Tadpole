using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [Header("Movement")]
    public bool debug;
    public Vector2 upwards;
    public LayerMask mask;

    [HideInInspector] public bool Grounded { get => grounded;}
    private bool grounded = false;
    private bool secondChance = false;

    private StickToSurface stickToSurface;
    public Rigidbody2D rb2d;
    
    public float velocity;
    public float maxMass;
    public float maxDrag;
    public float minDrag;
    [SerializeField] float jumpForce;
    [SerializeField] float secondChanceTimer;
    private float neutralRotationTimeCount;

    [Header("Water and life")]
    public float startWaterAmount;
    [SerializeField] private float waterAmount;
    public float WaterAmount { get => waterAmount;}

    private bool waterRemovable = true;
    private bool lifeRemovable = true;
    public WaterBar waterBar;
    public LifeManager lifeManager;
    public float lifeLossTimer = 2f;
    private bool alive = true;
    
    [Header("Animation")]
    public TadpoleController tadpole;
    private Animator bubbleAnimator;
    private int damageTakenHash = Animator.StringToHash("tookDamage");
    private SpriteScaler spriteScaler;
    private PlayerSoundControl playerSound;

    private void Awake()
    {
        LoadLastCheckpoint();
    }

    void Start()
    {
        bubbleAnimator = GetComponentInChildren<Animator>();
        spriteScaler = GetComponentInChildren<SpriteScaler>();
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
        if (Input.GetKeyDown("p"))
        {
            debug = !debug; 
        }

        if (debug)
        {
            if (Input.GetKeyDown("k"))
            {
                ChangeLifeAmount(false);
            }

            DebugMovement();
            return;
        }

        if (Input.GetButtonDown("Jump") && (secondChance || grounded) && alive)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            playerSound.PlayJumpSound();
            spriteScaler.JumpWobble();
            tadpole.Jump();
        }

        SetSizeBasedOnWaterAmount();

        velocity = rb2d.velocity.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 2f, mask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -transform.up, 2f, mask);

        if (stickToSurface.stuck)
        {
            /*float diff = transform.position.x - transform.parent.position.x;

            if(Mathf.Abs(diff) > 2f)
                transform.up = transform.position - transform.parent.position;*/
            transform.up = transform.position - transform.parent.position;
        }
        else if (hit.collider != null && hit2.collider != null)
        {
            transform.up = (hit.normal + hit2.normal) / 2;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 2f, Color.red);
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
            tadpole.SetGrounded();
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


    private void SetSizeBasedOnWaterAmount()
    {
        float newSize = 0.49f + waterAmount * 0.005f;
        transform.localScale = new Vector3(newSize, newSize, newSize);

        rb2d.mass = Mathf.Lerp(maxMass * 0.4f, maxMass, waterAmount * 0.01f);
        rb2d.drag = Mathf.Lerp(maxDrag, minDrag, waterAmount * 0.01f);
    }


    public void ChangeWaterAmount(int amount)
    {
        if (alive)
        {
            if (amount > 0)
            {
                playerSound.PlayWaterPickupSound();
            }
            waterAmount += amount;
            waterAmount = Mathf.Clamp(waterAmount, 0f, 100f);
            waterBar.SetWater(waterAmount);
        }

    }

    public void ChangeWaterAmount(int amount, float damageInterval)
    {
        if (waterRemovable && alive)
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
        if (alive)
        {
            if (increase)
            {
                lifeManager.GainLife();
                playerSound.PlayGetLifeSound();
            }
            else
            {
                if (lifeRemovable)
                {
                    lifeRemovable = false;
                    playerSound.PlayHurtSound();

                    if (lifeManager.LooseLife())
                    {
                        bubbleAnimator.SetTrigger(damageTakenHash);
                        Invoke(nameof(SetLifeRemovable), lifeLossTimer);
                        tadpole.Hurt();

                    }
                    else
                    {
                        bubbleAnimator.SetTrigger("Destroy");                    
                        Invoke(nameof(SetLifeRemovable), lifeLossTimer + 99f);
                        tadpole.Die();
                        GetComponent<ParticleSpawner>().active = false;
                        playerSound.MutePlayer();
                    }              
                }
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

    private void DebugMovement()
    {
        rb2d.Sleep();
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }
        rb2d.position += movement * 0.5f;
    }
    
    public void LoadLastCheckpoint()
    {
        if (Checkpoint.checkpoint)
        {
            transform.position = Checkpoint.lastCheckPoint;
        }
    }
}
