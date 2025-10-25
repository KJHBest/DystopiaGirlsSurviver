using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace DystopiaGirls.UI
{
    /// <summary>
    /// 레벨업 시 스킬 선택 UI
    /// </summary>
    public class SkillUpgradeUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private SkillCard[] skillCards = new SkillCard[3];

        [Header("Skill Pool")]
        [SerializeField] private Data.SkillUpgradeData[] availableUpgrades;

        [Header("Player Reference")]
        [SerializeField] private GameObject player;

        private void Start()
        {
            if (upgradePanel != null)
                upgradePanel.SetActive(false);

            // 플레이어의 레벨업 이벤트 구독
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (player != null)
            {
                var expSystem = player.GetComponent<Systems.ExperienceSystem>();
                if (expSystem != null)
                {
                    expSystem.OnLevelUp.AddListener(ShowUpgradeOptions);
                }
            }
        }

        private void ShowUpgradeOptions(int newLevel)
        {
            if (upgradePanel != null)
                upgradePanel.SetActive(true);

            // 랜덤으로 3개의 스킬 선택
            List<Data.SkillUpgradeData> selectedUpgrades = GetRandomUpgrades(3);

            // 카드에 스킬 정보 표시
            for (int i = 0; i < skillCards.Length && i < selectedUpgrades.Count; i++)
            {
                if (skillCards[i] != null)
                {
                    skillCards[i].SetupCard(selectedUpgrades[i], this);
                }
            }
        }

        private List<Data.SkillUpgradeData> GetRandomUpgrades(int count)
        {
            List<Data.SkillUpgradeData> result = new List<Data.SkillUpgradeData>();

            if (availableUpgrades == null || availableUpgrades.Length == 0)
                return result;

            List<Data.SkillUpgradeData> pool = new List<Data.SkillUpgradeData>(availableUpgrades);

            for (int i = 0; i < count && pool.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, pool.Count);
                result.Add(pool[randomIndex]);
                pool.RemoveAt(randomIndex);
            }

            return result;
        }

        public void OnSkillSelected(Data.SkillUpgradeData upgrade)
        {
            ApplyUpgrade(upgrade);

            if (upgradePanel != null)
                upgradePanel.SetActive(false);

            // 게임 재개
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResumeGame();
            }
        }

        private void ApplyUpgrade(Data.SkillUpgradeData upgrade)
        {
            if (player == null)
                return;

            switch (upgrade.upgradeType)
            {
                case Data.SkillUpgradeData.SkillUpgradeType.WeaponDamage:
                    // 모든 무기 데미지 증가
                    var weapons = player.GetComponentsInChildren<Weapon.WeaponBase>();
                    foreach (var weapon in weapons)
                    {
                        weapon.UpgradeWeapon();
                    }
                    break;

                case Data.SkillUpgradeData.SkillUpgradeType.MaxHealth:
                    var playerStats = player.GetComponent<Player.PlayerStats>();
                    if (playerStats != null)
                    {
                        playerStats.Heal(upgrade.valueIncrease);
                    }
                    break;

                case Data.SkillUpgradeData.SkillUpgradeType.MoveSpeed:
                    var playerController = player.GetComponent<Player.PlayerController>();
                    // 이동속도 증가 로직 (PlayerController에 속성 추가 필요)
                    break;

                // 다른 업그레이드 타입 처리...
            }
        }

        [System.Serializable]
        public class SkillCard
        {
            public GameObject cardObject;
            public Image iconImage;
            public TextMeshProUGUI nameText;
            public TextMeshProUGUI descriptionText;
            public Button selectButton;

            private Data.SkillUpgradeData currentUpgrade;
            private SkillUpgradeUI uiController;

            public void SetupCard(Data.SkillUpgradeData upgrade, SkillUpgradeUI controller)
            {
                currentUpgrade = upgrade;
                uiController = controller;

                if (iconImage != null && upgrade.icon != null)
                    iconImage.sprite = upgrade.icon;

                if (nameText != null)
                    nameText.text = upgrade.skillName;

                if (descriptionText != null)
                    descriptionText.text = upgrade.description;

                if (selectButton != null)
                {
                    selectButton.onClick.RemoveAllListeners();
                    selectButton.onClick.AddListener(OnCardSelected);
                }

                if (cardObject != null)
                    cardObject.SetActive(true);
            }

            private void OnCardSelected()
            {
                if (uiController != null && currentUpgrade != null)
                {
                    uiController.OnSkillSelected(currentUpgrade);
                }
            }
        }
    }
}
