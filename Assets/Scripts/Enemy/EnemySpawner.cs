using UnityEngine;
using System.Collections;

namespace DystopiaGirls.Enemy
{
    /// <summary>
    /// 화면 밖에서 적을 스폰
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private float spawnInterval = 2f;
        [SerializeField] private float spawnDistance = 15f;
        [SerializeField] private int maxEnemies = 100;

        [Header("References")]
        [SerializeField] private Transform player;

        private float nextSpawnTime;
        private int currentEnemyCount;

        private void Start()
        {
            if (player == null)
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                {
                    player = playerObj.transform;
                }
            }

            nextSpawnTime = Time.time + spawnInterval;
        }

        private void Update()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
                return;

            if (player == null)
                return;

            if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        private void SpawnEnemy()
        {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
                return;

            // 랜덤 적 선택
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // 화면 밖 랜덤 위치 계산
            Vector2 spawnPosition = GetRandomSpawnPosition();

            // 적 생성
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.SetParent(transform);

            currentEnemyCount++;

            // 적이 파괴될 때 카운트 감소
            var enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemy.GetComponent<Enemy>().enabled = true;
            }

            StartCoroutine(TrackEnemyDestruction(enemy));
        }

        private IEnumerator TrackEnemyDestruction(GameObject enemy)
        {
            while (enemy != null)
            {
                yield return null;
            }

            currentEnemyCount--;
        }

        private Vector2 GetRandomSpawnPosition()
        {
            // 플레이어 주변 원형 범위에서 랜덤 각도
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            Vector2 offset = new Vector2(
                Mathf.Cos(angle) * spawnDistance,
                Mathf.Sin(angle) * spawnDistance
            );

            return (Vector2)player.position + offset;
        }

        public int CurrentEnemyCount => currentEnemyCount;
    }
}
