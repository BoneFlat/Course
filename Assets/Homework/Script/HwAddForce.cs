using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwAddForce : MonoBehaviour
{
    public float force = 1.0f; // You can adjust the value of thrust in Unity editor
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Replace "Jump" with any key or input event you want
        {
            rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
    }
}
