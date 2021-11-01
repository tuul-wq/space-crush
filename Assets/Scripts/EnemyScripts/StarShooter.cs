using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class StarShooter : Enemy
    {
        [SerializeField] private float distanceX;
        
        private readonly List<GameObject> _projectiles = new List<GameObject>();
        
        private void Update()
        {
            if (IsDead) return;

            Shoot();
            RotateProjectiles();
        }

        private void OnDestroy()
        {
            _projectiles.Clear();
        }

        private void Shoot()
        {
            AttackCountdown -= Time.deltaTime;
            if (AttackCountdown > 0) return;

            DoubleShoot();

            AttackCountdown = enemyConfig.GetShootTimerRange();
        }
        
        private void DoubleShoot()
        {
            float[] twoSidesDistanceX = {-distanceX, distanceX}; 
            foreach (var distance in twoSidesDistanceX)
            {
                _projectiles.Add(LaunchProjectile(distance));
            }
            PlayAttackSound();
        }
        
        private void RotateProjectiles()
        {
            foreach (var proj in _projectiles)
            {
                if (proj)
                {
                    proj.transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime));
                }
            }
        }

    }
}