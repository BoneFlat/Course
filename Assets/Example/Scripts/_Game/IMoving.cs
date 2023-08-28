using UnityEngine;

namespace Example
{
    public interface IMoving
    {
        public float     Speed         { get; set; }
        public Vector3   Direction     { get; set; }

        public Vector3 GetNextMove();
    }
}