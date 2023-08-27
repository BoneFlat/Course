using UnityEngine;

namespace ChuongPV
{
    public interface ITrajectory
    {
        public Vector3 StartPos { get; set; }
        public Vector3 UpdatePosition(float time);
        public void SetTrajectory(Vector3 startPos, Vector3 endPos);
    }
}
