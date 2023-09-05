using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwDelegate : MonoBehaviour
{
    private delegate void MyDelegate(int x, int y);

    public static void Add(int x, int y)
    {
        Debug.Log($"x + y result is {x + y}");
    }

    public static void Divide(int x, int y)
    {
        if (y != 0)
        {
            Debug.Log($"x / y result is {x / y}");
        }
        else
        {
            Debug.Log("Divide by zero is not allowed");
        }
    }

    public static void Multiply(int x, int y)
    {
        Debug.Log($"x * y result is {x * y}");
    }

    private void Start()
    {
        MyDelegate myDelegate;

        myDelegate = Add;

        myDelegate(2, 3);

        myDelegate(4, 5);

        myDelegate = Divide;

        myDelegate(2, 0);

        myDelegate(10, 2);

        myDelegate = Multiply;

        myDelegate(6, 2);
    }
}


