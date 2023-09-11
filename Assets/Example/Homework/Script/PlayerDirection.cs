using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private Transform[] targetPosition;
    [SerializeField] private float speedRotation;
    private int _numbersTarget;
    private int _targetIndex;

    private Action<int, int> sum;

    private Func<int, int> add;

    private delegate TResult x<in T, out TResult>(T arg);

    private x<int, long> xx;
    private x<int, double> xxx;

    private long Add(int input)
    {
        return input++;
    }


    private void Awake()
    {
        xx += Add;
        _numbersTarget = targetPosition.Length;
        _targetIndex = Random.Range(0, _numbersTarget);
        Debug.Log(_targetIndex);
    }

    private Quaternion RotateDirectByQuaternion()
    {
        return Quaternion.RotateTowards(transform.rotation, targetPosition[_targetIndex].rotation, speedRotation * Time.deltaTime);
    }

    private void RotateDirectByVector3()
    {
        var targetDirection = targetPosition[_targetIndex].position - transform.position;
        
        var newDirection = Vector3.RotateTowards(transform.up, targetDirection, speedRotation / 180 * Mathf.PI, 1);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }

    private void Update()
    {
        // transform.rotation = RotateDirectByQuaternion(); 
        RotateDirectByVector3();
    }
}
