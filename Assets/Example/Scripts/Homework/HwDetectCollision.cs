using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Example.Scripts.Homework
{
    public class HwDetectCollision : MonoBehaviour
    {
        [SerializeField] private float _lengthDetect;
        
        private void Update()
        {
            var hit = Physics2D.RaycastAll(transform.position, Vector2.right, _lengthDetect);
            CanMove = !(hit.Length > 1);
        }

        public bool CanMove;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _lengthDetect);
        }
    }
}