using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NormalProjectile : Projectile
{
    public string targetTag = "Enemy";

    protected override void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * speed);
       
        if (transform.position == endPoint)
        {
            Destroy(gameObject);
        } 
    }

    public void SetTarget(Vector3 _target)
    {
        endPoint = _target;
    }

    
}
