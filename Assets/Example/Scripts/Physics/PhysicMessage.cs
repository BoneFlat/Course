namespace Jackal
{
	using System;
	using UnityEngine;

	public class PhysicMessage : MonoBehaviour
	{
		public Vector2 direction;
		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log($"{gameObject.name} enter trigger with {other.name}");
		}
		
		private void OnTriggerExit2D(Collider2D other)
		{
			Debug.Log($"{gameObject.name} exit trigger with {other.name}");
		}

		private void OnDrawGizmos()
		{
			direction = direction.normalized;
			var hit = Physics2D.Raycast(transform.position,  direction, 10000);

			Vector3 reflDirection;
			Vector3 hitPosition;
			
			if (hit)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawRay(transform.position,  direction * hit.distance);
				
				
				Vector2 normal = hit.normal;
				Vector2 incident = direction;
				Vector2 reflected = incident - 2 * Vector2.Dot(incident, normal) * normal;
				// reflDirection = Vector3.Reflect(direction, hit.normal);
				hitPosition = hit.point;
				
				Gizmos.DrawRay(hitPosition, reflected * 100);
			}
			else
			{
				Gizmos.color = Color.green;
				Gizmos.DrawRay(transform.position, direction * 100);
			}
		}
	}
}