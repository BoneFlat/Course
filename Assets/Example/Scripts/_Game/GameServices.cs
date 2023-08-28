using UnityEngine;

namespace Example
{
    public class GameServices
    {
        public static Vector3 LinearLerp(Vector3 p1, Vector3 p2, float t)
        {
            float x = Mathf.Lerp(p1.x, p2.x, t);
            float y = Mathf.Lerp(p1.y, p2.y, t);
            float z = Mathf.Lerp(p1.z, p2.z, t);

            return new Vector3(x, y, z);
        }

        public static Vector3 QuadraticBezierInterp(Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            var a = LinearLerp(p1, p2, t);
            var b = LinearLerp(p2, p3, t);

            return LinearLerp(a, b, t);
        }
    }
}