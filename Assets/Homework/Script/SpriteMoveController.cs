using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMoveController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float obstacleCheckDistance = 2f; // Khoảng cách kiểm tra vật cản
    private bool isJumping = false;
    private Rigidbody2D rb;
    //private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Ground"));

        float horizontalInput = Input.GetAxis("Horizontal");

        // Tạo một raycast để kiểm tra vật cản
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(horizontalInput), obstacleCheckDistance, LayerMask.GetMask("Obstacle"));

        if (hit.point.x - transform.position.x <= 2f)
        {
            // Nếu raycast gặp vật cản, dừng lại
            horizontalInput = -0.1f;
            Debug.Log($"Distance to object is {hit.point.x - transform.position.x}");
        }

        

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        Debug.DrawRay(transform.position, Vector2.right * Mathf.Sign(horizontalInput) * obstacleCheckDistance, Color.red);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }


}
