using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinetic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected between a rigidbody collider and a kinematic rigidbody collider.");
    }
}
