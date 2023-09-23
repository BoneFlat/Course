using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection2D : MonoBehaviour
{
    [Tooltip("Select target to move this object to check collider event")]
    public Transform Target;

    [Space(10f)]
    public float MoveSpeed = 5f;

    [HideInInspector] public bool ShouldMove = false;

    private Vector3 RootPosition;

    private void Start()
    {
        RootPosition = transform.position;
        ShouldMove = false;
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (ShouldMove)
            if (Target != null)
            {
                Vector3 direction = Target.position - transform.position;
                float distance = direction.magnitude;

                if (distance > 0.1f)
                {
                    Vector3 moveDirection = direction.normalized;

                    transform.position += moveDirection * MoveSpeed * Time.fixedDeltaTime;
                }
            }
            else if (Target == null)
                Debug.LogWarning("You have to set Target first");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");

        ShouldMove = false;
        Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.velocity = Vector2.zero;
        }
    }

    public void ResetPosition()
    {
        transform.position = RootPosition;
        ShouldMove = false;
    }    

    public void OnMoveToTarget()
    {
        ShouldMove = true;
    }    
}
