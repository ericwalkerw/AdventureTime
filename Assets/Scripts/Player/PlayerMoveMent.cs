using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private Rigidbody2D player;
    private Animator anim;

    private float horizontal;

    private bool grounded;

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
        Walking();
        Jump();
        SetAnim();
    }
    private void Walking()
    {
        player.velocity = new Vector2(horizontal * speed, player.velocity.y);
        Flip();
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && grounded) 
        {
            player.velocity = new Vector2(player.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            grounded = false;
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
        anim.SetBool("run", horizontal != 0);
        anim.SetBool("grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    public bool canAttack()
    {
        return horizontal == 0;
    }
}
