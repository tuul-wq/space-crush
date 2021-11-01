using ScriptableObjects.DynamicVars;
using UnityEngine;

namespace Game
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private IntValue playerHealth;
        
        private RectTransform _rectangle;
        private float _oneHealthWidth;

        private void Start()
        {
            _rectangle = GetComponent<RectTransform>();
            _oneHealthWidth = _rectangle.sizeDelta.x;
            SetupHealthBarSize();
        }

        private void Update()
        {
            SetupHealthBarSize();
        }

        private void SetupHealthBarSize()
        {
            _rectangle.sizeDelta = new Vector2(_oneHealthWidth * playerHealth.RuntimeValue, _rectangle.sizeDelta.y);
        }
    }
}