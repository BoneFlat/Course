namespace Jackal
{
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
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 30);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, hit.point);
			if (hit)
			{
				// Cach 1:
				//Gizmos.DrawLine(hit.point, hit.point + Vector2.Reflect((hit.point - (Vector2)transform.position), hit.normal));

				// Cach 2:
				Vector2 newPos = new Vector3(hit.point.x * 2 - transform.position.x, transform.position.y);
				Gizmos.DrawLine(hit.point, newPos);
			}
		}
	}
}