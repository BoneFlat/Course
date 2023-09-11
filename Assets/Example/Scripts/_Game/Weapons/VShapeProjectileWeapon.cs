using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VShapeProjectileWeapon : Weapon
{
    public int ProjectilePerShot = 1;
    public float VAngle = 45;

    private const float StandardOffset = 0.1f;

    [SerializeField] protected SimpleObjectPooler ProjectilePooler;
    protected override void Initialize()
    {

    }

    protected override void WeaponUse()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int remapIndex = 1 - ProjectilePerShot;
            for (int i = 1; i <= ProjectilePerShot; i++)
            {
                if(i>1)
                    remapIndex += 2;

                float realAngle = (90 - VAngle) / 2 * (remapIndex < 0 ? -1 : 1); 

                Quaternion rotationByVAngle = Quaternion.Euler(0, realAngle, 0);

                Vector3 offset = rotationByVAngle * new Vector3(StandardOffset * remapIndex,0 , -Mathf.Abs(StandardOffset * remapIndex));

                Debug.Log(offset);

                SpawnProjetile(offset);
            }
        }
    }   

    private void SpawnProjetile(Vector3 offset)
    {
        Vector3 direction = new Vector3 (Target.transform.position.x - transform.position.x, 0 , Target.transform.position.z - transform.position.z);

        float angle = 90 - Mathf.Atan2(direction.z, direction.x)*Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 realPosition = (rotation * offset) + transform.position;

        GameObject _projectile = ProjectilePooler.GetPooledObject();
        _projectile.SetActive(true);
        _projectile.transform.position = realPosition;
        _projectile.GetComponent<Projectile>()?.SetDirection(direction);
        _projectile.GetComponent<Projectile>()?.SetStartPosition(realPosition);
        _projectile.GetComponent<Projectile>()?.SetTarget(Target);
        _projectile.GetComponent<Projectile>().SourceWeapon = this;
    }
}
