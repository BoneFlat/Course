using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceModeTest : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private ForceMode2D forceMode;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector2.left * force, forceMode);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector2.right * force, forceMode);
        }
    }
}
