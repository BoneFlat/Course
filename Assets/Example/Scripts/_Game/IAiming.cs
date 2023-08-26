using UnityEngine;

namespace Example
{
    public interface IAiming
    {
        public Transform target { get; set; }
        void SetTarget(Transform target);
    }
}