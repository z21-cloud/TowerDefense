using System;
using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        [SerializeField] private InputManager _inputManager;

        private Rigidbody2D _rb;
        private bool _initializationError = false;

        public void Initalize(InputManager inputManager)
        {
            if(inputManager == null)
            {
                Debug.LogError("[PlayerMovement] Input Manager is null");
                _initializationError = true;
                return;
            }

            if (!this.TryGetComponent(out Rigidbody2D rb))
            {
                Debug.LogError("[PlayerMovement] Player has no Rigidbody2D");
                _initializationError = true;
                return;
            }

            _rb = rb;
            _inputManager = inputManager;
        }

        private void FixedUpdate()
        {
            if(_initializationError) 
            {
                Debug.LogError("[PlayerMovement] Initialization error! Check logs");
                return;
            }
            
            _rb.linearVelocity = _inputManager.GetMovement() * _speed;
        }
    }
}
