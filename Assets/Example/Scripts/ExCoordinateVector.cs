namespace Example
{
	using UnityEngine;

	public class ExCoordinateVector : MonoBehaviour
	{
		[SerializeField] private float _lenght = 5;
		
		[Header("Project")]
		[SerializeField] private Transform _toProject;
		[SerializeField] private Transform _projectRoot;

		[Header("Reflect")]
		[SerializeField] private Transform _toReflect;
		[SerializeField] private Transform _reflectRoot;
		
		[Header("Cross")]
		[SerializeField] private Transform _crossA;
		[SerializeField] private Transform _crossB;
		
		[Header("Angle")]
		[SerializeField] private Transform _angleA;
		[SerializeField] private Transform _angleB;
		[SerializeField] private Vector3   _normal = Vector3.up;
		
		[SerializeField] private Transform _angleA2D;
		[SerializeField] private Transform _angleB2D;
		
		public                   float     resultAngle;
		public                   float     resultSignedAngle;
		
		public                   float     resultAngle2D;
		public                   float     resultSignedAngle2D;

		private void OnValidate()
		{
			resultAngle       = Vector3.Angle(_angleA.forward, _angleB.forward);
			resultSignedAngle = Vector3.SignedAngle(_angleA.forward, _angleB.forward, _normal);
			
			resultAngle2D       = Vector2.Angle(_angleA2D.up, _angleB2D.up);
			resultSignedAngle2D = Vector2.SignedAngle(_angleA2D.up, _angleB2D.up);
		}

		private void OnDrawGizmos()
		{
			#region Project

			var projectVector = Vector3.Project(_toProject.forward * _lenght, _projectRoot.forward);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(_toProject.position, _toProject.position + _toProject.forward * _lenght);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(_projectRoot.position, _projectRoot.position + _projectRoot.forward);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_toProject.position, _toProject.position + projectVector);

			#endregion

			#region Reflect

			var reflect = Vector3.Reflect(_toReflect.forward, _reflectRoot.up);
			
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_toReflect.position, _toReflect.position + _toReflect.forward * -_lenght);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(_reflectRoot.position, _reflectRoot.position + _reflectRoot.up);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_toReflect.position, _toReflect.position + reflect);

			#endregion

			#region Cross

			var cross = Vector3.Cross(_crossA.forward, _crossB.forward);
			
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_crossA.position, _crossA.position + _crossA.forward * -_lenght);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(_crossB.position, _crossB.position + _crossB.forward * -_lenght);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_crossA.position, _crossA.position + cross);

			#endregion

			#region Angle
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(_angleA.position, _angleA.position + _normal);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_angleA.position, _angleA.position + _angleA.forward);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(_angleB.position, _angleB.position + _angleB.forward);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_angleA2D.position, _angleA2D.position + _angleA2D.up);
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(_angleB2D.position, _angleB2D.position + _angleB2D.up);

			#endregion
		}

		public void Exam()
		{
			//Example 1: Rotate2DBy(this Vector3 root, float degree, float magnitude)
			//Example 2: Gizmos V shape with x angle, y distance
		}
	}
}