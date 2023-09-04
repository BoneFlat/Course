using System;
using UnityEngine;

namespace Example.Scripts.Homework
{
    public class HwDelegate : MonoBehaviour
    {
        [SerializeField] private Vector2Int _pair;

        public delegate void Calculate(int x, int y);

        private Calculate _calculate;

        private void Start()
        {
            _calculate += CalSum;
            _calculate += CalMul;
            _calculate += CalDiv;
        }

        private void CalSum(int x, int y)
        {
            Debug.Log($"Sum: {x + y}");
        }

        private void CalDiv(int x, int y)
        {
            if (y == 0)
            {
                Debug.Log($"y is invalid");    
            }
            else
            {
                Debug.Log($"Div: {1f * x / y}");   
            }
        }

        private void CalMul(int x, int y)
        {
            Debug.Log($"Mul: {x * y}");
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                _calculate?.Invoke(_pair.x, _pair.y);
            }
        }
    }
}