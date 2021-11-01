using PlayerScripts;
using UnityEngine;

namespace Boosters
{
    public class Heal : Booster
    {
        [SerializeField] private int healAmount;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayPickUpSound();
                ApplyEffect(other.gameObject.GetComponent<Player>());
            }
        }
        
        private void ApplyEffect(Player playerSystem)
        {
            playerSystem.Heal(healAmount);
            Destroy(gameObject, pickUpDelay);
        }
    }
}