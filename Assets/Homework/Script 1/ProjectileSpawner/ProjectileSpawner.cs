using Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject bezierProjectilePrefab;
    public GameObject normalProjectilePrefab;
    public KeyCode fireBezierProjectileKey = KeyCode.F;
    public KeyCode fireNormalProjectileKey = KeyCode.G;

    public Transform enemyPosition;

    private void Start()
    {
        if (bezierProjectilePrefab == null || normalProjectilePrefab == null)
        {
            Debug.LogError("Please attach your projectile prefab");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(fireBezierProjectileKey))
        {
            FireBezierProjectile();
        } else if (Input.GetKeyDown(fireNormalProjectileKey))
        {
            FireNormalProjectile();
        }
    }

    private void FireBezierProjectile()
    {
        GameObject newProjectile = Instantiate(bezierProjectilePrefab, transform.position, Quaternion.identity);

        // Set the movement type here
        Vector3 playerPosition = transform.position;

        // Example: Bezier movement
        Vector3 controlPoint = (playerPosition + enemyPosition.position) + Vector3.up * 10f;
        newProjectile.GetComponent<BezierProjectile>().SetBezierMovement(playerPosition, controlPoint, enemyPosition.position);
    }

    private void FireNormalProjectile()
    {
        GameObject newProjectile = Instantiate(normalProjectilePrefab, transform.position, Quaternion.identity);

        Vector3 enemyPos = enemyPosition.position; // Set the enemy's position here

        // Example: Straight movement
        newProjectile.GetComponent<NormalProjectile>().SetTarget(enemyPos);
    }
}
