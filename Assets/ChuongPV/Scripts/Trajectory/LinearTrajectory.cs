using UnityEngine;

namespace ChuongPV
{
    public class LinearTrajectory : ITrajectory
    {
        private Vector3 _speedVector;

        public Vector3 StartPos { get; set; }

        public Vector3 UpdatePosition(float time)
        {
            return StartPos + _speedVector * time;
        }

        public void SetTrajectory(Vector3 startPos, Vector3 endPos)
        {
            StartPos = startPos;
            _speedVector = Vector3.Normalize(endPos - startPos);
        }
    }
}
