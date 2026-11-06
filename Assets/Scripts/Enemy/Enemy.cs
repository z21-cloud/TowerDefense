using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float waypointTimerThreshold = 5f;

        private Grid _grid;
        private int currentWaypointIndex = 0;
        private List<Vector2> _path;
        private Vector2 _currentWaypoint;
        private float _timer;

        public void Initialize(Grid grid)
        {
            _grid = grid;
        }

        public void FindEnemyPath(Transform target)
        {
            if (_grid == null)
            {
                Debug.LogError("[Enemy] Grid reference is not set for the enemy.");
                return;
            }

            _grid.FindPath(transform.position, target.position, out List<Vector2> path);
            _path = path;

            if (_path != null && _path.Count > 0)
            {
                currentWaypointIndex = 0;
                _currentWaypoint = _path[currentWaypointIndex];
                _timer = 0f;
            }
            else
            {
                Debug.LogError("No path found for the enemy.");
            }
        }

        private void Update()
        {
            if (_path == null || currentWaypointIndex >= _path.Count)
                return;

            // MoveTowards не перелетает цель — без этого при просадке кадра враг
            // осциллирует вокруг вейпоинта и не попадает в порог расстояния.
            transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _currentWaypoint) < 0.1f)
            {
                _timer = 0f;
                currentWaypointIndex++;
                if (currentWaypointIndex < _path.Count)
                {
                    _currentWaypoint = _path[currentWaypointIndex];
                }
                else
                {
                    // Reached the end of the path
                    Debug.Log("[Enemy] Enemy reached the end of the path.");
                }
            }
            else
            {
                // Страховка от застревания: если до точки не дошли за пороговое время —
                // переключаемся на следующую.
                _timer += Time.deltaTime;
                if (_timer >= waypointTimerThreshold)
                {
                    _timer = 0f;
                    currentWaypointIndex++;
                    if (currentWaypointIndex < _path.Count)
                    {
                        Debug.Log("[Enemy] Enemy stuck, moving to next waypoint.");
                        _currentWaypoint = _path[currentWaypointIndex];
                    }
                    // Иначе застрял на последней точке — считаем, что дошёл до конца пути.
                }
            }
        }
    }
}
