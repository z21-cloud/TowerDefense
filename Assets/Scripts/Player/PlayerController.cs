using System;
using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private WeaponRotation _weaponRotation;
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private AxeInteractor _axeInteractor;
        [SerializeField] private FlipComponent _flipComponent;
        [SerializeField] private Weapon _weapon;

        private IDamageDealer _currentDamagerDealer;
        private InputManager _inputManager;
        private InventoryView _inventoryView;
        private PickableInteractor _pickableInteractor;
        private Vector2 _moveVectorNormalized;
        private float _facing = 1f;

        public void Initialize(InputManager inputManager,
                                PickableInteractor pickableInteractor,
                                InventoryView inventoryView)
        {
            if (inputManager == null)
            {
                Debug.LogError("[PlayerController] InputManager is null.", this);
                return;
            }

            _inputManager = inputManager;
            _inventoryView = inventoryView;
            _pickableInteractor = pickableInteractor;
        }

        private void Awake()
        {
            if (_inputManager == null
            || _weaponRotation == null
            || _movementComponent == null
            || _pickableInteractor == null
            || _axeInteractor == null
            || _flipComponent == null
            || _inventoryView == null)
            {
                Debug.LogError($"[PlayerController] Component is null!", this);
                enabled = false;
                return;
            }

            _currentDamagerDealer = _axeInteractor;
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
            if(_inputManager.SelectPrimaryWeapon()) _currentDamagerDealer = _axeInteractor;
            if(_inputManager.SelectSecondaryWeapon()) _currentDamagerDealer = _weapon;
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
