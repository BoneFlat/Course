using System;
using System.Collections;
using UnityEngine;

namespace Example
{
    public class CommonlyUsedMethod : MonoBehaviour
    {
        private void Start()
        {
            Count();
            CoCount();
        }

        public void Count()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"{Time.frameCount} count = {i}");
            }
        }

        public void CoCount()
        {
            StartCoroutine(IECount());
            
            IEnumerator IECount()
            {
                for (int i = 0; i < 10; i++)
                {
                    yield return null;
                    // yield return new WaitForEndOfFrame();
                    Debug.Log($"Co {Time.frameCount} count = {i}");
                }
            }
        }
    }
}