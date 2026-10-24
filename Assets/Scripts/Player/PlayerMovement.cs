using System;
using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Rigidbody2D _rb;
        private InputManager _inputManager;

        public void Initialize(InputManager inputManager)
        {
            if(inputManager == null)
            {
                Debug.LogError("[PlayerMovement] Input Manager is null");
                return;
            }

            if (!TryGetComponent(out Rigidbody2D rb))
            {
                Debug.LogError("[PlayerMovement] Player has no Rigidbody2D");
                return;
            }

            _rb = rb;
            _inputManager = inputManager;
        }

        private void FixedUpdate()
        {
            if(_inputManager == null || _rb == null) 
            {
                Debug.LogError("[PlayerMovement] Initialization error! Check logs");
                return;
            }
            
            Vector2 normalizedDirection = _inputManager.GetMovement().normalized;

            // If player goes right -> flips right
            // If player goes left -> flips left
            // FlipSprite(normalizedDirection.x);

            _rb.linearVelocity = normalizedDirection * _speed;
            
            // Debug.Log(_rb.linearVelocity.magnitude);
        }
    }
}
