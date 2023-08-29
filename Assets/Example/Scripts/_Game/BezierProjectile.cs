using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierProjectile : Projectile
{
    [SerializeField] private float duration;
    
    protected override IEnumerator IEPerform()
    {
        float elapsed = 0f;
        var point1 = transform.position;
        var point2 = (transform.position + target.position) / 2 + Vector3.up * 10;
        var point3 = target.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            var position = (1 - t) * (1 - t) * point1 + 2 * (1 - t) * t * point2 + t * t * point3;
            var direction = 2 * (1 - t) * (point2 - point1) + 2 * t * (point3 - point2);
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
