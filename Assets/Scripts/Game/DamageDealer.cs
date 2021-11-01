using UnityEngine;

namespace Game
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        public int GetDamage()
        {
            return damage;
        }

        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}