using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMoveMent : MonoBehaviour
{ 

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashCoolDown;
   
    public float wallRadius;
    public float groundRadius;

    public Transform wallCheck;
    public Transform groundCheck;
    public LayerMask whatIsLayer;

    private Rigidbody2D player;
    private Animator anim;

    private float horizontal;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isDashing;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        StartGame();
    }
    private void StartGame()
    {
        CheckSurroudings();
        CheckIfWallSliding();
        Walking();
        Jump();
        SetAnim();
        CheckDash();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && player.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void Walking()
    { 
        player.velocity = new Vector2(horizontal * speed, player.velocity.y);

        if (isWallSliding)
        {
            if (player.velocity.y < -wallSlideSpeed)
            {
                player.velocity = new Vector2(player.velocity.x, -wallSlideSpeed);
            }

            if (Input.GetKey(KeyCode.K))
            {
                isWallSliding = false;
                player.velocity = new Vector2(player.velocity.x, jumpPower);
                anim.SetTrigger("jump");
            }

        }
        if (!isWallSliding)
        {
            Flip();
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                player.velocity = new Vector2(dashSpeed*horizontal, player.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
            }
        }
    }
    private void CheckSurroudings()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsLayer);   
       

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsLayer);      
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wallCheck.position, wallRadius);

        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.K) && isGrounded) 
        {
            player.velocity = new Vector2(player.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            isGrounded = false;
        }
    }
    private void Flip()
    {
        if (horizontal > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    private void SetAnim()
    {
        anim.SetBool("walk", horizontal != 0);
        anim.SetBool("grounded", isGrounded);
        anim.SetBool("sliding", isWallSliding);
    }   
    public bool canAttack()
    {
        return horizontal != 0 || !isGrounded || horizontal == 0;
    }
}
