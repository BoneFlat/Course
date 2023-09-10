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

        private void Awake()
        {
            Func<int, int, int> sum;
            sum = SumMethod;
            sum?.Invoke(10, 10);
        }

        public void MyFirstMethod()
        {
            
        }

        public void Main()
        {
            _myDelegate();
            
            //assign
            _myDelegate = Method1;
            
            //add 
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

        public int SumMethod(int a, int b)
        {
            return a + b;
        }
    }

   
}