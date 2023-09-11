using System.Collections;

namespace Example
{
	using System;
	using UnityEngine;
	using UnityEngine.Serialization;

	[DefaultExecutionOrder(100000)]
	public class Player : Character
	{
		[Header("Move Settings")] [SerializeField]
		private float _moveSpeed;

		[SerializeField] private float _rotationSpeed;

		[Header("Skill Settings")] [SerializeField]
		private float _lockTimeWhenCast;

		[SerializeField] private float _rotateSpeedWhenCast;

		[SerializeField] private Projectiles projectile;
		[SerializeReference] public ISpawnProjectile SpawnMachine = new SpawnSingle();
		[SerializeField] private Transform _target;
		[SerializeField] private TypeMove typeMove;
		[SerializeField] private float     _row = 3;
		[SerializeField] private float     _oxAngle = 50;
		[SerializeField] private float     _distanceBetween = 1;
		[SerializeField] private int numsPrj;
		[SerializeField] private float anglePrj;
		[SerializeField] private float disBetweenProjectile;

		private Quaternion _cacheMoveDirection;
		private Vector3    _cachedInput;
		
		

		private void SpawnProjectile()
		{
			var project = Instantiate(projectile);
			project.gameObject.transform.position = transform.position;
			project.MoveToTarget(_target.position, typeMove);
		}

		private void SpawnNumsProjectiles()
		{
			var yOnX = Mathf.Tan(Mathf.Deg2Rad * (anglePrj / 2));
			
			for (int i = 1; i <= numsPrj; i++)
			{
				var prj = Instantiate(projectile);
				var new2DPos = Calculate2DNewPos(Mathf.Abs((i - (float)(numsPrj + 1) / 2)) * disBetweenProjectile);
				
				var newY = i > (float)(numsPrj + 1) / 2 ? new2DPos.x * yOnX * Mathf.Abs((i - (float)(numsPrj + 1) / 2)) * disBetweenProjectile : -new2DPos.x * yOnX *Mathf.Abs((i - (float)(numsPrj + 1) / 2)) * disBetweenProjectile;
				new2DPos.y = newY + transform.position.y;
				var targetNew = new Vector3(_target.position.x, new2DPos.y, _target.position.z);
				
				prj.gameObject.transform.position = new2DPos;
				prj.MoveToTarget(targetNew, TypeMove.Linear);
			}
		}

		private Vector3 Calculate2DNewPos(float length)
		{
			var targetPosition = _target.position;
			var transformPosition = transform.position;
			var t = (transformPosition.z - targetPosition.z) / (transformPosition.x - targetPosition.x);
			var xNew = length / Mathf.Sqrt(t * t + 1) + transformPosition.x;
			var zNew = (xNew - transformPosition.x) * t + transformPosition.z;
			return new Vector3(xNew, 0, zNew);
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space)) SpawnProjectile();
			if(Input.GetKeyDown(KeyCode.K)) SpawnNumsProjectiles();
		}

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