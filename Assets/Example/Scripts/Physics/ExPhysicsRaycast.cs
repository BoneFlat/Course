namespace Jackal
{
	using System;
	using UnityEngine;

	public class ExPhysicsRaycast : MonoBehaviour
	{
		public float lenght = 100;

		//private Vector2 direction;
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			
			//var hit = Physics2D.Raycast(transform.position + transform.up * 2,  transform.up, lenght);
			var hit = Physics2D.Raycast(transform.position, transform.up, 1000);

			
			if (hit)
			{
				Gizmos.DrawLine(transform.position, (hit.point ) );
				Debug.Log($"{hit.collider.name}");
				// Gizmos.color = Color.red;
				// Gizmos.DrawRay(transform.position + transform.up * 2,  transform.up * hit.distance);
				

				//var vec1 = hit.transform.position - transform.position;
				
				Gizmos.DrawLine(hit.point, hit.point + Vector2.Reflect(hit.point - (Vector2)transform.position, hit.normal)*lenght);
				//Debug.Log("-======================" + hit.point);

			}
			// else
			// {
			// 	Gizmos.color = Color.green;
			// 	Gizmos.DrawRay(transform.position + transform.up * 2, transform.up * 1000);
			// }
		}
	}
}