using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class WeaponRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpedd = 5f;

        private GetRotation _getRotation;

        private void Awake()
        {
            _getRotation = new GetRotation();
        }

        public void RotateWeapon(Vector2 cursosPosition)
        {
            Vector2 objectPosition = transform.position;

            Quaternion targetRotation = _getRotation.FromPositionToTarget(objectPosition, cursosPosition);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotationSpedd * Time.deltaTime
            );
        }
    }
}
