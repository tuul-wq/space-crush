using UnityEngine;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour
    {
        [SerializeField] private AudioClip pickUpSound;
        [SerializeField] protected float pickUpDelay = 0.05f;

        protected abstract void OnTriggerEnter2D(Collider2D other);

        protected void PlayPickUpSound()
        {
            AudioSource.PlayClipAtPoint(pickUpSound, Camera.main!.transform.position);
        }
    }
}