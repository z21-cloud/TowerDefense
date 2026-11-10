using UnityEngine;

namespace TowerDefence.Core
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float waypointReachDistance = 0.1f;
        [SerializeField] private float waypointTimerThreshold = 5f;

        private EnemyPathFinder _pathFinder;
        private Transform _target;
        private TreeChoppedEventChannelSO _channel;

        public void Initialize(Grid grid, TreeChoppedEventChannelSO channel)
        {
            if (grid == null || channel == null)
            {
                Debug.LogError("[Enemy] Grid or event channel reference is not set for the enemy.");
                enabled = false;
                return;
            }

            _pathFinder = new EnemyPathFinder(grid, waypointReachDistance, waypointTimerThreshold);
            _channel = channel;
            _channel.OnEventRaised += HandleTreeChopped;
        }

        public void FindEnemyPath(Transform target)
        {
            if (_pathFinder == null)
            {
                Debug.LogError("[Enemy] Enemy is not initialized.");
                return;
            }

            _target = target;

            if (!_pathFinder.FindPath(transform.position, target.position))
                Debug.LogError("[Enemy] No path found for the enemy.");
        }

        // После рубки дерева сетка меняется — перестраиваем путь к сохранённой
        // цели. Врагов, дошедших до конца пути, не трогаем.
        private void HandleTreeChopped(TreeChoppedData data)
        {
            if (!gameObject.activeInHierarchy || _target == null || _pathFinder == null)
                return;

            if (!_pathFinder.HasPath || _pathFinder.HasReachedEnd)
                return;

            FindEnemyPath(_target);
        }

        private void OnDestroy()
        {
            if (_channel != null)
                _channel.OnEventRaised -= HandleTreeChopped;
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
