using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private LayerMask layerMask;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }


    private void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void Update()
    {
        Debug.DrawLine(rb.position, rb.position + direction * 2);
        var hit = Physics2D.Raycast(rb.position, direction, 2f, layerMask);
        if (hit != default)
        {
            rb.velocity = Vector2.zero;
            return;
        }


        rb.velocity = direction * speed;
    }

}
