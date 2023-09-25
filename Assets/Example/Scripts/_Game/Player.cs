using HomeWork;

namespace Example
{
	using System;
	using UnityEngine;
	using UnityEngine.Serialization;

	[DefaultExecutionOrder(100000)]
	public class Player : Character
	{
		[FormerlySerializedAs("_maxSpeed")] [Header("Move Settings")] [SerializeField]
		private float _moveSpeed;

		[SerializeField] private float _rotationSpeed;

		[Header("Skill Settings")] [SerializeField]
		private float _lockTimeWhenCast;

		[SerializeField] private float _rotateSpeedWhenCast;

		[SerializeField] private Transform _target;
		[SerializeField] private float     _row = 3;
		[SerializeField] private float     _oxAngle = 50;
		[SerializeField] private float     _distanceBetween = 1;

		private Quaternion _cacheMoveDirection;
		private Vector3    _cachedInput;

		private void FixedUpdate()
		{
			_cachedInput.x = Input.GetAxisRaw("Horizontal");
			_cachedInput.z = Input.GetAxisRaw("Vertical");
			
			_cachedInput.Normalize();

			transform.position += _moveSpeed * Time.fixedDeltaTime * _cachedInput;

			if (_cachedInput != Vector3.zero)
			{
				var targetRot = Quaternion.LookRotation(_target.transform.position - transform.position, Vector3.up);
				
				transform.rotation =
					Quaternion.RotateTowards(transform.rotation, targetRot, 
						_rotationSpeed * Time.fixedDeltaTime);
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				SpawnProjectile();
			}
		}

		private void OnDrawGizmos()
		{
			var cross = Vector3.Cross(transform.position - _target.position, Vector3.back);
			cross.z = 0;
			var crossAngle = Vector2.SignedAngle(Vector2.right, cross);
			
			Vector3 _deltaPointLeft = Vector3.zero;
			Vector3 _deltaPointRight = Vector3.zero;

			Gizmos.color = Color.red;
			
			for (int i = 1; i < _row; i++)
			{
				_deltaPointRight.x = Mathf.Cos((_oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
				_deltaPointRight.y = Mathf.Sin((_oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;

				_deltaPointLeft.x = Mathf.Cos((180 + crossAngle - _oxAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
				_deltaPointLeft.y = Mathf.Sin((180 - _oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
				
				Gizmos.DrawSphere(transform.position + _deltaPointLeft, 0.1f);
				Gizmos.DrawSphere(transform.position + _deltaPointRight, 0.1f);
			}
			
			Gizmos.DrawSphere(transform.position, 0.1f);
			
			var direction = enemy.position - transform.position;
			var orginPos = transform.position;
			var vCenter = -direction;
			var vLineLeft = vCenter.Rotate2DOnOxzBy(_vAngle / 2, 1);
			var vLineRight = vCenter.Rotate2DOnOxzBy(-_vAngle / 2, 1);
			
			Gizmos.DrawLine(orginPos, enemy.position);
			Gizmos.DrawLine(orginPos, orginPos + vLineLeft * 10);
			Gizmos.DrawLine(orginPos, orginPos + vLineRight * 10);
		}

		[Header("Shoot settings")]
		public Transform enemy;
		public GameObject projectilePrefab;
		public float _bulletSpeed = 10;

		[Header("V shoot settings")] 
		public int _rowLevel = 2;
		public float _vAngle = 60;
		public float _deltaDistance = 1f;

		public void SpawnProjectile()
		{
			var type = UnityEngine.Random.Range(0, 100) > 50 ? TrajectoryType.Linear : TrajectoryType.Bezier;
			var direction = enemy.position - transform.position;
			var lifeTime = direction.magnitude / _bulletSpeed;
			
			direction.Normalize();

			if (UnityEngine.Random.Range(0, 100) > 50) // shoot single
			{
				var go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
				var projectile = go.GetComponent<Projectile>();
				projectile.Shoot(direction, lifeTime, _bulletSpeed, type);
			}
			else // shot V
			{
				var orginPos = transform.position;
				var vCenter = -direction;
				var vLineLeft = vCenter.Rotate2DOnOxzBy(_vAngle / 2, 1);
				var vLineRight = vCenter.Rotate2DOnOxzBy(-_vAngle / 2, 1);

				var go = Instantiate(projectilePrefab, orginPos, Quaternion.identity);
				var projectile = go.GetComponent<Projectile>();
				projectile.Shoot(direction, lifeTime, _bulletSpeed, type);
				
				for (int i = 1; i <= _rowLevel; i++)
				{
					var go1 = Instantiate(projectilePrefab, orginPos + vLineLeft * _deltaDistance * i, Quaternion.identity);
					var projectile1 = go1.GetComponent<Projectile>();
					projectile1.Shoot(direction, lifeTime, _bulletSpeed, type);
					
					var go2 = Instantiate(projectilePrefab, orginPos + vLineRight * _deltaDistance * i, Quaternion.identity);
					var projectile2 = go2.GetComponent<Projectile>();
					projectile2.Shoot(direction, lifeTime, _bulletSpeed, type);
				}
			}
			
		}
	}
}