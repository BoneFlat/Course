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
		[SerializeField] private GameObject _projectileBezier;
		[SerializeField] private GameObject _projectileV;
		public int bulletCount;
		public float bulletSpeed;
		public float arrowAngle;
		public float arrowLength;

		[SerializeField] private Transform _firePoint;


		[SerializeField] private float _rotateSpeedWhenCast;

		[SerializeField] private Transform _target;
		[SerializeField] private float     _row = 3;
		[SerializeField] private float     _oxAngle = 50;
		[SerializeField] private float     _distanceBetween = 1;

		private Quaternion _cacheMoveDirection;
		private Vector3    _cachedInput;
		
		public enum MyEnumeratedType 
		{
			Straight, Bezier, Vshaped
		}
		public MyEnumeratedType option;
		private void FixedUpdate()
		{
			_cachedInput.x = Input.GetAxisRaw("Horizontal");
			_cachedInput.z = Input.GetAxisRaw("Vertical");
			
			_cachedInput.Normalize();

			transform.position += _moveSpeed * Time.fixedDeltaTime * _cachedInput;

			if (_cachedInput != Vector3.zero)
			{
				Vector3 direction = _target.transform.position - transform.position;
				var targetRot = Quaternion.LookRotation(direction, Vector3.up);
				
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

		private void SpawnProjectile()
		{
			Vector3 direction;
			switch (option)
			{
				case MyEnumeratedType.Straight:
					GameObject bullet = Instantiate(_projectileV, _firePoint.position, _firePoint.rotation);
					direction = _target.position - _firePoint.position;
					Projectile _bullet = bullet.GetComponent<Projectile>();
					_bullet.Initialize(bulletSpeed, direction);
					break;
				
				case MyEnumeratedType.Bezier:
					GameObject projectile = Instantiate(_projectileBezier, _firePoint.position, _firePoint.rotation);
					projectile.GetComponent<ProjectileBezier>().SetBezierMovement(_target.position);
					break;
				
				case MyEnumeratedType.Vshaped:
					GameObject firstBullet = Instantiate(_projectileV, _firePoint.position, _firePoint.rotation);
					direction = _target.position - _firePoint.position;
					Projectile _projectile = firstBullet.GetComponent<Projectile>();
					_projectile.Initialize(bulletSpeed, direction);

					for (int i = 1; i < bulletCount; i++)
					{
						GameObject bullet1 = Instantiate(_projectileV, _firePoint.position, _firePoint.rotation);
						 _projectile = bullet1.GetComponent<Projectile>();
						
						Vector3 offset = Quaternion.Euler(0f, arrowAngle, 0f) * (transform.position - firstBullet.transform.position).normalized * 1 * i;
						offset.y = 0;
						bullet1.transform.position = firstBullet.transform.position + offset;
						_projectile.Initialize(bulletSpeed, direction);
						
						GameObject bullet2 = Instantiate(_projectileV, _firePoint.position, _firePoint.rotation);
						_projectile = bullet2.GetComponent<Projectile>();
						
						offset = Quaternion.Euler(0f, -arrowAngle, 0f) * (transform.position - firstBullet.transform.position).normalized * 1 * i;
						offset.y = 0;
						bullet2.transform.position = firstBullet.transform.position + offset;
						_projectile.Initialize(bulletSpeed, direction);
					}
					break;
			}
		}

		public void StopMove()
		{
			
		}

		public GameSound GameSound;
		public void Die()
		{
            GameSound.DisableSound();
            // GameEventHandler.OnPlayerDie?.Invoke();
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
		}
		
	}
}