using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwDelegate : MonoBehaviour
{
    public Action<int, int> action;

    private void Start()
    {
        action += Method1;
        action += Method2;
        action += (x, y) =>
        {
            Debug.Log(x * y);
        };
        action.Invoke(10, 20);
    }

    private void Method1(int x, int y)
    {
        Debug.Log(x + y);
    }

    private void Method2(int x, int y)
    {
        Debug.Log((x / y));
    }
}
