namespace Jackal
{
	using System;
	using UnityEngine;

	public class PhysicMessage : MonoBehaviour
	{
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
			Gizmos.color = Color.yellow;

			var hit = Physics2D.Raycast(transform.position, transform.up);
			Gizmos.DrawLine(transform.position, hit.point);
			var direction = hit.point - (Vector2)transform.position;
			var reflectDirection = Reflect(direction, hit.normal);
			Gizmos.DrawRay(hit.point, reflectDirection);
		}

		Vector2 Reflect(Vector2 inDirection, Vector2 normal)
		{
			var tangent = new Vector2(normal.y, -normal.x);
			var projection = Vector2.Dot(inDirection, tangent) / tangent.sqrMagnitude * tangent;
			var outDirection = projection * 2 - inDirection;
			return outDirection;
		}
	}
}