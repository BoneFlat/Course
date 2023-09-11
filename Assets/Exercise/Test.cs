using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : IGun
{
    public int dame { get; set; }
}

public interface IGun
{
    public int dame { get; set; }
}
public class Test : MonoBehaviour
{
    void Start()
    {
        A();
    }
    void A()
    {
        Gun gun = new Gun();
        gun.dame = 6;
        Debug.Log(gun.dame);

        IGun gun1 = gun;
        gun1.dame = 7;
        Debug.Log(gun.dame);

        var gun2 = (IGun)gun1;
        gun2.dame = 8;
        Debug.Log(gun.dame);

    }
}
