using System;
using UnityEngine;

namespace Example
{
    public class Bullet : MonoBehaviour, IAiming
    {
        [SerializeField] private float _speed;

        public Transform target { get; set; }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void Update()
        {
            transform.position = Vector3.Lerp(transform.position, this.target.position, _speed);

            if ((transform.position - target.position).magnitude < GameStaticVariables.MIN_DISTANCE)
            {
                Destroy(gameObject);
            }
        }
    }
}