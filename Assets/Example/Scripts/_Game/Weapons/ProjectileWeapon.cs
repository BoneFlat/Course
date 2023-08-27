using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] protected SimpleObjectPooler ProjectilePooler;
    protected override void Initialize()
    {
        
    }

    protected override void WeaponUse()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject _projectile = ProjectilePooler.GetPooledObject();
            _projectile.SetActive(true);
            _projectile.transform.position = transform.position;
            _projectile.GetComponent<Projectile>()?.SetDirection(Target.transform.position - transform.position);
            _projectile.GetComponent<Projectile>()?.SetStartPosition(transform.position);
            _projectile.GetComponent<Projectile>()?.SetTarget(Target);
            _projectile.GetComponent<Projectile>().SourceWeapon = this;
        }    
    }
}
