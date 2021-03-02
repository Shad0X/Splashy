using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameOverPrompt : MonoBehaviour
    {
        Text textComponent;

        void Start()
        {
            textComponent = GetComponent<Text>();
            textComponent.enabled = false;

            GameLogic gameLogic = FindObjectOfType<GameLogic>();
            gameLogic.OnReadyToPlay += HideText;
            gameLogic.OnGameOver += ShowText;
        }

        void ShowText()
        {
            textComponent.enabled = true;
        }

        void HideText()
        {
            textComponent.enabled = false;
        }
    }
}