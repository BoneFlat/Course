using System;
using UnityEngine;

namespace HomeWork
{
    public class HwDelegate : MonoBehaviour
    {
        private delegate void HWDelegate(int x, int y);

        private HWDelegate _hwDelegate;

        private void Start()
        {
            _hwDelegate += (x, y) =>
            {
                Debug.Log($"x + y = {x + y}");
            };
            
            _hwDelegate += (x, y) =>
            {
                if (y == 0)
                {
                    Debug.Log("Something wrong");
                    return;
                }
                
                Debug.Log($"x/y = {(float)x / y}");
            };
            
            _hwDelegate += (x, y) =>
            {
                Debug.Log($"x * y = {x * y}");
            };
            
            _hwDelegate?.Invoke(3,10);
        }
    }
}