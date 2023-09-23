using System;
using UnityEngine;

namespace Example.Scripts.Homework
{
    public class HwCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"Object {other.gameObject.name} collide with Object {gameObject.name}");
        }
    }
}