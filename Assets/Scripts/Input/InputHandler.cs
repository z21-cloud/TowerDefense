using System;
using UnityEngine;

namespace TowerDefence.PlayerInput
{
    public class InputHandler : MonoBehaviour, IInputReader
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public Vector2 MousePosition { get; private set; }
        public bool LMB { get; private set; }
        public bool R { get; private set; } // R button
        public bool TAB { get; private set; }

        private void Update()
        {
            // WASD INPUT
            HorizontalInput();
            VerticalInput();

            // Mouse
            MouseInput();
            MouseButtons();

            // Keyboard
            KeyboardHandler();
        }

        private void KeyboardHandler()
        {
            // R = Input.GetButtonDown();

            // TAB = Input.GetButtonDown();
        }

        private void MouseButtons()
        {
            LMB = Input.GetMouseButton(0);
        }

        private void MouseInput()
        {
            MousePosition = Input.mousePosition;
        }

        private void VerticalInput()
        {
            Vertical = Input.GetAxisRaw("Vertical");
        }

        private void HorizontalInput()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
        }
    }
}
