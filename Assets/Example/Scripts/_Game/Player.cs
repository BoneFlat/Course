namespace Example
{
	using System;
	using UnityEngine;
	using UnityEngine.Serialization;

	[DefaultExecutionOrder(100000)]
	public class Player : Character, IFindTarget
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

		[SerializeField] private Transform _barrel;
		[SerializeField] private Transform _bulletPrefab;

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
				var targetRot = Quaternion.LookRotation(_cachedInput, Vector3.up);
				
				transform.rotation =
					Quaternion.RotateTowards(transform.rotation, targetRot, 
						_rotationSpeed * Time.fixedDeltaTime);
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				Fire();
			}
		}
		
		private void Fire()
		{
			if (IsExistTarget())
			{
				var target = FindTarget();
				var bullet = Instantiate(_bulletPrefab, _barrel.position, Quaternion.identity);
				var aiming = bullet.GetComponent<Bullet>() as IAiming;
				aiming.SetTarget(target);
			}
		}
		
		public Transform FindTarget()
		{
			var enemies = GameObject.FindGameObjectsWithTag(GameStaticVariables.ENEMY_TAG);
			return enemies[0].transform;
		}

		public bool IsExistTarget()
		{
			var enemies = GameObject.FindGameObjectsWithTag(GameStaticVariables.ENEMY_TAG);
			return (enemies.Length > 0);
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