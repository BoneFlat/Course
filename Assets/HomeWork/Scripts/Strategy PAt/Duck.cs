using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Animal
{
    public Duck()
    {
        SetFly(new Canfly());

    }
    // Start is called before the first frame update
    void Start()
    {
        TryToFly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
