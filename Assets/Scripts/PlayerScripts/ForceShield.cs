using System.Collections.Generic;
using Game;
using UnityEngine;

namespace PlayerScripts
{
    public class ForceShield : MonoBehaviour
    {
        [Header(header: "Audio")]
        [SerializeField] private AudioClip shieldDown;
        [SerializeField] private AudioClip shieldHit;
        
        [Header(header: "Config")]
        [SerializeField] private List<Sprite> sprites;
        
        private int _blockAmount;
        private float _initialBlockAmount;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateShieldSprite();
        }
        
        private void PlayShieldDown()
        {
            AudioSource.PlayClipAtPoint(shieldDown, Camera.main!.transform.position);
        }
        
        private void PlayHitSound()
        {
            AudioSource.PlayClipAtPoint(shieldHit, Camera.main!.transform.position, 0.5f);
        }

        public void SetBlockAmount(int blockAmount)
        {
            _blockAmount = blockAmount;
            _initialBlockAmount = blockAmount;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) return;

            ApplyDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
        
        private void ApplyDamage(int damageValue)
        {
            _blockAmount -= damageValue;
            if (_blockAmount <= 0)
            {
                Deactivate(true);
            }
            else
            {
                PlayHitSound();
                UpdateShieldSprite();
            }
        }

        private void UpdateShieldSprite()
        {
            if (_blockAmount > _initialBlockAmount * 2 / 3)
            {
                _spriteRenderer.sprite = sprites[2];
            }
            else if (_blockAmount > _initialBlockAmount / 3)
            {
                _spriteRenderer.sprite = sprites[1];
            }
            else
            {
                _spriteRenderer.sprite = sprites[0];
            }
        }

        public void Deactivate(bool playSound)
        {
            if (playSound)
            {
                PlayShieldDown();
            }
            Destroy(gameObject);
        }
    }
}