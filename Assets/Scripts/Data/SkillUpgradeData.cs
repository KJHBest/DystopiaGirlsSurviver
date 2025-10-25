using UnityEngine;

namespace DystopiaGirls.Data
{
    /// <summary>
    /// 스킬 업그레이드 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "SkillUpgrade", menuName = "DystopiaGirls/Skill Upgrade")]
    public class SkillUpgradeData : ScriptableObject
    {
        [Header("Basic Info")]
        public string skillName;
        [TextArea(3, 5)]
        public string description;
        public Sprite icon;

        [Header("Upgrade Type")]
        public SkillUpgradeType upgradeType;

        [Header("Values")]
        public float valueIncrease;
        public int maxLevel = 5;

        public enum SkillUpgradeType
        {
            WeaponDamage,
            WeaponAttackSpeed,
            NewWeapon,
            MaxHealth,
            MoveSpeed,
            PickupRange,
            ExperienceGain
        }
    }
}
