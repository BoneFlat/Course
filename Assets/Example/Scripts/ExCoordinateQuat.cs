namespace Jackal
{
	using UnityEngine;

	public class ExCoordinateQuat : MonoBehaviour
	{
		[SerializeField] private Transform _root;
		[SerializeField] private Transform _rotateToPoint;

		[SerializeField] private Transform _gimbal;

		
		private void OnValidate()
		{
			_root.rotation = Quaternion.LookRotation(_rotateToPoint.position - _root.position, Vector3.right);
			
			Debug.Log(_gimbal.eulerAngles);
			Debug.Log(_gimbal.rotation);
			
			//exercise: make player always rotate to the red cube
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(_root.position - Vector3.up * 0.5f, Vector3.one);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_root.position, _rotateToPoint.position);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_root.position, _root.position + _root.forward * 0.2f);
		}
	}
}