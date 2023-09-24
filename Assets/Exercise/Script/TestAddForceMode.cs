using UnityEngine;

public class TestAddForceMode : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbBox;
    [SerializeField] private Vector2 force;
    [SerializeField] private ForceMode2D forceMode2D;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rbBox.AddForce(force, forceMode2D);
        }
    }

    /* 
    => Difference between ForceMode2D.Force and ForceMode2D.Impulse when we add force:
        - Force: The force is applied every frame, velocity = force / mass * deltaTime
        - Impulse: The force is applied instantly, and it affects the object's velocity as a one-time event: velocity = force / mass
    */
}
