using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandleWeapon : MonoBehaviour
{
    [SerializeField] protected Weapon TargetWeapon;
    [SerializeField] protected GameObject Target;

    private GameObject _weapon;

    private void Start()
    {
        _weapon = Instantiate<GameObject>(TargetWeapon.gameObject, transform);
        _weapon.GetComponent<Weapon>().SetTarget(Target);
        _weapon.GetComponent<Weapon>().Owner = gameObject;
    }
}
