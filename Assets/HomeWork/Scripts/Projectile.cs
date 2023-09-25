using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] protected float speed;
    [SerializeField] private Vector3 direction;


    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    public void Initialize(float bulletSpeed, Vector3 bulletDirection)
    {
        speed = bulletSpeed;
        direction = bulletDirection.normalized;
    }

    protected virtual void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
