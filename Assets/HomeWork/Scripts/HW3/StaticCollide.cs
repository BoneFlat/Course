using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollide : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected between a static collider and a rigidbody collider.");
    }
}
