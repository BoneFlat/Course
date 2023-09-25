using System;
using UnityEngine;

namespace HomeWork._3
{
    public class TestCollisionCase : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{gameObject.name} enter collision with {other.gameObject.name}");
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            Debug.Log($"{gameObject.name} exit collision with {other.gameObject.name}");
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            Debug.Log($"{gameObject.name} stay collision with {other.gameObject.name}");
        }
    }
}