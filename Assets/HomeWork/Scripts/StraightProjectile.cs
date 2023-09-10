using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : Projectile
{
    [SerializeField] private float speed = 10f;
    
    protected override IEnumerator IEPerform()
    {
        if(target == null) { yield break;}

        while ((transform.position - target.position).sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
