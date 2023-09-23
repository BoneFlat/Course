using System;
using UnityEngine;

public class HwAddForce : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public ForceMode2D forceMode;
    public float speed;
    public bool isExplainForceMode = false;
    public LayerMask obstacleLayer;
    public float lenght = 10;

    private float _horizontal;
    private Vector2 _direction;
    private Vector2 _from;
    private RaycastHit2D _hit;
    

    private void Start()
    {
        if (!isExplainForceMode) return;
        switch (forceMode)
        {
            case ForceMode2D.Force:
                Debug.Log("Apply the force in each FixedUpdate over a duration of time");
                break;
            case ForceMode2D.Impulse:
                Debug.Log("Apply the impulse force instantly");
                Debug.Log("should use speed * deltaTime/fixedDeltaTime");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void FixedUpdate()
    {
        if (Rigidbody2D is null) return;
        _horizontal = Input.GetAxisRaw("Horizontal");
        if (_horizontal == 0 && Rigidbody2D.velocity == Vector2.zero)
        {
            return;
        }
        if (_horizontal != 0)
        {
            _direction = _horizontal > 0 ? Vector2.right : Vector2.left;
        }
        _from = (Vector2) transform.position + _direction * (transform.localScale.x / 2);
        _hit = Physics2D.Raycast(_from, _direction, lenght, obstacleLayer);
        if (_hit && _hit.distance <= 2)
        {
            Debug.Log("obstacle");
            Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        Rigidbody2D.AddForce(_horizontal * speed * Vector2.right, forceMode);
    }

    private void OnDrawGizmos()
    {
        float distance;
        if (_hit && _hit.distance <= 2)
        {
            Gizmos.color = Color.red;
            distance = _hit.distance;
        }
        else
        {
            Gizmos.color = Color.green;
            distance = lenght;
        }
        Gizmos.DrawRay(_from, _direction * distance);
    }
}