using System;
using UnityEngine;

namespace Example
{
    public class GameEventHandler : MonoBehaviour
    {
        // public delegate void Action();
        
        public static Action OnPlayerDie;
        public static Func<int> OnPlayerTakeDamage;
        public static Action OnEnemyDie;

        public static Action<int, LoadGameProgress> OnLoadGame;
    }

    public enum LoadGameProgress
    {
        OnDownloadData,
        OnGameReady
    }
}