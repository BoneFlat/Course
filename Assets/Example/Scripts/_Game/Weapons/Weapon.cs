using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]public GameObject Owner;
    [HideInInspector]protected GameObject Target;

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        WeaponUse();
    }

    protected virtual void Initialize()
    { }
    protected virtual void WeaponUse()
    { }

    public void SetTarget(GameObject target)
    {
        Debug.Log(target);
        Target = target;
        Debug.Log(Target);
    }
}
