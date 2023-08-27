using System;
using UnityEngine;

namespace Example
{
    public class GameSound : MonoBehaviour
    {
        private void Start()
        {
            GameEventHandler.OnPlayerDie += DisableSound;
        }

        private void OnDestroy()
        {
            GameEventHandler.OnPlayerDie -= DisableSound;
        }

        public void DisableSound()
        {
            
        }
    }
}