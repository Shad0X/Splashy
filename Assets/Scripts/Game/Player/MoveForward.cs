using Game.Level;
using UnityEngine;
using DG.Tweening;

namespace Game.Player
{
    public class MoveForward : MonoBehaviour
    {
        LevelSpawner platformSpawner;

        Vector3 startPosition;
        void Start()
        {
            platformSpawner = FindObjectOfType<LevelSpawner>();

            startPosition = transform.position;

            GameLogic gameLogic = FindObjectOfType<GameLogic>();
            gameLogic.OnReadyToPlay += PrepareForGame;
            gameLogic.OnStartPlaying += MoveToNextPlatform;
            gameLogic.OnGameOver += OnGameFailed;

            timeBetweenPlatforms = FindObjectOfType<BallBouncer>().GetJumpTime();
        }

        void PrepareForGame()
        {
            transform.position = startPosition;
            nextPlatformIndex = 0;
        }

        int nextPlatformIndex = 0;

        void MoveToNextPlatform()
        {
            if (nextPlatformIndex < platformSpawner.platformPositions.Count)
            {
                Move(platformSpawner.platformPositions[nextPlatformIndex]);
                nextPlatformIndex += 1;
            }
        }
        
        Tween tween;
        float timeBetweenPlatforms;

        void Move(Vector3 newPosition)
        {
            tween = transform.DOMoveZ(newPosition.z, timeBetweenPlatforms).SetEase(Ease.Linear).OnComplete(MoveToNextPlatform);
        }

        void OnGameFailed()
        {
            tween.Kill();
        }

    }
}