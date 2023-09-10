using System.Collections;
using UnityEngine;

public class BezierMovment : MonoBehaviour
{
    public Vector3 point1 = new Vector3(0, 0, 0);
    public Vector3 point2 = new Vector3(5, 15, 0);
    public Vector3 point3 = new Vector3(10, 0 ,0);
    public float duration = 1f;

    private void Start()
    {
        StartCoroutine(IEMove());
    }

    IEnumerator IEMove()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);

            var position = (1 - t) * (1 - t) * point1 + 2 * (1 - t) * t * point2 + t * t * point3;
            
            var direction = 2 * (1 - t) * (point2 - point1) + 2 * t * (point3 - point2);

            transform.position = position;
            transform.up = direction.normalized;
            
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}