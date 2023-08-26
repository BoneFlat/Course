using UnityEngine;

namespace Example
{
    public interface IFindTarget
    {
        public Transform FindTarget();

        public bool IsExistTarget();
    }
}