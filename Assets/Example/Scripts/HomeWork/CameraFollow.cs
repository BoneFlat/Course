using System;
using System.Collections;
using System.Collections.Generic;
using Example;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Player _player;

    private Vector3 offset;
    private Vector3 playerOffset;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        offset = transform.position;
        playerOffset = _player.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = offset + (_player.transform.position - playerOffset);
    }
}
