using System;

namespace Homework
{

using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveBezier : MonoBehaviour
{
    [SerializeField] private List<Vector3> listPoint;
    [SerializeField] private bool moveLinear;
    [SerializeField] private Vector2 rangeMove;
    [SerializeField] private float rangeTime;

    private static Vector3 startPos;
    private static Vector3 bezierPos;
    private static Vector3 endPos;
    private float _deltaTime;

    private float time = 0;

    private void Awake()
    {
        SetUpLine(listPoint[0], listPoint[1], listPoint[2]);
    }

    public static void SetUpLine(Vector3 start, Vector3 bezier, Vector3 end)
    {
        startPos = start;
        bezierPos = bezier;
        endPos = end;
    }

    public static Vector3 CalculateBezierPos(float t)
    {
        var xPos = CalculatePoint(startPos.x, bezierPos.x, endPos.x);
        var yPos = CalculatePoint(startPos.y, bezierPos.y, endPos.y);
        var zPos = CalculatePoint(startPos.z, bezierPos.z, endPos.z);

        float CalculatePoint(float start, float bezier, float end)
        {
            return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * bezier + t * t * end;
        }

        return new Vector3(xPos, yPos, zPos);
    }

    public static float CalculateDistance(Vector3 start, Vector3 end)
    {
        var x = start.x - end.x;
        var y = start.y - end.y;
        var z = start.z - end.z;

        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    private void DoBezierMove()
    {
        if(time >= 1) return;
        _deltaTime = rangeTime;
        var nextPos = CalculateBezierPos(time + _deltaTime);
        while (CalculateDistance(nextPos, transform.position) > rangeMove.y || CalculateDistance(nextPos, transform.position) < rangeMove.x)
        { 
            if (CalculateDistance(nextPos, transform.position) > rangeMove.y) _deltaTime /= 2;
            else _deltaTime *= 1.5f;
            nextPos = CalculateBezierPos(time + _deltaTime > 1 ? 1 : time + _deltaTime);
        }

        time = time + _deltaTime > 1 ? 1 : time + _deltaTime;

        var newDirect = Vector3.RotateTowards(transform.forward, nextPos - transform.position, Mathf.PI, 3);
        
        transform.position = nextPos;
        transform.rotation = Quaternion.LookRotation(newDirect);
        
    }

    private void Update()
    {
        if(moveLinear) DoBezierMove();
        else
        {
            if(time >= 1) return;
            time += Time.deltaTime / 2;
            var nextPos = CalculateBezierPos(time);
            var newDirect = Vector3.RotateTowards(transform.forward, nextPos - transform.position, 3, 3);
            transform.position = nextPos;
            transform.rotation = Quaternion.LookRotation(newDirect);    
        }
    }
}

}

