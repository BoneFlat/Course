using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierProjectile : Projectile
{
    public float bezierDuration = 1f;

    protected override void Update()
    {
        base.Update();
        if (startPoint != Vector3.zero)
        {
            timeElapsed += Time.deltaTime / bezierDuration;
            Vector3 bezierPosition = CalculateQuadraticBezierPoint(startPoint, controlPoint, endPoint, timeElapsed);
            transform.position = bezierPosition;

            if (timeElapsed >= 1f)
            {
                startPoint = Vector3.zero;
                Destroy(gameObject);
            }               
        }
    }

    public void SetBezierMovement(Vector3 start, Vector3 control, Vector3 end)
    {
        startPoint = start;
        controlPoint = control;
        endPoint = end;
        timeElapsed = 0f;
    }

    private Vector3 CalculateQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return uu * p0 + 2 * u * t * p1 + tt * p2;
    }

}
