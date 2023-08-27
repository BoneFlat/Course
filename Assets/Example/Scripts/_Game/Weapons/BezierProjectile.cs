using UnityEngine;

public class BezierProjectile : Projectile
{
    private float t = 0;
 
    private void OnEnable()
    {
        t = 0;
    }

    protected override void Move()
    {
        if (Target != null)
        {
            t += Time.fixedDeltaTime * MoveSpeed;
            t = Mathf.Clamp01(t);

            Vector3 newPos = CalculateQuadBezierPoint(StartPos, StartPos + Target.transform.position + Vector3.up * 10, Target.transform.position, t);
            transform.position = newPos;
            Debug.Log(transform.position);
        }
    }

    private Vector3 CalculateQuadBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}
