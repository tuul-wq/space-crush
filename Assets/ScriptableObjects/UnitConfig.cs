using UnityEngine;

namespace ScriptableObjects
{
    public abstract class UnitConfig : ScriptableObject
    {
        [Header(header: "SFX")]
        [SerializeField] private AudioClip deathSfx;
        [SerializeField] private AudioClip hitSfx;

        [Header(header: "Volume")]
        [SerializeField] [Range(0f, 1f)] private float deathSfxVolume = 0.75f;
        [SerializeField] [Range(0f, 1f)] private float hitSfxVolume = 0.75f;

        [Header(header: "VFX")]
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private GameObject hitVFX;

        private ParticleSystem _hitParticleSystem;
        private ParticleSystem _deathParticleSystem;
        
        private void Awake()
        {
            _hitParticleSystem = hitVFX.GetComponent<ParticleSystem>();
            _deathParticleSystem = deathVFX.GetComponent<ParticleSystem>();
        }

        public AudioClip GetDeathSfx()
        {
            return deathSfx;
        }
        
        public AudioClip GetHitSfx()
        {
            return hitSfx;
        }
        
        public float GetDeathSfxVolume()
        {
            return deathSfxVolume;
        }
        
        public float GetHitSfxVolume()
        {
            return hitSfxVolume;
        }

        public GameObject GetDeathVFX()
        {
            return deathVFX;
        }
        
        public float GetDeathVFXLifetime()
        {
            return _deathParticleSystem.main.startLifetimeMultiplier;
        }
        
        public GameObject GetHitVFX()
        {
            return hitVFX;
        }
        
        public float GetHitVFXLifetime()
        {
            return _hitParticleSystem.main.startLifetimeMultiplier;
        }
    }
}