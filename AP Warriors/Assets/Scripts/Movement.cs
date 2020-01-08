using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    run,
    idle,
    attack,
    stagger
}

public class Movement : MonoBehaviour
{
    //public PlayerStats pStats;

    private Rigidbody2D rb2D;
    private Animator anim;

    [Header("Movement")]
    public float movementSpeed;
    private float moveInput;

    [Header("Ground Jump")]
    public bool isGrounded;
    public LayerMask whatIsGround;
    public float jumpTime;
    public float fallMultiplier;
    [Range(1, 50)] public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    private float jumpTimeCounter;
    private bool isJumping;

    [Header("Player Stats")]
    public string playerName;
    public float currentHealth;
    public float currentMana;
    public float minPhysicalPowerStrength;
    public float maxPhysicalPowerStrength;
    public float minMagicPowerStrength;
    public float maxMagicPowerStrength;
    public float physicalResistance;
    public float magicalResistance;

    void Start()
    {
        /*
        playerName = pStats.name;
        currentHealth = pStats.maxHealth;
        currentMana = pStats.maxMana;
        minPhysicalPowerStrength = pStats.minPhysicalPowerStrength;
        maxPhysicalPowerStrength = pStats.maxPhysicalPowerStrength;
        minMagicPowerStrength = pStats.minMagicPowerStrength;
        maxMagicPowerStrength = pStats.maxMagicPowerStrength;
        magicalResistance = pStats.magicResistance;
        physicalResistance = pStats.physicalResistance;
        */
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        /*
        if (!isLocalPlayer)
        {
            return;
        }
        */
        PlayerJump();
    }


    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
       // DamageOnContact dOnC = other.gameObject.GetComponent<DamageOnContact>();

        if (dOnC && other.gameObject.CompareTag("Projectile"))
        {
            if (pStats.currentHealth > 0)
            {
                currentHealth -= dOnC.Damage;
            }
            else if (pStats.currentHealth <= 0)
            {
                pStats.currentHealth = 0;
                Destroy(this.gameObject);
                //DeathEffect();
            }
        }

    }

        */

    void FixedUpdate()
    {
        /*
        if (!isLocalPlayer)
        {
            return;
        }
        */
        PlayerMove();
    }

    void PlayerMove()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(movementSpeed * moveInput, rb2D.velocity.y);
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (moveInput < 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void PlayerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2D.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetButton("Jump"))
        {
            if (isJumping && jumpTimeCounter > 0)
            {
                rb2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                if (Input.GetButtonUp("Jump"))
                {
                    jumpTimeCounter = 0;
                }
            }
        } else {
            isJumping = false;
        }

        if (!isGrounded)
        {
            anim.SetBool("Jump", true);
        } else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Grounded", true);
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
    }
}







