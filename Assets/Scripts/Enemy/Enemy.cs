using UnityEngine;

namespace DystopiaGirls.Enemy
{
    /// <summary>
    /// 적 AI - 플레이어를 추적하고 공격
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float maxHealth = 50f;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float experienceValue = 10f;

        [Header("References")]
        private Transform player;
        private Rigidbody2D rb;

        private float currentHealth;
        private float lastAttackTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
        }

        private void Start()
        {
            // 플레이어 찾기
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            if (player == null)
                return;

            // 플레이어 방향으로 이동
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // 경험치 드롭
            DropExperience();

            Destroy(gameObject);
        }

        private void DropExperience()
        {
            // 경험치 오브젝트 생성 또는 직접 플레이어에게 경험치 부여
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                var expSystem = playerObj.GetComponent<Systems.ExperienceSystem>();
                if (expSystem != null)
                {
                    expSystem.AddExperience(experienceValue);
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // 플레이어에게 데미지
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    var playerStats = collision.gameObject.GetComponent<Player.PlayerStats>();
                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(damage);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }

        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;
    }
}
