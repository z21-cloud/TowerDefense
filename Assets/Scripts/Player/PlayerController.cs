using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerController : MonoBehaviour
    {
        private WeaponRotation _weaponRotation;
        private MovementComponent _movementComponent;
        private AxeInteractor _axeInteractor;
        private FlipComponent _flipComponent;
        private Weapon _weapon;

        private IDamageDealer _currentDamagerDealer;
        private InputManager _inputManager;
        private InventoryView _inventoryView;
        private PickableInteractor _pickableInteractor;
        private Vector2 _moveVectorNormalized;
        private float _facing = 1f;

        public void Initialize(InputManager inputManager,
                                PickableInteractor pickableInteractor,
                                InventoryView inventoryView,
                                WeaponRotation weaponRotation,
                                MovementComponent movementComponent,
                                AxeInteractor axeInteractor,
                                FlipComponent flipComponent,
                                Weapon weapon)
        {
            if (inputManager == null
            || pickableInteractor == null
            || inventoryView == null
            || weaponRotation == null
            || movementComponent == null
            || axeInteractor == null
            || flipComponent == null
            || weapon == null)
            {
                Debug.LogError("[PlayerController] Component is null!", this);
                enabled = false;
                return;
            }

            _inputManager = inputManager;
            _pickableInteractor = pickableInteractor;
            _inventoryView = inventoryView;
            _weaponRotation = weaponRotation;
            _movementComponent = movementComponent;
            _axeInteractor = axeInteractor;
            _flipComponent = flipComponent;
            _weapon = weapon;

            // Стартовое состояние: в руках топор, огнестрельное оружие скрыто
            _currentDamagerDealer = _axeInteractor;
            _axeInteractor.Activate();
            _weapon.Deactivate();
        }

        private void Update()
        {
            HandleMoveInput();

            // weapon & player rotation
            HandleRotationInput();

            HandleAttackInput();

            HandleInteractionInput();

            HandleInventoryInput();

            HandleWeaponSwitchInput();
        }

        private void HandleWeaponSwitchInput()
        {
            if (_inputManager.SelectPrimaryWeapon())
            {
                _currentDamagerDealer = _axeInteractor;
                _weapon.Deactivate();
                _axeInteractor.Activate();
            }
            if (_inputManager.SelectSecondaryWeapon())
            {
                _currentDamagerDealer = _weapon;
                _axeInteractor.Deactivate();
                _weapon.Activate();
            }
        }

        private void HandleInventoryInput()
        {
            if (_inputManager.GetOpenInventory())
            {
                bool value = _inventoryView.gameObject.activeInHierarchy;
                _inventoryView.OpenInventory(!value);
            }
        }

        private void HandleInteractionInput()
        {
            if (_inputManager.GetInteraction()) _pickableInteractor.Interact(_facing);
        }

        private void HandleAttackInput()
        {
            if (_inputManager.GetAttack())
            {
                _currentDamagerDealer.DealDamage(_facing);
            }
        }

        private void HandleRotationInput()
        {
            Vector2 mousePosition = _inputManager.GetMousePositionConverted();
            float side = mousePosition.x - transform.position.x;

            if (Mathf.Abs(side) > 0.05f)
            {
                _facing = Mathf.Sign(side);
                _flipComponent.SetFacing(_facing);
            }
            _weaponRotation.RotateWeapon(mousePosition);
        }

        private void HandleMoveInput()
        {
            _moveVectorNormalized = _inputManager.GetMovement().normalized;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _movementComponent.Move(_moveVectorNormalized);
        }
    }
}
