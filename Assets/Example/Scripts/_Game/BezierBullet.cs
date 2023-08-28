using System;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
    public class BezierBullet : Bullet
    {
        private List<Vector3> _points;
        private float         _tMove;
        private float         _stepMove;
        
        public override void SetTarget(Transform target)
        {
            base.SetTarget(target);
            
            var pos       = transform.position;
            var targetPos = Target.position;
            _points = new List<Vector3>() {pos, (pos + targetPos) + Vector3.up * 5, targetPos};

            _stepMove = 1 / ((targetPos - pos).magnitude / (Speed * Time.fixedDeltaTime));
            _tMove    = 0;
        }

        public override Vector3 GetNextMove()
        {
            _tMove += _stepMove;
            return GameServices.QuadraticBezierInterp(_points[0], _points[1], _points[2], _tMove);
        }
    }
}