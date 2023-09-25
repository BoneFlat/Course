using System;
using UnityEngine;

namespace Example.Scripts
{
    [DefaultExecutionOrder(10000)]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform  target;
        [SerializeField] private float      _smooth = 0.2f;
        [SerializeField] private UpdateMode _updateMode;

        private Vector3 _offset;

        private void Start()
        {
            _offset = target.position - transform.position;
<<<<<<< HEAD
            //gameObject.layer = LayerMask.NameToLayer("Default");
=======
>>>>>>> master
        }

        private void Update()
        {
            if (_updateMode == UpdateMode.Update)
            {
                transform.position = target.position - _offset;
            } else if (_updateMode == UpdateMode.LerpUpdate)
            {
                transform.position = Vector3.Lerp(transform.position,target.position - _offset, _smooth);
            }
        }

        private void FixedUpdate()
        {
            if (_updateMode == UpdateMode.FixedUpdate)
            {
                transform.position = target.position - _offset;
            } else if (_updateMode == UpdateMode.LerpFixedUpdate)
            {
                transform.position = Vector3.Lerp(transform.position,target.position - _offset, _smooth);
            }
        }
        
        public enum UpdateMode
        {
            FixedUpdate,
            Update,
            LerpFixedUpdate,
            LerpUpdate
        }
    }
}