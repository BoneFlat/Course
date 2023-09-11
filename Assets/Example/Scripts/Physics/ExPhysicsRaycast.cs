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
				
			}
			else
			{
				Gizmos.color = Color.green;
				Gizmos.DrawRay(transform.position + transform.up * 2, transform.up * 1000);
			}
		}
	}
}