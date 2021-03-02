using Game.Player;
using System;
using UnityEngine;

namespace Game
{
    public class ScoreKeeper : MonoBehaviour
    {
        void Start()
        {
            BallBouncer bouncer = FindObjectOfType<BallBouncer>();
            bouncer.OnPlatformTouched += IncrementScore;
            bouncer.OnFailed += ResetScore;
        }

        int score;
        public event Action<int> OnScoreChanged;

        void IncrementScore()
        {
            score += 1;
            OnScoreChanged?.Invoke(score);
        }

        void ResetScore()
        {
            score = 0;
            OnScoreChanged?.Invoke(score);
        }

        void OnDestroy()
        {
            CleanUpListeners();
        }

        void CleanUpListeners()
        {
            Delegate[] listeners = OnScoreChanged.GetInvocationList();
            foreach (Delegate listener in listeners)
            {
                OnScoreChanged -= (listener as Action<int>);
            }
        }

    }
}