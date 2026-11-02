using System;
using UnityEngine;

namespace TowerDefence.Core
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 10;
        [SerializeField] private EnemyFactory _enemyFactory;

        private ObjectPooling<Enemy> _pool;

        public void Initialize()
        {
            _enemyFactory.OnCreated += HandleEnemyCreated;
            _pool = new ObjectPooling<Enemy>(_enemyFactory, _initialCount);
        }

        private void HandleEnemyCreated(Enemy enemy)
        {
            enemy.GetComponent<Health>().OnDeath += () => _pool.Return(enemy);
        }

        public Enemy Get()
        {
            Enemy enemy = _pool.Get();

            if (enemy == null)
            {
                Debug.LogError($"[EnemyPool] Pool is Empty! Return null...");
                return null;
            }

            // GameObject is not active!
            return enemy;
        }

        public void Return(Enemy enemy)
        {
            enemy.GetComponent<Health>().ResetState();
            _pool.Return(enemy);
        }

        private void OnDestroy()
        {
            _enemyFactory.OnCreated -= HandleEnemyCreated;
        }
    }
}
