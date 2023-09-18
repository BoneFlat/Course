using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Class : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        //Physics2D.auoSimulation
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.Simulate(Time.deltaTime);
    }
}
