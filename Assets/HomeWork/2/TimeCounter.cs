using System;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeWork
{
    public class TimeCounter : MonoBehaviour
    {
        public float time = 60f;

        private float _timeElapsed;
        private async void Start()
        {
            for (int i = 0; i < time; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _timeElapsed += 1;
                Debug.Log($"Time = {_timeElapsed}");
            }
            
            Debug.Log("Finish Homework");
        }
    }
}