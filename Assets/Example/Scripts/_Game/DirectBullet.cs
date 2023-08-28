using UnityEngine;

namespace Example
{
    public class DirectBullet : Bullet
    {
        public override Vector3 GetNextMove()
        {
            return transform.position + Direction * Speed * Time.fixedDeltaTime;
        }
    }
}