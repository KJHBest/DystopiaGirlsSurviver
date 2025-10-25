using UnityEngine;

namespace DystopiaGirls.Weapon
{
    /// <summary>
    /// 발사체 - 적에게 데미지를 줌
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private bool destroyOnHit = true;

        private float damage;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        public void Initialize(float damageAmount)
        {
            damage = damageAmount;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                var enemy = collision.GetComponent<Enemy.Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
