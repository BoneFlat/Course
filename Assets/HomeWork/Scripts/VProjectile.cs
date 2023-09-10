using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VProjectile : Projectile
{
    [SerializeField] private int numProjectiles = 5;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float angle = 60f;
    [SerializeField] private float delta = 0.3f;
    [SerializeField] private GameObject projectilePrefab;
    protected override IEnumerator IEPerform()
    {
        GameObject[] projectiles = new GameObject[numProjectiles];
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i] = Instantiate(projectilePrefab);
        }

        var originPosition = transform.position;
        RecalculateProjectilesPositionAndRotation(originPosition);

        
        while ((originPosition - target.position).sqrMagnitude > 0.1f)
        {
            originPosition = Vector3.MoveTowards(originPosition, target.position, speed * Time.deltaTime);
            RecalculateProjectilesPositionAndRotation(originPosition);
            yield return null;
        }
        
        Destroy(gameObject);
        foreach (var projectile in projectiles)
        {
            Destroy(projectile);
        }
        
        void RecalculateProjectilesPositionAndRotation(Vector3 originPosition)
        {
            var direction = (target.position - originPosition).normalized;
            var tangent = Vector3.Cross(direction, Vector3.up);

            projectiles[0].transform.position = originPosition;
            for (int i = 1; i < projectiles.Length; i++)
            {
                float radius = (i + 1) / 2 * delta;
                float offsetY = Mathf.Cos(angle / 2 * Mathf.Deg2Rad) * radius;
                float offsetX = Mathf.Sin(angle / 2 * Mathf.Deg2Rad) * radius * (i % 2 == 0 ? 1 : -1);
                projectiles[i].transform.position = originPosition + tangent * offsetX + Vector3.up * offsetY;
            }

            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i].transform.forward = direction;
            }
        }
    }
    
}
