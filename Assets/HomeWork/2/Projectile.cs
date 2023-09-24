using System;
using Example;
using UnityEngine;

namespace HomeWork
{
    public enum TrajectoryType
    {
        Linear,
        Bezier
    }
    public class Projectile : MonoBehaviour
    {
        private float _lifeTime;
        private float _speed;
        private float _timeElapsed;
        
        private Vector3 _direction;
        private Vector3 _originPosition;
        private Vector3 _target;
        
        private TrajectoryType _trajectoryType;

        private void Awake()
        {
            _originPosition = transform.position;
        }

        public void Shoot(Vector3 direction, float lifeTime, float speed, TrajectoryType type)
        {
            _direction = direction;
            _lifeTime = lifeTime;
            _speed = speed;
            _target = transform.position + _direction * (_speed * _lifeTime);
            
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            _trajectoryType = type;
        }

        private void FixedUpdate()
        {
            _timeElapsed += Time.fixedDeltaTime;
            
            if (_trajectoryType == TrajectoryType.Linear)
            {
                transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
            }
            else
            {
                var t = _timeElapsed / _lifeTime;
                transform.position = MathfHelper.QuadraticBezier(_originPosition,
                    (_originPosition + _target) * 0.5f + Vector3.up * 10, _target, t);
            }
            
            if (_timeElapsed >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}