using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 range;

    private void Awake()
    {
        range = player.position - transform.position;
    }

    private void Update()
    {
        transform.position = player.position - range;
    }
}
