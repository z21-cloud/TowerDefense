using UnityEngine;

namespace TowerDefence.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private Transform _campfire;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _spawnInterval = 3f;
        [SerializeField] private int _maxEnemies = 10;

        private Transform _target;
        private float _timer;
        private int _currentEnemies = 0;

        public void Initialize()
        {
            if (_enemyPool == null || _player == null || _spawnPoints == null || _spawnPoints.Length == 0 || _campfire == null)
            {
                Debug.LogError("[EnemySpawner] Spawner is not configured: assign pool, player and spawn points.");
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

            if(Vector3.Distance(enemy.transform.position, _player.position) < Vector3.Distance(enemy.transform.position, _campfire.position))
            {
                _target = _player;
            }
            else
            {
                _target = _campfire;
            }

            enemy.FindEnemyPath(_target);
            _currentEnemies++;
        }
    }
}
