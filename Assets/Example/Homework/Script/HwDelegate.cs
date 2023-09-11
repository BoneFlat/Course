using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwDelegate : MonoBehaviour
{
    public delegate void Caculate(int x, int y);

    private void Sum(int x, int y) => Debug.Log((x + y));
    private void Multiple(int x, int y) => Debug.Log(x * y);
    private void Divide(int x, int y) => Debug.Log((float)x / y);

    private Caculate Logs;

    private void Awake()
    {
        Logs += Sum;
        Logs += Multiple;
        Logs += Divide;
    }

    private void Start()
    {
        Logs?.Invoke(2, 3);
    }
}
