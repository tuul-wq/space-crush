using ScriptableObjects.DynamicVars;
using TMPro;
using UnityEngine;

namespace Game
{
    public class GameScore : MonoBehaviour
    {
        [SerializeField] private bool resetOnStart;
        [SerializeField] private IntValue scoreValue;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Start()
        {
            if (resetOnStart)
            {
                ResetScoreValue();
            }
        }

        private void Update()
        {
            SetScoreText();
        }

        private void SetScoreText()
        {
            var newValue = scoreValue.RuntimeValue.ToString();
            if (!scoreText.text.Equals(newValue))
            {
                scoreText.text = newValue;
            }
        }
        
        private void ResetScoreValue()
        {
            scoreValue.ResetRuntimeValue();
            SetScoreText();
        }
    }
}