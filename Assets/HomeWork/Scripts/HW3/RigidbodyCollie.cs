using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCollie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected between two rigidbody colliders.");
    }
}
