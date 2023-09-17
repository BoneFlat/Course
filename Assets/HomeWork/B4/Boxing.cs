using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Boxing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        const int iterations = 100000000;
        
        Stopwatch stopwatch = new Stopwatch();
        
        // Boxing Scenario
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            IWeapon weapon = new Gun();
            // Perform some operation with the weapon
        }
        stopwatch.Stop();
        
        Debug.Log("Boxing Scenario: " + stopwatch.ElapsedMilliseconds + " ms");
        
        stopwatch.Reset();
        
        // Non-Boxing Scenario
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            Gun gun = new Gun();
            // Perform some operation with the gun
        }
        stopwatch.Stop();
        
        Debug.Log("Non-Boxing Scenario: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    // Update is called once per frame
    public interface IWeapon { }
    public struct Gun : IWeapon { }
    
}
