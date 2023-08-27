using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
   
    private Vector3 _deltaPos;

    private void Start()
    {
        _deltaPos = transform.position - _target.position;
    }

    void FixedUpdate()
    {
        var position = transform.position;
        position = Vector3.Lerp(position, _deltaPos + _target.position, _smoothSpeed);
        transform.position = position;
    }
}
