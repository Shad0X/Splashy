using Game.Player;
using System;
using UnityEngine;

namespace Game.Level
{
    public class FinishZoneLogic : MonoBehaviour
    {

        
        GameObject ball;

        new ParticleSystem particleSystem;

        void Start()
        {
            ball = FindObjectOfType<BallBouncer>().gameObject;
            particleSystem = GetComponent<ParticleSystem>();
        }

        public event Action OnBallTouchedFinishZone;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetInstanceID() == ball.GetInstanceID())
            {
                OnBallTouchedFinishZone?.Invoke();
                particleSystem.Clear();
                particleSystem.Play();
            }
        }

        private void OnDestroy()
        {
            Delegate[] listeners = OnBallTouchedFinishZone.GetInvocationList();
            foreach (Delegate listener in listeners)
            {
                OnBallTouchedFinishZone -= (listener as Action);
            }
        }
    }
}