using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DystopiaGirls.UI
{
    /// <summary>
    /// 게임 플레이 중 HUD (체력바, 경험치바, 스킬 표시 등)
    /// </summary>
    public class GameHUD : MonoBehaviour
    {
        [Header("Health Bar")]
        [SerializeField] private Image healthFillImage;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Experience Bar")]
        [SerializeField] private Image experienceFillImage;
        [SerializeField] private TextMeshProUGUI levelText;

        [Header("Skills Display")]
        [SerializeField] private Transform skillsContainer;
        [SerializeField] private GameObject skillIconPrefab;

        [Header("Player Reference")]
        [SerializeField] private GameObject player;

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (player != null)
            {
                SetupPlayerEvents();
            }
        }

        private void SetupPlayerEvents()
        {
            // 체력 이벤트 구독
            var playerStats = player.GetComponent<Player.PlayerStats>();
            if (playerStats != null)
            {
                playerStats.OnHealthChanged.AddListener(UpdateHealthBar);
                UpdateHealthBar(playerStats.CurrentHealth, playerStats.MaxHealth);
            }

            // 경험치 이벤트 구독
            var expSystem = player.GetComponent<Systems.ExperienceSystem>();
            if (expSystem != null)
            {
                expSystem.OnExperienceChanged.AddListener(UpdateExperienceBar);
                expSystem.OnLevelUp.AddListener(UpdateLevel);
                UpdateExperienceBar(expSystem.CurrentExperience, expSystem.ExperienceRequired);
                UpdateLevel(expSystem.CurrentLevel);
            }
        }

        private void UpdateHealthBar(float current, float max)
        {
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = current / max;
            }

            if (healthText != null)
            {
                healthText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
            }
        }

        private void UpdateExperienceBar(float current, float required)
        {
            if (experienceFillImage != null)
            {
                experienceFillImage.fillAmount = current / required;
            }
        }

        private void UpdateLevel(int level)
        {
            if (levelText != null)
            {
                levelText.text = $"Lv. {level}";
            }
        }

        public void AddSkillIcon(Sprite icon)
        {
            if (skillsContainer == null || skillIconPrefab == null)
                return;

            GameObject skillIcon = Instantiate(skillIconPrefab, skillsContainer);
            var image = skillIcon.GetComponent<Image>();
            if (image != null)
            {
                image.sprite = icon;
            }
        }
    }
}
