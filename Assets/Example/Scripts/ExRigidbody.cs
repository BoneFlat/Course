using System;
using UnityEngine;

namespace Example.Scripts
{
    [DefaultExecutionOrder(1)]
    // https://docs.unity3d.com/Manual/CollidersOverview.html
    public class ExRigidbody : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private MovingMode _movingMode;
        [SerializeField] private float speed = 5;

        private void Start()
        {
            Application.targetFrameRate = 30;
            
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"On trigger {other.name}");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"On Collision {other.gameObject.name}");
        }

        private void FixedUpdate()
        {
            var a = MathfHelper.Rotate2DBy(Vector3.right, 30, 1);
            
            if (_rigidbody2D != null)
            {
                if (Input.GetAxisRaw("Horizontal") == 0)
                    return;
                
                switch (_movingMode)
                {
                    case MovingMode.AddForce:
                        _rigidbody2D.AddForce(Input.GetAxisRaw("Horizontal") * speed * Vector2.right);
                        break;
                    case MovingMode.Velocity:
                        _rigidbody2D.velocity =
                            Input.GetAxisRaw("Horizontal") * speed * Vector2.right ;
                        
                        break;

                    case MovingMode.MovePosition:
                        _rigidbody2D.MovePosition(_rigidbody2D.position +
                                                  Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime *
                                                  Vector2.right);

                        break;
                    case MovingMode.Transform:
                        _rigidbody2D.position += Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime *
                                                 Vector2.right;
                        break;
                }
            }
        }

        public enum MovingMode
        {
            AddForce,
            Velocity,
            MovePosition,
            Transform
        }
    }
}