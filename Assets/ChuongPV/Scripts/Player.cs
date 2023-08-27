using System.Linq;
using Example;

namespace ChuongPV
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

		[SerializeField] private Gun _gun;
		[SerializeField] private float     _row = 3;
		[SerializeField] private float     _oxAngle = 50;
		[SerializeField] private float     _distanceBetween = 1;

		private Quaternion _cacheMoveDirection;
		private Vector3    _cachedInput;

		protected override void Start()
		{
			base.Start();
			FindTarget();
		}

		private void FindTarget()
		{
			if (_gun.Target == null)
			{
				var enemis = GameObject.FindGameObjectsWithTag(Constants.ENEMY);
				
				_gun.Target = enemis.Length > 0 ? enemis[0].transform : null;
			}
		}

		private void FixedUpdate()
		{
			//move with button
			_cachedInput.x = Input.GetAxisRaw("Horizontal");
			_cachedInput.z = Input.GetAxisRaw("Vertical");
			
			_cachedInput.Normalize();

			transform.position += _moveSpeed * Time.fixedDeltaTime * _cachedInput;
		}
	}
}