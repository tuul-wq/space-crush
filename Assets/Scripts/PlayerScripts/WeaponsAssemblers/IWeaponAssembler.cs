using PlayerScripts.Weapons;
using UnityEngine;

namespace PlayerScripts.WeaponsAssemblers
{
    public interface IWeaponAssembler
    {
        public void AssembleWeapon(GameObject ship, Weapon weapon);
    }
}