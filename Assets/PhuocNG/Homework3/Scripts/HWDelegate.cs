using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWDelegate : MonoBehaviour
{
    public delegate void CalculateDelegate(float a, float b);

    public CalculateDelegate calculateDelegate;

    private void Start()
    {
        int x = 1, y = 2;
        calculateDelegate += (x, y) => { Debug.Log( x + y); };
        calculateDelegate += (x, y) => { Debug.Log( x * y); };
        calculateDelegate += (x, y) => { Debug.Log(x / y); };

        calculateDelegate(x, y);
    }
}
