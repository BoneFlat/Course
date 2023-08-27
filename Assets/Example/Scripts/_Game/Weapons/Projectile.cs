using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Weapon SourceWeapon;
    public float MoveSpeed = 5f;

    protected GameObject Target;

    protected Vector3 StartPos;
    private Vector3 _direction;

    protected virtual void FixedUpdate()
    {
        Move();
        Rotate();
    }  

    public void SetDirection(Vector3 newDirection)
    {
        _direction = newDirection.normalized;
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    public void SetStartPosition(Vector3 startPos)
    {
        StartPos = startPos;
    }

    protected virtual void Move()
    {
        transform.position = transform.position + _direction * MoveSpeed * Time.fixedDeltaTime;
    }

    protected virtual void Rotate()
    {
        transform.rotation = Quaternion.LookRotation(_direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemies")
        {
            gameObject.SetActive(false);
        }
    }
}
