using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StaticCollider"))
        {
            Debug.Log("Rigidbody Collider collides with Static Collider");
        } else if (collision.gameObject.CompareTag("RigidbodyCollider"))
        {
            Debug.Log("Rigidbody Collider collides with Rigidbody Collider");
        }
        else if (collision.gameObject.CompareTag("Kinematic"))
        {
            Debug.Log("Rigidbody Collider collides with Kinematic Collider");
        }

    }
}
