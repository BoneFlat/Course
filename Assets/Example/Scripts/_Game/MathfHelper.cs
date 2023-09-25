namespace Example
{
	using UnityEngine;

	public static class MathfHelper
	{
		public static Vector3 Rotate2DBy(this Vector3 root, float degree, float magnitude)
		{
			var rootOxAngle = Vector2.SignedAngle(Vector2.right, root);
			var angle       = rootOxAngle + degree;

			var x = Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude;
			var y = Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude;

			return Vector3.up * y + Vector3.right * x;
		}
		
		public static Vector3 Rotate2DOnOxzBy(this Vector3 root, float degree, float magnitude)
		{
			var rootOxAngle = Vector3.SignedAngle(Vector3.forward, root, Vector3.up);
			var angle       = rootOxAngle + degree;

			var z = Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude;
			var x = Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude;

			return Vector3.forward * z + Vector3.right * x;
		}
		
		public static Vector3 QuadraticBezier(Vector3 startPosition, Vector3 heightPosition, Vector3 endPosition, float t)
		{
			var r = 1 - t;
			return r * (r * startPosition + t * heightPosition) + t * (r * heightPosition + t * endPosition);
		}	
		
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			var r = 1 - t;
			return Mathf.Pow(r,3) * p0 + 3 * r * r * t * p1 + 3 * r * t * t * p2 + Mathf.Pow(t, 3) * p3;
		}
	}
}