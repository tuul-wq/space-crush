using ScriptableObjects.DynamicVars;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Player effects")]
    public class PlayerConfig : UnitConfig
    {
        [SerializeField] private IntValue health;

        public void ReduceHealth(int value)
        {
            health.RuntimeValue -= value;
        }
        
        public void IncreaseHealth(int value)
        {
            health.RuntimeValue += value;
        }
        
        public bool IsFullHealth()
        {
            return health.GetInitValue() == health.RuntimeValue;
        }

        public int GetHealth()
        {
            return health.RuntimeValue;
        }

        public bool IsPlayerDead()
        {
            return health.RuntimeValue == 0;
        }
    }
}