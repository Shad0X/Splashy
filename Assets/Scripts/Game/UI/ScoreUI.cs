using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{ 
    public class ScoreUI : MonoBehaviour
    {
        Text textUI;

        void Start()
        {
            textUI = GetComponent<Text>();

            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            scoreKeeper.OnScoreChanged += UpdateUI;
        }

        void UpdateUI(int value)
        {
            textUI.text = value.ToString();
        }

    }
}