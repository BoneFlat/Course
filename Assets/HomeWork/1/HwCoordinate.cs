using Example;
using UnityEngine;

namespace HomeWork
{
    public class HwCoordinate : MonoBehaviour
    {
        [SerializeField] private Transform sprite;
        [SerializeField] private Transform[] targets;

        private int targetIndex;

        private Vector3 GetNextTargetPos()
        {
            return targets[GetIndex()].position;
        }

        private int GetIndex()
        {
            if (targetIndex > targets.Length - 1)
                targetIndex = 0;

            return targetIndex++;
        }

        private void RotateNext()
        {
            sprite.rotation = Quaternion.LookRotation(GetNextTargetPos() - sprite.position, Vector3.back);
        }

        private void RotateNextByAngle()
        {
            sprite.forward = Vector3.up;

            var direction = GetNextTargetPos() - sprite.position;
            var deltaAngle = Vector2.SignedAngle(Vector3.up, direction);
            
            sprite.forward = Rotate2DBy(Vector3.up, deltaAngle, 1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RotateNext();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                RotateNextByAngle();
            }
            
            if (Input.GetKey(KeyCode.Alpha3))
            {
                RunBezier();
            }
        }
        
        public Vector3 Rotate2DBy(Vector3 root, float degree, float magnitude)
        {
            var rootOxAngle = Vector2.SignedAngle(Vector2.right, root);
            var angle       = rootOxAngle + degree;

            var x = Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude;
            var y = Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude;

            return Vector3.up * y + Vector3.right * x;
        }

        public Transform _bezierRect;

        public Vector3 p0 = Vector3.zero;
        public Vector3 p1 = new Vector3(5,15,0);
        public Vector3 p2 = new Vector3(10,0,0);

        private float t;

        public void RunBezier()
        {
            t += Time.deltaTime;

            var prevPos = _bezierRect.position;
            var nextPos = MathfHelper.QuadraticBezier(p0, p1, p2, t);
            _bezierRect.position = nextPos;
            
            var inNormal = Vector3.Cross(nextPos - prevPos, Vector3.forward);
            _bezierRect.rotation = Quaternion.LookRotation(inNormal, Vector3.forward);

            if (t > 1)
                t = 0;
        }
    }
}