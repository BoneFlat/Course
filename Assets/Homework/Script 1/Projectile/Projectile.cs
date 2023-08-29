using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    protected Vector3 startPoint;
    protected Vector3 controlPoint;
    protected Vector3 endPoint;

    protected float timeElapsed;

    protected virtual void Update() {}
    protected virtual void Movement(Vector3 direction) {}


}
