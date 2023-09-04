using System;
using UnityEngine;

namespace Example.Scripts.Homework
{
    public class HwBezierObject : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Vector3 p1, p2, p3;
        private float t;
        
        private void Start()
        {
            p1 = new Vector3(0, 0, 0);
            p2 = new Vector3(5, 15, 0);
            p3 = new Vector3(10, 0, 0);
            t = 0;
        }

        private void Update()
        {
            t += Time.deltaTime * _speed;
            transform.position = GameServices.QuadraticBezierInterp(p1, p2, p3, t);
        }
    }
}