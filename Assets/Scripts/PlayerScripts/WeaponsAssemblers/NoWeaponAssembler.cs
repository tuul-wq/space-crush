using PlayerScripts.Weapons;
using UnityEngine;

namespace PlayerScripts.WeaponsAssemblers
{
    public class NoWeaponAssembler : IWeaponAssembler
    {
        private float SHOT_OFFSET = 0.65f;
        
        public void AssembleWeapon(GameObject ship, Weapon weapon)
        {
            weapon.transform.SetParent(ship.transform);

            var transform = weapon.transform;
            transform.position = new Vector2(transform.position.x, transform.position.y + SHOT_OFFSET);
        }
    }
}