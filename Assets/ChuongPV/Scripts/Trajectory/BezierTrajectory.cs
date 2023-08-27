using UnityEngine;

namespace ChuongPV
{
    public class BezierTrajectory : ITrajectory
    {
        public Vector3 StartPos { get; set; }
        
        private Vector3 _high = Vector3.up * 10;
        private Vector3 _endPos;

        public void SetHigh(Vector3 high)
        {
            _high = _high;
        }
        
        public Vector3 UpdatePosition(float time)
        {
            if (time > 1)
            {
                return _endPos;
            }

            return  _endPos * Mathf.Pow(time, 2) + (StartPos + _endPos + _high * 2) * (time * (1 - time)) +
                    StartPos * Mathf.Pow(1 - time, 2);
        }

        public void SetTrajectory(Vector3 startPos, Vector3 endPos)
        {
            StartPos = startPos;
            _endPos = endPos;
        }
    }
}