using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int seconds;

    private async void Start()
    {
        while (seconds > 0)
        {
            seconds--;
            Debug.Log("Countdown: " + seconds);
            await Task.Delay(1000);
        }
        
        Debug.Log("Finish Homework");
    }
}
