using PlayerScripts.Weapons;
using UnityEngine;

namespace PlayerScripts.WeaponsAssemblers
{
    public class DoubleShotAssembler : IWeaponAssembler
    {
        public void AssembleWeapon(GameObject ship, Weapon weapon)
        {
            weapon.transform.SetParent(ship.transform);
            
            // Add more assemble functions if new player ships added 
        }
    }
}