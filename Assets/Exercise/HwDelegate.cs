using System;
using UnityEngine;

namespace Exercise
{
    public class HwDelegate : MonoBehaviour
    {
        private delegate int MyIntDelegate(int x, int y);
        private delegate float MyFloatDelegate(int x, int y);

        private void OnEnable()
        {
            Exercise();
        }

        [SerializeField] private int x, y;

        private void Exercise()
        {
            MyIntDelegate _myDelegate = Add;
            _myDelegate += Sub;

            MyFloatDelegate _myFloatDelegate = Divide;
            if (_myDelegate != null)
            {
                _myDelegate(x, y);
                _myDelegate.Invoke(x, y);
            }
            
            if (_myFloatDelegate != null)
            {
                _myFloatDelegate(x, y);
                _myFloatDelegate.Invoke(x, y);
            }
        }

        private int Add(int x, int y)
        {
            Debug.Log(x + y);
            return x + y;
        }
        
        private int Sub(int x, int y)
        {
            Debug.Log(x - y);
            return x - y;
        }
        
        private float Divide(int x, int y)
        {
            Debug.Log((float)x / y);
            return (float) x / y;
        }
        
    }
}