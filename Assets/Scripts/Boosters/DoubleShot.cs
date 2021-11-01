using PlayerScripts;
using PlayerScripts.WeaponsAssemblers;
using UnityEngine;

namespace Boosters
{
    public class DoubleShot : Booster
    {
        [SerializeField] private GameObject doubleShootPrefab;
        [SerializeField] private int ammo;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayPickUpSound();
                ApplyEffect(other.gameObject.GetComponent<WeaponSystem>());
            }
        }

        private void ApplyEffect(WeaponSystem weaponSystem)
        {
            var weapon = Instantiate(doubleShootPrefab, weaponSystem.transform.position, Quaternion.identity);
            
            weaponSystem.SetWeaponAssembler(new DoubleShotAssembler());
            weaponSystem.SetWeapon(weapon, ammo);
            Destroy(gameObject, pickUpDelay);
        }
    }
}