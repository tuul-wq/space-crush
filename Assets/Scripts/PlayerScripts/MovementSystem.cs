using ScriptableObjects;
using UnityEngine;

namespace PlayerScripts
{
    public class MovementSystem : MonoBehaviour
    {
        [Header(header: "Config")]
        [SerializeField] private PlayerConfig playerConfig;
        
        [SerializeField] private float moveSpeed = 15f;
        [SerializeField] private float padding = 0.75f;
        
        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        
        private void Start()
        {
            SetUpMoveBoundaries();
        }
        
        private void Update()
        {
            if (playerConfig.IsPlayerDead()) return;

            Move();
        }
        
        private void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            var deltaY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            var position = transform.position;
            var clampX = Mathf.Clamp(position.x + deltaX, _xMin, _xMax);
            var clampY = Mathf.Clamp(position.y + deltaY, _yMin, _yMax);

            transform.position = new Vector2(clampX, clampY);
        }
        
        private void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            _xMin = gameCamera!.ViewportToWorldPoint(new Vector3(0, 0)).x + padding;
            _xMax = gameCamera!.ViewportToWorldPoint(new Vector3(1, 0)).x - padding;
            _yMin = gameCamera!.ViewportToWorldPoint(new Vector3(0, 0)).y + padding;
            _yMax = gameCamera!.ViewportToWorldPoint(new Vector3(1, 1)).y - padding;
        }
    }
}