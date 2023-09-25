using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private Iflyable _flyable;
    // Start is called before the first frame update
    public void TryToFly()
    {
        _flyable.Fly();
    }

    public void SetFly(Iflyable _fly)
    {
        _flyable = _fly;
    }
}
