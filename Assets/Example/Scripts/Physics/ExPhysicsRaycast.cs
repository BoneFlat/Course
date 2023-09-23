namespace Jackal
{
	using System;
	using UnityEngine;

	public class ExPhysicsRaycast : MonoBehaviour
	{
		public float lenght = 10;

		private void OnDrawGizmos()
		{
			var hit = Physics2D.Raycast(transform.position + transform.up * 2,  transform.up, lenght);
			
			if (hit)
			{
				Debug.Log($"{hit.distance} {hit.collider.name}");
				Gizmos.color = Color.red;
				Gizmos.DrawRay(transform.position + transform.up * 2,  transform.up * hit.distance);

				Vector2 direction = (Vector2)transform.position - hit.point;
				float angle = Mathf.Atan2(direction.y, direction.x);
				float realAngle = 0;
				if (angle < 90)
					realAngle = 90 + (90 - angle);
				else
					realAngle = 90 - (angle - 90);
				Vector2 realDirection = Quaternion.Euler(0, 0, realAngle) * direction;
				Gizmos.DrawRay(hit.point, realDirection);
			}
			else
			{
				Gizmos.color = Color.green;
				Gizmos.DrawRay(transform.position + transform.up * 2, transform.up * 1000);
			}
		}
	}
}