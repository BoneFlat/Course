using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform transformToFolow;

    private void Update()
    {
        if(transformToFolow == null) {return;}
        
        transform.position = transformToFolow.position;
    }
}
