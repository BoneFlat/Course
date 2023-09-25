using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    
    // Start is called before the first frame update
    void Start()
    {
        SetFly(new Cannotfly());
        TryToFly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
