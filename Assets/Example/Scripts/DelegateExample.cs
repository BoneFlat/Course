using System;
using UnityEngine;

namespace Example.Scripts
{
    public class DelegateExample : MonoBehaviour
    {
        delegate void MyDelegate();
        MyDelegate attack;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 1
                if (attack != null)
                {
                    attack();
                }

                //2 short hand
                attack?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                attack = PrimaryAttack;
                attack += PrimaryAttack;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                attack = SecondaryAttack;
            }
        }
        void PrimaryAttack()
        {
            Debug.Log("Gun Attack");
        }
        void SecondaryAttack()
        {
            Debug.Log("Knife Attack");
        }

        private void OnDestroy()
        {
            attack -= PrimaryAttack;
        }


        public delegate void MyFirstDelegate();

        private MyFirstDelegate _myDelegate;

        public void MyFirstMethod()
        {

        }

        public void Main()
        {
            _myDelegate();

            //add 
            _myDelegate = Method1;
            _myDelegate += Method2;

            //remove ref
            _myDelegate -= Method2;

            // 1
            if (_myDelegate != null)
                _myDelegate();

            // 2. short hand
            _myDelegate?.Invoke();
        }

        public void Method1()
        {
            Debug.Log("method 1");
        }

        public void Method2()
        {
            Debug.Log("method 2");
        }

        Func<int, int, int> sum;

        int CalSum(int a, int b)
        {
            return a + b;
        }

        public void Test()
        {
            sum += CalSum;
            var result = sum.Invoke(3, 4);
        }
    }
}