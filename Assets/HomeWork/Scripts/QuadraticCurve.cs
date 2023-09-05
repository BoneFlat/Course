using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticCurve : MonoBehaviour
{
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;

    public Vector3 evaluate(float t)
    {
        /*Vector3 ac = Vector3.Lerp(A, Control, t);
        Vector3 cb = Vector3.Lerp(Control, B, t);
        return Vector3.Lerp(ac, cb, t);*/
        
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; 
        p += 2 * u * t * p1; 
        p += tt * p2;

        return p;
    }

    private void OnDrawGizmos()
    {
        if (p0 == null || p1 == null || p2 == null)
        {
            return;
        }

        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawWireSphere(evaluate(i / 20f), 0.1f);
        }
    }
}
