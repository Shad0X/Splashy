using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayAgainPrompt : MonoBehaviour
    {
        Text uiText;
        void Start()
        {
            uiText = GetComponent<Text>();

            GameLogic gameLogic = FindObjectOfType<GameLogic>();
            gameLogic.OnReadyToPlay += HideText;
            gameLogic.OnLevelCompleted += ShowPrompt;
        }

        void ShowPrompt()
        {
            uiText.enabled = true;
        }

        void HideText()
        {
            uiText.enabled = false;
        }
    }
}