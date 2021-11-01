using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private void Start()
        {
            if (!player) return;
            
            player.GetComponent<Player>().DeathAction += ShowGameOver;
        }

        private int GetSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        private void ShowGameOver(float delay)
        {
            Invoke(nameof(LoadNextScene), delay);
        }
        
        public void LoadNextScene()
        {
            SceneManager.LoadScene(GetSceneIndex() + 1);
        }

        public void Restart()
        {
            SceneManager.LoadScene(1);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}