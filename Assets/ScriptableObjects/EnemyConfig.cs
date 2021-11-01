using ScriptableObjects.DynamicVars;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Enemy effects")]
    public class EnemyConfig : UnitConfig
    {
        [Header(header: "General")]
        [SerializeField] private int initialHealth = 100;
        
        [Header(header: "Shoot timer")]
        [SerializeField] private float shootTimerMin;
        [SerializeField] private float shootTimerMax;

        [Header(header: "Score")]
        [SerializeField] private IntValue scoreValue;
        
        [Header(header: "Audio")]
        [SerializeField] private AudioClip attackSfx;
        [SerializeField] [Range(0f, 1f)] private float attackSfxVolume = 0.75f;

        public int GetInitialHealth()
        {
            return initialHealth;
        }
        
        public float GetShootTimerRange()
        {
            return Random.Range(shootTimerMin, shootTimerMax);
        }

        public int UpdateScoreValue(int value)
        {
            return scoreValue.RuntimeValue += value;
        }
        
        public AudioClip GetAttackSfx()
        {
            return attackSfx;
        }
        
        public float GetAttackSfxVolume()
        {
            return attackSfxVolume;
        }
    }
}