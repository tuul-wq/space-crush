using UnityEngine;

namespace Utils
{
    public class MusicPlayer : MonoBehaviour
    {
        private void Awake()
        {
            SetupSingleton();
        }

        private void SetupSingleton()
        {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}