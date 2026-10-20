using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpedd = 5f;

        [SerializeField] private InputManager _inputManager;

        // private Rigidbody2D _rb;

        public void Initalize(InputManager inputManager)
        {
            /*if (!this.TryGetComponent(out Rigidbody2D rb))
            {
                Debug.LogError("[PlayerMovement] Player has no Rigidbody2D");
                return;
            }

            _rb = rb;*/

            _inputManager = inputManager;
        }

        private void Update()
        {
            Debug.Log($"[PlayerRotation] _inputManager.GetMousePositionConverted() - {_inputManager.GetMousePositionConverted()}");
            Vector2 position = transform.position;
            Vector2 direction = _inputManager.GetMousePositionConverted() - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotationSpedd * Time.deltaTime
            );
        }
    }
}