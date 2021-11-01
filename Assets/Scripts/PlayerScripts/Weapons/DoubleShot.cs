using UnityEngine;

namespace PlayerScripts.Weapons
{
    public class DoubleShot : Weapon
    {
        public override void Fire()
        {
            var offsetX = transform.GetChild(0).transform.localPosition.x;
            
            FireSingleWeapon(-offsetX);
            FireSingleWeapon(offsetX);
            
            PlayAttackSound();
            
            if (--Ammo == 0)
            {
                PlayOutOfAmmoSound();
                OnEmptyAmmo();
            }
        }

        private void FireSingleWeapon(float offsetX)
        {
            var shotPosition = new Vector2(transform.position.x + offsetX, transform.position.y);
            
            GameObject laserObject = Instantiate(projectile, shotPosition, Quaternion.identity);
            laserObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }
}