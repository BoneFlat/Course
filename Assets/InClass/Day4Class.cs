using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4Class : MonoBehaviour
{
    private void Start()
    {
        Method();
    }

    public void Method()
    {
        var gun = new Gun();
        gun.Dmg = 10;
        IWeapon weapon = gun;
        gun.Dmg = 20;
        var newGun = (IWeapon)weapon;
        gun.Dmg = 30;

        Debug.LogError("Gun: " + gun.Dmg);
        Debug.LogError("weapon: " + weapon.Dmg);
        Debug.LogError("newGun: " + newGun.Dmg);
    }
}

public interface IWeapon
{
    public int Dmg { get; set; }
}

public struct Gun : IWeapon
{
    public int Dmg { get ; set; }
}

// Ex4: StopWatch