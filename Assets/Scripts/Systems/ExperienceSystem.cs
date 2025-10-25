using UnityEngine;
using UnityEngine.Events;

namespace DystopiaGirls.Systems
{
    /// <summary>
    /// 경험치 및 레벨업 시스템
    /// </summary>
    public class ExperienceSystem : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private float currentExperience = 0f;
        [SerializeField] private float baseExperienceRequired = 100f;
        [SerializeField] private float experienceMultiplier = 1.5f;

        [Header("Events")]
        public UnityEvent<float, float> OnExperienceChanged; // current, required
        public UnityEvent<int> OnLevelUp; // new level

        private float experienceRequired;

        private void Awake()
        {
            experienceRequired = baseExperienceRequired;
        }

        private void Start()
        {
            OnExperienceChanged?.Invoke(currentExperience, experienceRequired);
        }

        public void AddExperience(float amount)
        {
            currentExperience += amount;

            while (currentExperience >= experienceRequired)
            {
                LevelUp();
            }

            OnExperienceChanged?.Invoke(currentExperience, experienceRequired);
        }

        private void LevelUp()
        {
            currentExperience -= experienceRequired;
            currentLevel++;

            // 다음 레벨에 필요한 경험치 계산
            experienceRequired = Mathf.Floor(baseExperienceRequired * Mathf.Pow(experienceMultiplier, currentLevel - 1));

            OnLevelUp?.Invoke(currentLevel);

            // 게임 일시정지하고 스킬 선택 UI 표시
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PauseGame();
            }
        }

        public int CurrentLevel => currentLevel;
        public float CurrentExperience => currentExperience;
        public float ExperienceRequired => experienceRequired;
        public float ExperiencePercentage => currentExperience / experienceRequired;
    }
}
