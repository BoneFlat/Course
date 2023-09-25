using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iflyable
{
    void Fly();
}

public class Canfly : Iflyable
{
    public void Fly()
    {
        Debug.Log("flying");
    }
}

public class Cannotfly : Iflyable
{
    public void Fly()
    {
        Debug.Log("falling");
    }
}
