using System;
using Game;
using ScriptableObjects;
using UnityEngine;

namespace EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [Header(header: "Config")] [SerializeField]
        protected EnemyConfig enemyConfig;

        [SerializeField] protected GameObject projectile;
        [SerializeField] protected float projectileSpeed = 12f;
        [SerializeField] protected float projectileYOffset = 0.65f;

        public event Action<Vector3> DeathAction;
        public int Score { get; set; }

        // Inner state
        private int _health;
        protected float AttackCountdown;
        protected bool IsDead;

        // Cache components
        private Animator _animator;
        private Collider2D _colliderComponent;
        private readonly int _alive = Animator.StringToHash("Alive");

        private void Start()
        {
            _colliderComponent = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();

            AttackCountdown = enemyConfig.GetShootTimerRange();
            _health = enemyConfig.GetInitialHealth();
        }

        private void Update()
        {
            if (IsDead) return;

            Shoot();
        }

        private void Shoot()
        {
            AttackCountdown -= Time.deltaTime;
            if (AttackCountdown > 0) return;

            LaunchProjectile();
            PlayAttackSound();

            AttackCountdown = enemyConfig.GetShootTimerRange();
        }

        protected GameObject LaunchProjectile(float distanceX = 0)
        {
            var laserStartPos = new Vector2(transform.position.x, transform.position.y - projectileYOffset);
            var enemyProjectile = Instantiate(projectile, laserStartPos, Quaternion.Euler(0f, 0f, 180f));
            enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(distanceX, -projectileSpeed);

            return enemyProjectile;
        }

        protected void PlayAttackSound()
        {
            var attackSound = enemyConfig.GetAttackSfx();
            if (!attackSound) return;

            AudioSource.PlayClipAtPoint(attackSound, Camera.main!.transform.position, enemyConfig.GetAttackSfxVolume());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) return;

            if (damageDealer.CompareTag("PlayerAttack"))
            {
                ApplyDamage(damageDealer.GetDamage());
                damageDealer.Hit();
            }
        }

        private void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            if (_health <= 0)
            {
                OnDeath();
            }
            else
            {
                PlayHitVFX();
                PlayHitSound();
            }
        }

        private void PlayHitSound()
        {
            var hitSound = enemyConfig.GetHitSfx();
            if (!hitSound) return;

            AudioSource.PlayClipAtPoint(hitSound, Camera.main!.transform.position, enemyConfig.GetHitSfxVolume());
        }

        private void PlayHitVFX()
        {
            var hitVFX = enemyConfig.GetHitVFX();
            if (!hitVFX) return;

            // TODO: should move with enemy
            var hitVFXInstance = Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFXInstance, enemyConfig.GetHitVFXLifetime());
        }

        private void OnDeath()
        {
            IsDead = true;
            enemyConfig.UpdateScoreValue(Score);
            NotifySubscribers();
            DisableCollision();
            PlayDeathVFX();
            PlayDeathSound();
            PlayDeathAnimation();
            Destroy(gameObject, enemyConfig.GetDeathVFXLifetime());
        }

        private void DisableCollision()
        {
            _colliderComponent.enabled = false;
        }

        private void PlayDeathVFX()
        {
            var deathVFX = enemyConfig.GetDeathVFX();
            if (!deathVFX) return;

            var deathVFXInstance = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(deathVFXInstance, enemyConfig.GetDeathVFXLifetime());
        }

        private void PlayDeathAnimation()
        {
            _animator.SetBool(_alive, false);
        }

        private void PlayDeathSound()
        {
            var deathSound = enemyConfig.GetDeathSfx();
            if (!deathSound) return;

            AudioSource.PlayClipAtPoint(deathSound, Camera.main!.transform.position, enemyConfig.GetDeathSfxVolume());
        }

        private void NotifySubscribers()
        {
            DeathAction?.Invoke(gameObject.transform.position);
            DeathAction = null;
        }
    }
}