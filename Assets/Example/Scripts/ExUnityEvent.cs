using System;
using UnityEngine;
using UnityEngine.Events;

namespace Example.Scripts
{
    [Serializable]
    public class StringEvent : UnityEvent<string>{
    
    }
    
    public class ExUnityEvent : MonoBehaviour
    {
        public UnityEvent baseEvent;
        public StringEvent stringEvent;

        private void Start()
        {
            baseEvent.AddListener(DoNothing);
            
            stringEvent.AddListener(DoWithString);
            stringEvent.Invoke("aaaaa");
        }

        public void DoNothing()
        {
            
        }

        public void DoSomething1(int a)
        {
            
        }

        public void DoSomething2(int a, int b)
        {
            
        }

        public void DoWithString(string input)
        {
            Debug.Log(input);
        }

        // https://www.jacksondunstan.com/articles/3335#comment-713798
        // https://stackoverflow.com/questions/2139812/what-is-a-callback
    }
}