using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    private bool isFacingRight = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(isFacingRight)
            rigidbody.velocity = new Vector2(moveSpeed, 0);
        else
            rigidbody.velocity = new Vector2(-moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (isFacingRight)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            isFacingRight = !isFacingRight;
        }
    }
}
