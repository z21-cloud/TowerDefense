using UnityEngine;

namespace TowerDefence.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _spawnInterval = 3f;
        [SerializeField] private int _maxEnemies = 10;

        private float _timer;
        private int _currentEnemies = 0;

        public void Initialize()
        {
            if (_enemyPool == null || _target == null || _spawnPoints == null || _spawnPoints.Length == 0)
            {
                Debug.LogError("[EnemySpawner] Spawner is not configured: assign pool, target and spawn points.");
                enabled = false;
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer < _spawnInterval || _currentEnemies >= _maxEnemies)
                return;

            _timer = 0f;
            Spawn();
        }

        private void Spawn()
        {
            Enemy enemy = _enemyPool.Get();
            if (enemy == null)
                return;

            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            enemy.transform.position = spawnPoint.position;
            enemy.gameObject.SetActive(true);
            enemy.FindEnemyPath(_target);
            _currentEnemies++;
        }
    }
}
