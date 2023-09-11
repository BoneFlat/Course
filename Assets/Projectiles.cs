using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Homework;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 targetPos;
    private Vector3 startPos;

    public void MoveToTarget(Vector3 target, TypeMove type)
    {
        startPos = transform.position;
        targetPos = target;
        switch (type)
        {
            case TypeMove.Linear:
                MoveLinear();
                break;
            case TypeMove.Bezier:
                MoveBezier();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void MoveLinear()
    {
        var dis = ObjectMoveBezier.CalculateDistance(targetPos, transform.position);
        
        var newDirect = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 3, 3);
        transform.rotation = Quaternion.LookRotation(newDirect);

        transform.DOMove(targetPos, dis / speed).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
    }

    private void MoveBezier()
    {
        ObjectMoveBezier.SetUpLine(startPos, startPos + targetPos + Vector3.up * 10,
            targetPos);
        float startTime = 0;
        float endTime = 1;
        DOTween.To(() => startTime, change =>
        {
            transform.position =
                ObjectMoveBezier.CalculateBezierPos(change);
        }, endTime, 5);
    }

}

public enum TypeMove
{
    Linear,
    Bezier
}
