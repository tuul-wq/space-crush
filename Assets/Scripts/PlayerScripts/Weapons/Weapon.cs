using System;
using UnityEngine;

namespace PlayerScripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header(header: "Sound")]
        [SerializeField] private AudioClip attackSfx;
        [SerializeField] private AudioClip outOfAmmoSfx;
        [SerializeField] [Range(0f, 1f)] private float attackSfxVolume = 0.5f;

        [Header(header: "Projectile")]
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected float projectileSpeed = 20f;
        [SerializeField] protected float projectileFiringPeriod = 0.2f;

        public event Action EmptyAmmo;
        
        protected int Ammo;
        
        public abstract void Fire();

        public void SetAmmo(int ammoValue)
        {
            Ammo = ammoValue;
        }
            
        protected void PlayAttackSound()
        {
            AudioSource.PlayClipAtPoint(attackSfx, Camera.main!.transform.position, attackSfxVolume);
        }
        
        protected void PlayOutOfAmmoSound()
        {
            AudioSource.PlayClipAtPoint(outOfAmmoSfx, Camera.main!.transform.position);
        }

        public float GetProjectileFiringPeriod()
        {
            return projectileFiringPeriod;
        }

        protected virtual void OnEmptyAmmo()
        {
            EmptyAmmo?.Invoke();
            EmptyAmmo = null;
        }
    }
}