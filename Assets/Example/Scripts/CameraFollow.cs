using System;
using UnityEngine;

namespace Example.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float _smooth = 0.2f;

        private Vector3 _offset;

        private void Start()
        {
            _offset = target.position - transform.position;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position,target.position - _offset, _smooth);
        }

        
    }
}