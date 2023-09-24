using UnityEngine;

public class TestRaycastCheckObstacles : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private MovingMode _movingMode;
    [SerializeField] private float speed = 5;

    private void FixedUpdate()
    {
        Vector3 prevPos = transform.position;

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
                        Input.GetAxisRaw("Horizontal") * speed * Vector2.right;

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

        if (Physics2D.Raycast(transform.position, Vector2.right, 2.5f) && Input.GetAxisRaw("Horizontal") >= 0 ||
            Physics2D.Raycast(transform.position, Vector2.left, 2.5f) && Input.GetAxisRaw("Horizontal") <= 0)
        {
            Debug.Log("detect obstacles!");
            _rigidbody2D.position = prevPos;
            _rigidbody2D.velocity = Vector2.zero;
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