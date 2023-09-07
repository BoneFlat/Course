using UnityEngine;

namespace ChuongPV
{
	using System;

	public class BezierCubeTrajectory : ITrajectory
	{
		public Vector3 StartPos { get => new Vector3(0, 0, 0); set { } }

		private Vector3 _midPos = new Vector3(5, 15, 0);
		private Vector3 _endPos = new Vector3(10, 0, 0);

		public static Action endAction;
		
		public Vector3 UpdatePosition(float time)
		{
			if (time > 1)
			{
				endAction?.Invoke();
				return _endPos;
			}

			return _endPos * Mathf.Pow(time, 2) + (StartPos + _endPos + _midPos) * (time * (1 - time)) +
			       StartPos * Mathf.Pow(1 - time, 2);
		}

		public void SetTrajectory(Vector3 startPos, Vector3 endPos) { }
	}
}