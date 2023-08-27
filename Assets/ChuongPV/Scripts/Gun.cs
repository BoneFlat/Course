using UnityEngine;

namespace ChuongPV
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private Bullet _bullet;
        
        public Transform Target { get; set; }
        
        private void Update()
        {
            //rotate foward target
            var targetRot = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
            transform.rotation = targetRot;
			
            //shoot projectile
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }

        private void Fire()
        {
            var bullet = Instantiate(_bullet, _barrel.position, _barrel.rotation);
            bullet.SetStartPosEndPos(_barrel.position, Target.position);
            bullet.Fire();
        }
    }
}
