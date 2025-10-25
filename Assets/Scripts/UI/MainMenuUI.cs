using UnityEngine;
using UnityEngine.UI;

namespace DystopiaGirls.UI
{
    /// <summary>
    /// 메인 메뉴 UI 컨트롤러
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject metaUpgradePanel;
        [SerializeField] private GameObject settingsPanel;

        [Header("Buttons")]
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button metaUpgradeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            // 버튼 리스너 등록
            if (startGameButton != null)
                startGameButton.onClick.AddListener(OnStartGame);

            if (metaUpgradeButton != null)
                metaUpgradeButton.onClick.AddListener(OnMetaUpgrade);

            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettings);

            if (quitButton != null)
                quitButton.onClick.AddListener(OnQuit);

            ShowMainMenu();
        }

        private void OnStartGame()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartGame();
            }
        }

        private void OnMetaUpgrade()
        {
            ShowPanel(metaUpgradePanel);
        }

        private void OnSettings()
        {
            ShowPanel(settingsPanel);
        }

        private void OnQuit()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.QuitGame();
            }
        }

        public void ShowMainMenu()
        {
            ShowPanel(mainMenuPanel);
        }

        private void ShowPanel(GameObject panel)
        {
            // 모든 패널 비활성화
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (metaUpgradePanel != null) metaUpgradePanel.SetActive(false);
            if (settingsPanel != null) settingsPanel.SetActive(false);

            // 선택된 패널만 활성화
            if (panel != null) panel.SetActive(true);
        }

        private void OnDestroy()
        {
            // 버튼 리스너 해제
            if (startGameButton != null)
                startGameButton.onClick.RemoveListener(OnStartGame);

            if (metaUpgradeButton != null)
                metaUpgradeButton.onClick.RemoveListener(OnMetaUpgrade);

            if (settingsButton != null)
                settingsButton.onClick.RemoveListener(OnSettings);

            if (quitButton != null)
                quitButton.onClick.RemoveListener(OnQuit);
        }
    }
}
