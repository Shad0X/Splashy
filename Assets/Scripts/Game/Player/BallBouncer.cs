using System;
using UnityEngine;
using DG.Tweening;

namespace Game.Player
{
    public class BallBouncer : MonoBehaviour
    {
        Vector3 topPosition;
        Vector3 bottomPosition;

        [SerializeField]
        float jumpHeight = 3f;

        void Start()
        {
            bottomPosition = transform.position;

            topPosition = gameObject.transform.position;
            topPosition.y += jumpHeight;

            GameLogic gameLogic = FindObjectOfType<GameLogic>();
            gameLogic.OnStartPlaying += MoveUp;
            gameLogic.OnReadyToPlay += PrepareForGame;
        }

        void PrepareForGame()
        {
            StopAllTweens();
        }

        void StopAllTweens()
        {
            tweenUp.Kill();
            tweenDown.Kill();
            tweenFall.Kill();
            tweenShake.Kill();
        }

        [SerializeField]
        float timeToReachTopPosition = 0.5f;

        [SerializeField]
        float timeToReachBottomPosition = 0.1f;

        public float GetJumpTime()
        {
            return timeToReachTopPosition + timeToReachBottomPosition;
        }

        Tween tweenUp;
        void MoveUp()
        {
            tweenUp = transform.DOMoveY(topPosition.y, timeToReachTopPosition).OnComplete(MoveDown);
        }

        Tween tweenDown;
        void MoveDown()
        {
            tweenDown = transform.DOMoveY(bottomPosition.y, timeToReachBottomPosition).OnComplete(CheckForPlatform);
        }

        public event Action OnPlatformTouched;

        void CheckForPlatform() //could use better name or refactoring..
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 10f))
            {
                OnPlatformTouched?.Invoke();
                MoveUp();
            }
            else
            {
                FallAndFail();
            }

        }

        Tween tweenShake;
        public event Action OnFailed;

        void FallAndFail()
        {
            tweenShake = transform.DOShakeScale(0.2f).OnComplete(Fall);
            OnFailed?.Invoke();
        }

        Tween tweenFall;
        void Fall()
        {
            tweenFall = transform.DOMoveY(transform.position.y - 10, 1f); //hardcoded values, not so great
        }

        void OnDestroy()
        {
            UnsubscribeListeners(OnFailed);
            UnsubscribeListeners(OnPlatformTouched);
        }

        void UnsubscribeListeners(Action action)
        {
            Delegate[] listeners = action.GetInvocationList();
            foreach (Delegate listener in listeners)
            {
                OnFailed -= (listener as Action);
            }
        }

    }
}