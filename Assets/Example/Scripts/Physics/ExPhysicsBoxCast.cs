using Example;
using UnityEngine;

public class ExPhysicsBoxCast : MonoBehaviour
{
    public float     angle;
    public Vector2   size;
    public Transform maxtrixTranform;

    private void OnDrawGizmos()
    {
        var hit = Physics2D.BoxCast(transform.position + transform.up * 2, size, angle, transform.up, 10);

        if (hit)
        {
            Debug.Log($"{hit.distance} {hit.collider.name}");
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + transform.up * 2,  transform.up * hit.distance);

            maxtrixTranform.localScale = size;
            maxtrixTranform.rotation   = Quaternion.LookRotation(Vector3.forward,Vector3.up.Rotate2DBy(angle, 1));
            maxtrixTranform.position   = transform.position + transform.up * 2 + transform.up * hit.distance;
            
            Gizmos.matrix = maxtrixTranform.localToWorldMatrix;
            // Gizmos.DrawWireCube(transform.position + transform.up * 2 + transform.up * hit.distance, size);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + transform.up * 2, transform.up * 1000);
        }
            
    }
}
