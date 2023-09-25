namespace Jackal
{
	using System;
	using UnityEngine;

	public class PhysicMessage : MonoBehaviour
	{
		private Vector2 direction;
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
			
		}
	}
	
}