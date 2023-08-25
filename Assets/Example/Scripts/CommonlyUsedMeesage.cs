using System;
using UnityEngine;

namespace Example
{
    public class CommonlyUsedMeesage : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private void OnValidate()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 5f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log("on focus");
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Debug.Log("on pause");
        }

        private void OnMouseDown()
        {
            Debug.Log("On Mouse down");
        }
        
        private void OnMouseDrag()
        {
            Debug.Log("On Mouse drag");
        }
    }
}