using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class WeaponRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpedd = 5f;
        [SerializeField] private InputManager _inputManager;

        private GetRotation _getRotation;

        public void Initalize(InputManager inputManager)
        {
            _getRotation = new GetRotation();

            _inputManager = inputManager;
        }

        private void Update()
        {
            RotateWeapon();
        }

        private void RotateWeapon()
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
