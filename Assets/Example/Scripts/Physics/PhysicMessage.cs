namespace Jackal
{
	using System;
	using UnityEngine;

	public class PhysicMessage : MonoBehaviour
	{
        private void OnDrawGizmos()
        {
			Gizmos.DrawLine(transform.position, transform.position + transform.up * -1 * 100);

			Gizmos.color = Color.green;
        }

        private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log($"{gameObject.name} enter trigger with {other.name}");
		}
		
		private void OnTriggerExit2D(Collider2D other)
		{
			Debug.Log($"{gameObject.name} exit trigger with {other.name}");
		}
	}
}