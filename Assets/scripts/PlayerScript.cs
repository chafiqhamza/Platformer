using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float playerjump = 16f;
    [SerializeField] float climbingSpeed = 6f;
    [SerializeField] Vector2 deathSpeed = new Vector2(3f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform Gun;
    Rigidbody2D rb;
    Animator animator;
    Vector2 playerInput;
    BoxCollider2D bxcoll;
    CapsuleCollider2D capsuleCollider;
    [SerializeField] float StartGravity;
    bool isAlive = true;
    [SerializeField] float cayote_time = 0.2f;
    float cayoteTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        StartGravity = rb.gravityScale;
        bxcoll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        PlayerFlip();
        Climbing();
        die();

        if (bxcoll.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            cayoteTimer = cayote_time;
        }
        else
        {
            cayoteTimer -= Time.deltaTime;
        }

    }
     void OnFire(InputValue Value) 
       {
        if (!isAlive) { return; }

        Instantiate(bullet,Gun.position,transform.rotation);

       }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        playerInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (value.isPressed && (bxcoll.IsTouchingLayers(LayerMask.GetMask("Ground")) || cayoteTimer > 0))
        {
            rb.velocity += new Vector2(0f, playerjump);
            cayoteTimer = 0f;
        }
    }

    void Climbing()
    {
        if (bxcoll.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            bool PlayerLadder = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            Vector2 playerClimb = new Vector2(rb.velocity.x, playerInput.y * climbingSpeed);
            rb.velocity = playerClimb;
            rb.gravityScale = 0f;
            animator.SetBool("isClimbing", PlayerLadder);
        }
        else
        {
            animator.SetBool("isClimbing", false);
            rb.gravityScale = StartGravity;
        }
    }

    void Run()
    {
        if (!isAlive) { return; }
        bool PlayerRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(playerInput.x * playerSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        animator.SetBool("isRunning", PlayerRunning);
    }

    void PlayerFlip()
    {
        bool PlayerHorizontal = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (PlayerHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void die()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathSpeed;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
