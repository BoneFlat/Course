using Example.Scripts;
using UnityEngine;

namespace HomeWork._3
{
    public class RaycastMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private ExRigidbody.MovingMode _movingMode;
        [SerializeField] private float speed = 5;
        
        private void FixedUpdate()
        {
            var prevPos = transform.position;
            
            if (_rigidbody2D != null)
            {
                if (Input.GetAxisRaw("Horizontal") == 0)
                    return;
                
                switch (_movingMode)
                {
                    case ExRigidbody.MovingMode.Velocity:
                        _rigidbody2D.velocity =
                            Input.GetAxisRaw("Horizontal") * speed * Vector2.right ;
                        
                        break;

                    case ExRigidbody.MovingMode.MovePosition:
                        _rigidbody2D.MovePosition(_rigidbody2D.position +
                                                  Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime *
                                                  Vector2.right);

                        break;
                    case ExRigidbody.MovingMode.Transform:
                        _rigidbody2D.position += Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime *
                                                 Vector2.right;
                        break;
                }
            }

            if (Physics2D.Raycast(transform.position, Vector2.right, 2.5f) && Input.GetAxisRaw("Horizontal") >= 0||
                Physics2D.Raycast(transform.position, Vector2.left, 2.5f) && Input.GetAxisRaw("Horizontal") <= 0)
            {
                _rigidbody2D.position = prevPos;
                _rigidbody2D.velocity = Vector2.zero;
            }
        }
    }
}