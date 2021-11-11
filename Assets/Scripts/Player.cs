using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float gravity = 1f;
    [SerializeField] BoxCollider2D feetCollider;
    [SerializeField] Transform startPoint;
    [SerializeField] float startTimeInterval;
    [SerializeField] float winTimeInterval;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2d;

    private bool disabled = false;
        
    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider2d = gameObject.GetComponent<BoxCollider2D>();

        gameObject.transform.position = startPoint.position;
        StartCoroutine(StartFromPortal());

    }

    private IEnumerator StartFromPortal()
    {
        disabled = true;
        rigidBody.gravityScale = 0;

        yield return new WaitForSeconds(startTimeInterval);

        disabled = false;
        rigidBody.gravityScale = gravity;
    }

    public void SetPlayerOnWinStatus()
    {
        disabled = true;
        rigidBody.velocity = new Vector2(0f, 0f);
        rigidBody.gravityScale = 0;
        animator.SetTrigger("win");
    }

    void Update()
    {
        if (!disabled)
        {
            Climb();
            Move();
            Jump();
        }
    }

    private void Climb()
    {
        if (boxCollider2d.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            float inputMove = Input.GetAxis("Vertical");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, climbSpeed * inputMove);

            animator.SetBool("climb", true);
            rigidBody.gravityScale = 0f;
        }
        else
        {
            animator.SetBool("climb", false);
            rigidBody.gravityScale = gravity;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
        }
    }

    private void Move()
    {
        float inputMove = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(moveSpeed * inputMove, rigidBody.velocity.y);

        if(inputMove != 0f)
        {
            spriteRenderer.flipX = (inputMove > 0f) ? false : true;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !disabled)
        {
            disabled = true;
            animator.SetTrigger("dead");
            rigidBody.velocity = deathKick;
            GameSession gameSession = FindObjectOfType<GameSession>();
            if(gameSession != null)
                gameSession.playerDeathManage();
        }
    }
}
