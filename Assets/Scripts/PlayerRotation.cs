using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpedd = 5f;
        [SerializeField] private InputManager _inputManager;

        // private Rigidbody2D _rb;
        private GetRotation _getRotation;

        public void Initalize(InputManager inputManager)
        {
            /*if (!this.TryGetComponent(out Rigidbody2D rb))
            {
                Debug.LogError("[PlayerMovement] Player has no Rigidbody2D");
                return;
            }

            _rb = rb;*/
            _getRotation = new GetRotation();

            _inputManager = inputManager;
        }

        private void Update()
        {
            Debug.Log($"[PlayerRotation] _inputManager.GetMousePositionConverted() - {_inputManager.GetMousePositionConverted()}");

            RotatePlayer();
        }

        private void RotatePlayer()
        {
            Vector2 objectPosition = transform.position;
            Vector2 cursosPosition = _inputManager.GetMousePositionConverted();

            Quaternion targetRotation = _getRotation.FromPositionToTarget(objectPosition, cursosPosition);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotationSpedd * Time.deltaTime
            );
        }
    }
}