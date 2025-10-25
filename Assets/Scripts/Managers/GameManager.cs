using UnityEngine;
using UnityEngine.SceneManagement;

namespace DystopiaGirls
{
    /// <summary>
    /// 게임 전반을 관리하는 싱글톤 매니저
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        [SerializeField] private bool isGamePaused = false;
        [SerializeField] private bool isGameOver = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
            isGamePaused = false;
            isGameOver = false;
            SceneManager.LoadScene("GameScene");
        }

        public void PauseGame()
        {
            isGamePaused = true;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1f;
        }

        public void GameOver()
        {
            isGameOver = true;
            Time.timeScale = 0f;
            // TODO: Show game over UI
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public bool IsGamePaused => isGamePaused;
        public bool IsGameOver => isGameOver;
    }
}
