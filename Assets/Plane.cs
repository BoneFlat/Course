using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision at: " + m_rb);
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
}
