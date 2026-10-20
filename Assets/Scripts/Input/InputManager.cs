using UnityEngine;

namespace TowerDefence.PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        private IInputReader _input;
        public void Initialize(IInputReader inputReader)
        {
            _input = inputReader;
        }

        public Vector2 GetMovement()
        {
            Vector2 movement = new Vector2(_input.Horizontal, _input.Vertical);

            Debug.Log($"[InputManager] HorizontalInput = {_input.Horizontal}");
            Debug.Log($"[InputManager] VerticalInput = {_input.Vertical}");

            return movement;
        }

        public bool GetAttack()
        {
            Debug.Log($"[InputManager] input.Attack = {_input.Attack}");
            return _input.Attack;
        }

        public bool GetInteraction()
        {
            Debug.Log($"[InputManager] input.Interact = {_input.Interact}");
            return _input.Interact;
        }

        public bool GetReload()
        {
            Debug.Log($"[InputManager] input.Reload = {_input.Reload}");
            return _input.Reload;
        }

        public bool GetOpenInventory()
        {
            Debug.Log($"[InputManager] input.OpenInventory = {_input.OpenInventory}");
            return _input.OpenInventory;
        }

        public bool SelectPrimaryWeapon()
        {
            Debug.Log($"[InputManager] input.SelectPrimaryWeapon = {_input.SelectPrimaryWeapon}");
            return _input.SelectPrimaryWeapon;
        }

        public bool SelectSecondaryWeapon()
        {
            Debug.Log($"[InputManager] input.SelectSecondaryWeapon = {_input.SelectSecondaryWeapon}");
            return _input.SelectSecondaryWeapon;
        }

        public Vector2 GetMousePositionConverted()
        {
            Vector2 convertedMousePosition = Camera.main.ScreenToWorldPoint(_input.MousePosition);
            return convertedMousePosition;
        }

        public Vector2 GetMousePositionRaw()
        {
            return _input.MousePosition;
        }
    }
}
