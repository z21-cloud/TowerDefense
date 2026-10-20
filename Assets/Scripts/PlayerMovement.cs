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

        public void Initalize(InputManager inputManager)
        {
            if (!this.TryGetComponent(out Rigidbody2D rb))
            {
                Debug.LogError("[PlayerMovement] Player has no Rigidbody2D");
                return;
            }

            _rb = rb;
            _inputManager = inputManager;
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _inputManager.GetMovement() * _speed;
        }
    }
}
