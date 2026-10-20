using UnityEngine;

namespace TowerDefence.PlayerInput
{
    public class InputHandler : MonoBehaviour, IInputReader
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public Vector2 MousePosition { get; private set; }
        public bool Attack { get; private set; }
        public bool Reload { get; private set; }
        public bool Interact { get; private set; }
        public bool OpenInventory { get; private set; }
        public bool SelectPrimaryWeapon { get; private set; }
        public bool SelectSecondaryWeapon { get; private set; }

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
            Reload = Input.GetButtonDown("Reload");

            OpenInventory = Input.GetButtonDown("Inventory");

            Interact = Input.GetButtonDown("Interact");

            SelectPrimaryWeapon = Input.GetButtonDown("Primary Weapon");
            SelectSecondaryWeapon = Input.GetButtonDown("Secondary Weapon");
        }

        private void MouseButtons()
        {
            Attack = Input.GetMouseButtonDown(0);
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
