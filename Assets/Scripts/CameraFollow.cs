using UnityEngine;

namespace TowerDefence.PlayerView
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _cameraOffset = -10f;
        [SerializeField] private Transform _player;

        // [Header("Camera bounds")]
        // [SerializeField] private bool _clampToMapBounds;
        // [SerializeField] private Vector2 _minBounds;
        // [SerializeField] private Vector2 _maxBounds;

        // private Camera _camera;
        // private bool _cameraConfigurationErrorLogged;

        // Initialize player position, use game bootstrapper for this 
        // Скорее всего потребуется интерфейс просто для передачи позиции игрока, ее можно будет получить из PlayerController
        public void Initailize()
        {
            
        }

        // private void Awake()
        // {
        //     _camera = GetComponent<Camera>();
        // }

        private void LateUpdate()
        {
            if(_player == null)
            {
                Debug.LogError("[CameraFollow] player position is null! Returning...");
                return;
            }

            Vector2 lerpPosition = Vector2.Lerp(transform.position, _player.position, _speed * Time.deltaTime);
            Vector3 resultPosition = new Vector3(lerpPosition.x, lerpPosition.y, _cameraOffset);

            // if (_clampToMapBounds)
            // {
            //     if (_camera == null || !_camera.orthographic)
            //     {
            //         if (!_cameraConfigurationErrorLogged)
            //         {
            //             Debug.LogError("[CameraFollow] Map bounds clamping requires an orthographic Camera component.");
            //             _cameraConfigurationErrorLogged = true;
            //         }
            //         return;
            //     }

            //     _cameraConfigurationErrorLogged = false;
            //     resultPosition = ClampToMapBounds(resultPosition);
            // }

            transform.position = resultPosition;
        }

        // private Vector3 ClampToMapBounds(Vector3 position)
        // {
        //     float halfHeight = _camera.orthographicSize;
        //     float halfWidth = halfHeight * _camera.aspect;

        //     position.x = ClampAxis(position.x, _minBounds.x, _maxBounds.x, halfWidth);
        //     position.y = ClampAxis(position.y, _minBounds.y, _maxBounds.y, halfHeight);

        //     return position;
        // }

        // private static float ClampAxis(float position, float minBound, float maxBound, float cameraHalfSize)
        // {
        //     float minPosition = minBound + cameraHalfSize;
        //     float maxPosition = maxBound - cameraHalfSize;

        //     return minPosition > maxPosition
        //         ? (minBound + maxBound) * 0.5f
        //         : Mathf.Clamp(position, minPosition, maxPosition);
        // }
    }
}
