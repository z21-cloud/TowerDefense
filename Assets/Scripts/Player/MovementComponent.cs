using UnityEngine;

namespace TowerDefence.Core
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            if (!TryGetComponent(out Rigidbody2D rb))
            {
                Debug.LogError($"[PlayerMovement] {name} has no Rigidbody2D");
                return;
            }

            _rb = rb;
        }

        public void Move(Vector2 moveDirectionNormalized)
        {
            if(_rb == null) return;
            
            _rb.linearVelocity = moveDirectionNormalized * _speed;
        }
    }
}
