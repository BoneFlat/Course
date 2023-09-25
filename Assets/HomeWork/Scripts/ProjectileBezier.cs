using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileBezier : Projectile
{
    public QuadraticCurve curve;
    [SerializeField] private Transform _enemy;
    public Transform Enemy
    {
        get { return _enemy;}
        set { _enemy = value; }
    }
    // Start is called before the first frame update

    private float sampleTime;
    void Start()
    { 
    }


    protected override void FixedUpdate()
    {
        sampleTime += Time.deltaTime * speed;
        transform.position = curve.evaluate(sampleTime);
        transform.forward = curve.evaluate(sampleTime + 0.001f) - transform.position;
        
    }

    public void SetBezierMovement(Vector3 p2)
    {
        sampleTime = 0;
        transform.GetComponent<QuadraticCurve>().p0 = transform.position;
        transform.GetComponent<QuadraticCurve>().p2 = p2;
        transform.GetComponent<QuadraticCurve>().p1 = (transform.position + p2) + Vector3.up * 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
