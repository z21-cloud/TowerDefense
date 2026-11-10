using UnityEngine;

namespace TowerDefence.Core
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float waypointReachDistance = 0.1f;
        [SerializeField] private float waypointTimerThreshold = 5f;

        private EnemyPathFinder _pathFinder;

        public void Initialize(Grid grid)
        {
            if (grid == null)
            {
                Debug.LogError("[Enemy] Grid reference is not set for the enemy.");
                enabled = false;
                return;
            }

            _pathFinder = new EnemyPathFinder(grid, waypointReachDistance, waypointTimerThreshold);
        }

        public void FindEnemyPath(Transform target)
        {
            if (_pathFinder == null)
            {
                Debug.LogError("[Enemy] Enemy is not initialized.");
                return;
            }

            if (!_pathFinder.FindPath(transform.position, target.position))
                Debug.LogError("[Enemy] No path found for the enemy.");
        }

        private void Update()
        {
            if (_pathFinder == null || !_pathFinder.HasPath || _pathFinder.HasReachedEnd)
                return;

            // MoveTowards не перелетает цель — без этого при просадке кадра враг
            // осциллирует вокруг вейпоинта и не попадает в порог расстояния.
            transform.position = Vector3.MoveTowards(transform.position, _pathFinder.CurrentWaypoint, speed * Time.deltaTime);

            _pathFinder.Tick(transform.position, Time.deltaTime);
        }
    }
}
