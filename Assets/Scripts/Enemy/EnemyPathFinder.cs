using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    // Хранит путь и состояние продвижения по нему. Это чистый класс без
    // MonoBehaviour: трансформом двигает Enemy, а этот класс решает,
    // пройден ли текущий вейпоинт и какой следующий.
    public class EnemyPathFinder
    {
        private readonly Grid _grid;
        private readonly float _waypointReachDistance;
        private readonly float _stuckTimeout;

        private List<Vector2> _path;
        private int _currentWaypointIndex;
        private float _stuckTimer;

        public Vector2 CurrentWaypoint { get; private set; }
        public bool HasPath { get; private set; }
        public bool HasReachedEnd { get; private set; }

        public EnemyPathFinder(Grid grid, float waypointReachDistance, float stuckTimeout)
        {
            _grid = grid;
            _waypointReachDistance = waypointReachDistance;
            _stuckTimeout = stuckTimeout;
        }

        public bool FindPath(Vector2 start, Vector2 target)
        {
            _grid.FindPath(start, target, out List<Vector2> path);

            HasReachedEnd = false;
            _stuckTimer = 0f;

            if (path == null || path.Count == 0)
            {
                HasPath = false;
                return false;
            }

            _path = path;
            _currentWaypointIndex = 0;
            CurrentWaypoint = _path[_currentWaypointIndex];
            HasPath = true;
            return true;
        }

        // Вызывается из Enemy.Update после перемещения: проверка должна идти
        // в том же кадре и после движения, поэтому свой Update здесь не нужен.
        public void Tick(Vector2 currentPosition, float deltaTime)
        {
            if (!HasPath || HasReachedEnd)
                return;

            if (HasPassedWaypoint(currentPosition))
            {
                _stuckTimer = 0f;
                AdvanceToNextWaypoint();
                return;
            }

            // Страховка от застревания: если до точки не дошли за пороговое
            // время — переключаемся на следующую.
            _stuckTimer += deltaTime;
            if (_stuckTimer >= _stuckTimeout)
            {
                Debug.Log("[EnemyPathFinder] Enemy stuck, skipping to next waypoint.");
                _stuckTimer = 0f;
                AdvanceToNextWaypoint();
            }
        }

        public bool HasPassedWaypoint(Vector2 currentPosition)
        {
            return HasPath && !HasReachedEnd
                && Vector2.Distance(currentPosition, CurrentWaypoint) < _waypointReachDistance;
        }

        private void AdvanceToNextWaypoint()
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex < _path.Count)
            {
                CurrentWaypoint = _path[_currentWaypointIndex];
            }
            else
            {
                // Если застрял на последней точке — тоже считаем, что дошёл до конца пути.
                HasReachedEnd = true;
                Debug.Log("[EnemyPathFinder] Enemy reached the end of the path.");
            }
        }
    }
}
