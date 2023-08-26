using System;
using UnityEngine;

namespace Example
{
    public class FollowingCamera : MonoBehaviour, IAiming
    {
        [SerializeField] private Transform _player;
        
        public Transform target { get; set; }
        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private Vector3 _distance;

        private void Start()
        {
            SetTarget(_player);
            if (target != null)
            {
                _distance = transform.position - target.position;
            }
        }

        private void Update()
        {
            transform.position = target.position + _distance;
        }
    }
}