using UnityEngine;

public class BezierMovement2D : MonoBehaviour
{
    public Transform[] controlPoints;
    public float speed = 1f;

    private int currentPointIndex = 0;
    [SerializeField] private float t = 0f;
    private Vector2 position;
    

    private void Start()
    {
        position = controlPoints[0].position;
    }

    private void Update()
    {
        Vector2 newPosition = CalculateBezierPoint(t);
        position += newPosition * Time.deltaTime * speed;
        transform.position = position;

        Vector2 forwardDirection = CalculateBezierTangent(t);
        transform.up = forwardDirection;

        t += Time.deltaTime * speed;

        if (t >= 1.5f)
        {
            currentPointIndex = 0;
            position = controlPoints[0].position;
            t = 0f;
        }
    }

    private Vector2 CalculateBezierPoint(float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 p = uu * controlPoints[currentPointIndex].position;
        p += 2f * u * t * (Vector2)controlPoints[currentPointIndex + 1].position;
        p += tt * (Vector2)controlPoints[currentPointIndex + 2].position;


        return p;
    }

    private Vector2 CalculateBezierTangent(float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 tangent = -2f * u * controlPoints[currentPointIndex].position;
        tangent += 2f * u * (Vector2)controlPoints[currentPointIndex + 1].position;
        tangent += 2f * t * (Vector2)controlPoints[currentPointIndex + 1].position;
        tangent += 2f * t * (Vector2)controlPoints[currentPointIndex + 2].position;

        tangent.Normalize();

        return tangent;
    }
}