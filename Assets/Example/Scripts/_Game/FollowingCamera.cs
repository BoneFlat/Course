using System;
using UnityEngine;

namespace Example
{
    public class FollowingCamera : MonoBehaviour, IAiming
    {
        [SerializeField] private Transform _player;
        
        public Transform Target { get; set; }
        public void SetTarget(Transform target)
        {
            this.Target = target;
        }

        private Vector3 _distance;

        private void Start()
        {
            SetTarget(_player);
            if (Target != null)
            {
                _distance = transform.position - Target.position;
            }
        }

        private void Update()
        {
            transform.position = Target.position + _distance;
        }
    }
}