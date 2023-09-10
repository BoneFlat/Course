using System;
using UnityEngine;

public class HwDelegate : MonoBehaviour
{
    public int a = 3, b = 7;
    public delegate void TwoInteger(int x, int y);
    public TwoInteger OnLog;

    private void Start()
    {
        OnLog += LogSum;
        OnLog += LogDivide;
        OnLog += LogMulti;
        OnLog?.Invoke(a, b);
    }

    private void LogSum(int x, int y)
    {
        Debug.Log(x + y);
    }
    
    private void LogDivide(int x, int y)
    {
        Debug.Log(x / y);
    }
    
    private void LogMulti(int x, int y)
    {
        Debug.Log(x * y);
    }
}