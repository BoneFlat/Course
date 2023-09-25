using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HomeWork._3
{
    public class TestAddForce : MonoBehaviour
    {
        public Rigidbody2D _rigidbody2D;
        public Vector2 force = Vector2.one;
         public ForceMode2D forceMode2D;

        private void OnValidate()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _rigidbody2D.AddForce(force, forceMode2D);
            }
        }
        
        // with impulse velocity += impulse / mass when we add force
        // with force velocity += (force / mass) * physicStepDeltaTime when we add force
    }
}