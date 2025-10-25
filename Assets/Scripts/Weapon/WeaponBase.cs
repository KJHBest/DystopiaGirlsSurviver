using UnityEngine;

namespace DystopiaGirls.Weapon
{
    /// <summary>
    /// 무기 베이스 클래스
    /// </summary>
    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Weapon Stats")]
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float attackSpeed = 1f;
        [SerializeField] protected int level = 1;

        protected float lastAttackTime;

        protected virtual void Update()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
                return;

            if (CanAttack())
            {
                Attack();
            }
        }

        protected virtual bool CanAttack()
        {
            return Time.time >= lastAttackTime + (1f / attackSpeed);
        }

        protected abstract void Attack();

        public virtual void UpgradeWeapon()
        {
            level++;
            // 레벨업 시 스탯 증가
            damage *= 1.1f;
            attackSpeed *= 1.05f;
        }

        public float Damage => damage;
        public float AttackSpeed => attackSpeed;
        public int Level => level;
    }
}
