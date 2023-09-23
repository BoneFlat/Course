using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics2D : MonoBehaviour
{
    private const float FORCE_COEFFICIENT = 5;

    [Tooltip("Homework 3, task 3")]
    [SerializeField] private bool RaycastCheck;
    [SerializeField] private LayerMask ObstacleLayer;

    private Vector3 _startPosition;

    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ForceMovingIfCastToObstacle();
    }

    private void Initialize()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
    }

    public void AddForceByImpulse()
    {
        _rigidBody2D.AddForce(Vector2.right * FORCE_COEFFICIENT, ForceMode2D.Impulse);

        // Với Impulse, ngay lập tức sinh ra một lực tác động mạnh vào một vật trong một khoảnh khắc, đơn vị tính Unity là frame
        // Phù hợp với các loại tác động lực như vụ nổ, vụ va chạm truyền động lượng vì vật chỉ tác động lực 1 lần
    }    

    public void AddForceByForce()
    {
        _rigidBody2D.AddForce(Vector2.right * FORCE_COEFFICIENT, ForceMode2D.Force);

        //Với Force, lực tác động này được tác động trong một khoảng thời gian (Impulse/s), chính vì vậy, lực tác động này sẽ nhỏ hơn
        //Phù hợp với các loại tác động lực như động cơ kéo, đẩy. Vì gia tốc của vật được tăng từ từ nên vật chuyển động nhanh dần
        //Spam liên tục nút AddForce.Force trên màn hình thấy rõ hiệu quả này
    }    

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }    

    private void ForceMovingIfCastToObstacle()
    {
        if (RaycastCheck)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 2f, ObstacleLayer);

            if (hit.collider != null)
            {
                _rigidBody2D.velocity = Vector2.zero;
            }
        }
    }    
}
