using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace ChuongPV
{
    public enum TrajectoryType
    {
        Linear,
        Bezier
    }

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;
        [SerializeField] private TrajectoryType _type;

        private ITrajectory _trajectory;
        private float _time;
        private bool _isFire;

        private void OnEnable()
        {
            _isFire = false;
            SetTrajectory();
        }

        private void SetTrajectory()
        {
            switch (_type)
            {
                case TrajectoryType.Bezier:
                    _trajectory = new BezierTrajectory();
                    break;
                case TrajectoryType.Linear:
                    _trajectory = new LinearTrajectory();
                    break;
            }
        }

        public void Fire()
        {
            _time = 0;
            _isFire = true;
        }

        public void SetStartPosEndPos(Vector3 startPos, Vector3 endPos)
        {
            _trajectory.SetTrajectory(startPos, endPos);
        }

        private void FixedUpdate()
        {
            if (_isFire)
            {
                transform.position = _trajectory.UpdatePosition(_time * _speed);
                _time += Time.fixedDeltaTime;
            }
        }
    }
}