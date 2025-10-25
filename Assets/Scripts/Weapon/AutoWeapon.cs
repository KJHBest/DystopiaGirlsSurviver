using UnityEngine;

namespace DystopiaGirls.Weapon
{
    /// <summary>
    /// 자동으로 가장 가까운 적을 공격하는 무기
    /// </summary>
    public class AutoWeapon : WeaponBase
    {
        [Header("Auto Weapon Settings")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private LayerMask enemyLayer;

        protected override void Attack()
        {
            Transform target = FindNearestEnemy();

            if (target == null)
                return;

            // 발사체 생성
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                // 적 방향으로 발사
                Vector2 direction = (target.position - transform.position).normalized;

                var rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * projectileSpeed;
                }

                // 발사체에 데미지 설정
                var projectileScript = projectile.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.Initialize(damage);
                }

                lastAttackTime = Time.time;
            }
        }

        private Transform FindNearestEnemy()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

            Transform nearest = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy.transform;
                }
            }

            return nearest;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
