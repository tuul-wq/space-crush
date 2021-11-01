using System;
using Game;
using ScriptableObjects;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;

        public event Action<float> DeathAction;
        
        private Animator _animator;
        private Collider2D _colliderComponent;
        private readonly int _alive = Animator.StringToHash("Alive");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _colliderComponent = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) return;

            ApplyDamage(damageDealer.GetDamage());
            if (damageDealer.CompareTag("EnemyAttack"))
            {
                damageDealer.Hit();
            }
        }

        private void ApplyDamage(int damageValue)
        {
            playerConfig.ReduceHealth(damageValue);
            if (playerConfig.GetHealth() <= 0)
            {
                OnDeath();
            }
            else
            {
                PlayHitVFX();
            }
        }

        private void OnDeath()
        {
            var delay = playerConfig.GetDeathVFXLifetime();
            
            PlayDeathVFX();
            PlayDeathSound();
            PlayDeathAnimation();
            DisableCollision();
            NotifySubscribers(delay + 1f);
            Destroy(gameObject, delay);
        }

        private void DisableCollision()
        {
            _colliderComponent.enabled = false;
        }

        private void PlayHitVFX()
        {
            var hitVFX = playerConfig.GetHitVFX();
            if (!hitVFX) return;
            
            var hitVFXInstantiate = Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFXInstantiate, playerConfig.GetHitVFXLifetime());
        }

        private void PlayDeathVFX()
        {
            var deathVFX = playerConfig.GetDeathVFX();
            if (!deathVFX) return;
            
            var deathVFXInstantiate = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(deathVFXInstantiate, playerConfig.GetDeathVFXLifetime());
        }
        
        private void PlayDeathAnimation()
        {
            _animator.SetBool(_alive, false);   
        }
        
        private void PlayDeathSound()
        {
            var deathSound = playerConfig.GetDeathSfx();
            if (!deathSound) return;
            
            AudioSource.PlayClipAtPoint(deathSound, Camera.main!.transform.position, playerConfig.GetDeathSfxVolume());
        }
        
        private void NotifySubscribers(float delay)
        {
            DeathAction?.Invoke(delay);
            DeathAction = null;
        }

        public void Heal(int value)
        {
            if (playerConfig.IsFullHealth()) return;
            
            playerConfig.IncreaseHealth(value);
        }
    }
}