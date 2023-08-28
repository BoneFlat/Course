using System;
using UnityEngine;

namespace Example
{
    public class Bullet : MonoBehaviour, IAiming, IMoving
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _timeExist;

        private Vector3 _dir;

        public Transform Target { get; set; }

        public virtual void SetTarget(Transform target)
        {
            Target = target;
            _dir   = (Target.position - transform.position).normalized;
        }

        public void FixedUpdate()
        {
            transform.position = GetNextMove();

            _timeExist -= Time.fixedDeltaTime;
            if (_timeExist < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(GameStaticVariables.ENEMY_TAG))
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameStaticVariables.ENEMY_TAG))
            {
                Destroy(gameObject);
            }
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public Vector3 Direction
        {
            get => _dir;
            set => _dir = value;
        }

        public virtual Vector3 GetNextMove()
        {
            return transform.position;
        }
    }
}