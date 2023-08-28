using UnityEngine;

namespace Example
{
    public interface IAiming
    {
        public Transform Target { get; set; }

        public void SetTarget(Transform target);
    }
}