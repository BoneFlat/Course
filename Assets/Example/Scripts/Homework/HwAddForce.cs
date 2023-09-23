using Sirenix.OdinInspector;
using UnityEngine;

namespace Example.Scripts.Homework
{
    public class HwAddForce : MonoBehaviour
    {
        [SerializeField] private ForceMode2D _mode;
        [SerializeField] private Vector2 _force;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Button]
        private void AddForce()
        {
            _rigidbody.AddForce(_force * Time.fixedDeltaTime, _mode);
        }

        [Button]
        private void Stop()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}