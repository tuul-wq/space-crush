using PlayerScripts;
using UnityEngine;

namespace Boosters
{
    public class Shield : Booster
    {
        [SerializeField] private GameObject forceShield;
        [SerializeField] private int blockAmount = 3;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayPickUpSound();
                ApplyEffect(other.gameObject);
            }
        }
        
        private void ApplyEffect(GameObject player)
        {
            var currentForceShield = player.transform.GetComponentInChildren<ForceShield>();
            if (currentForceShield)
            {
                currentForceShield.Deactivate(false);
            }
            
            var shield = Instantiate(forceShield, player.transform.position, Quaternion.identity);
            shield.transform.SetParent(player.transform);
            shield.GetComponent<ForceShield>().SetBlockAmount(blockAmount);
            
            Destroy(gameObject, pickUpDelay);
        }
    }
}