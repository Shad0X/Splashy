using Game.Level;
using Game.Player;
using System;
using UnityEngine;

namespace Game
{
    public class GameLogic : MonoBehaviour
    {

        enum GameState
        {
            ReadyToPlay, Playing, CompletedLevel, Failed
        }

        GameState gameState;

        void Start()
        {
            gameState = GameState.ReadyToPlay;

            BallBouncer ballBouncer = FindObjectOfType<BallBouncer>();
            ballBouncer.OnFailed += FailGame;

            FinishZoneLogic levelCompletedLogic = FindObjectOfType<FinishZoneLogic>();
            levelCompletedLogic.OnBallTouchedFinishZone += LevelCompleted;
        }

        public event Action OnGameOver;

        void FailGame()
        {
            gameState = GameState.Failed;
            OnGameOver?.Invoke();
        }

        public event Action OnLevelCompleted;

        void LevelCompleted()
        {
            gameState = GameState.CompletedLevel;
            OnLevelCompleted?.Invoke();
        }

        void Update()
        {
            if (ReceivedPlayerInput())
            {
                if (gameState == GameState.CompletedLevel)
                {
                    PrepareNewLevel();
                } 
                else if (gameState == GameState.ReadyToPlay)
                {
                    StartPlaying();
                }

                if (gameState == GameState.Failed)
                {
                    PrepareToPlay();
                }

            }
        }

        public event Action OnStartPlaying;

        void StartPlaying()
        {
            gameState = GameState.Playing;
            OnStartPlaying?.Invoke();
        }

        public event Action OnReadyToPlay;

        void PrepareToPlay()
        {
            gameState = GameState.ReadyToPlay;
            OnReadyToPlay?.Invoke();
        }

        void PrepareNewLevel()
        {
            gameState = GameState.ReadyToPlay;
            OnReadyToPlay?.Invoke();
        }

        bool ReceivedPlayerInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }

            return false;
        }

        void OnDestroy()
        {
            CleanUpListeners(OnReadyToPlay);
            CleanUpListeners(OnStartPlaying);
            CleanUpListeners(OnGameOver);
            CleanUpListeners(OnLevelCompleted);
        }

        void CleanUpListeners(Action action)
        {
            Delegate[] listeners = action.GetInvocationList();
            foreach (Delegate listener in listeners)
            {
                action -= (listener as Action);
            }
        }
    }
}