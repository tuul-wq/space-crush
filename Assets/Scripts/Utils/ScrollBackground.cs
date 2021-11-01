using UnityEngine;

namespace Utils
{
    public class ScrollBackground : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 0.05f;

        private MeshRenderer _meshRenderer;
        private readonly int _mainTex = Shader.PropertyToID("_MainTex");

        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            float offsetY = Time.time * scrollSpeed;
            _meshRenderer.sharedMaterial.SetTextureOffset(_mainTex, new Vector2(0, offsetY));
        }
    }
}