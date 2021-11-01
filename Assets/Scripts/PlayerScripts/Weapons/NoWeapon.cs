using UnityEngine;

namespace PlayerScripts.Weapons
{
    public class NoWeapon : Weapon
    {
        public override void Fire()
        {
            GameObject laserObject = Instantiate(projectile, transform.position, Quaternion.identity);
            laserObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            
            PlayAttackSound();
        }
    }
}