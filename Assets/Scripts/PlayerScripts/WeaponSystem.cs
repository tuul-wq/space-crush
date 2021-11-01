using System.Collections;
using PlayerScripts.Weapons;
using PlayerScripts.WeaponsAssemblers;
using ScriptableObjects;
using UnityEngine;

namespace PlayerScripts
{
    public class WeaponSystem : MonoBehaviour
    {
        [Header(header: "Config")]
        [SerializeField] private GameObject defaultWeapon;
        [SerializeField] private PlayerConfig playerConfig;

        private Coroutine _fireCoroutine;
        private Weapon _activeWeapon;
        private IWeaponAssembler _assembler = new NoWeaponAssembler();
        // private GameObject _activeWeapon;

        private void Start()
        {
            SetupDefaultWeapon();
        }

        private void SetupDefaultWeapon()
        {
            if (_activeWeapon)
            {
                Destroy(_activeWeapon.gameObject);
            }
            
            var defaultWeaponInstance = Instantiate(defaultWeapon, transform.position, Quaternion.identity);
            SetWeapon(defaultWeaponInstance, 1);
        }

        private void Update()
        {
            if (playerConfig.IsPlayerDead()) return;

            Fire();
        }
        
        private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _fireCoroutine = StartCoroutine(FireContinuously());
            }

            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(_fireCoroutine);
            }
        }

        private IEnumerator FireContinuously()
        {
            while (true)
            {
                _activeWeapon.Fire();
                yield return new WaitForSeconds(_activeWeapon.GetProjectileFiringPeriod());
            }
        }

        public void SetWeaponAssembler(IWeaponAssembler assembler)
        {
            _assembler = assembler;
        }
        
        public void SetWeapon(GameObject newWeapon, int ammo)
        {
            if (_activeWeapon)
            {
                Destroy(_activeWeapon.gameObject);
            }
            
            _activeWeapon = newWeapon.GetComponent<Weapon>();
            _activeWeapon.SetAmmo(ammo);
            _activeWeapon.EmptyAmmo += SetupDefaultWeapon; 
            _assembler.AssembleWeapon(gameObject, _activeWeapon);
        }
    }
}