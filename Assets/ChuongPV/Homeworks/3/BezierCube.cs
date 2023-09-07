using System;
using ChuongPV;
using Sirenix.OdinInspector;
using UnityEngine;

public class BezierCube : MonoBehaviour
{
	[SerializeField] private float _speed = 5;

	private BezierCubeTrajectory _trajectory = new BezierCubeTrajectory();

	private bool  _move;
	private float _time;

	private void Start()
	{
		Move();
		BezierCubeTrajectory.endAction += () => { _move = false; };
	}


	[Button]
	public void Move()
	{
		_move = true;
		_time = 0;
	}

	private void FixedUpdate()
	{
		if (_move)
		{
			var newPos = _trajectory.UpdatePosition(_time * _speed);

			transform.rotation = Quaternion.LookRotation(newPos - transform.position);
			transform.position = newPos;

			_time += Time.fixedDeltaTime;
		}
	}
}