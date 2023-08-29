using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Perform()
    {
        StartCoroutine(IEPerform());
    }
    
    protected abstract IEnumerator IEPerform();
}