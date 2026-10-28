using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private WeaponRotation _weaponRotation;
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private PickableInteractor _pickableInteractor;
        [SerializeField] private AxeInteractor _axeInteractor;
        [SerializeField] private FlipComponent _flipComponent;
        [SerializeField] private InventoryView _inventoryView;

        private InputManager _inputManager;
        private Vector2 _moveVectorNormalized;
        private float _facing = 1f;

        public void Initialize(InputManager inputManager)
        {
            if (inputManager == null)
            {
                Debug.LogError("[PlayerController] InputManager is null.", this);
                return;
            }

            _inputManager = inputManager;
            _pickableInteractor.Initialize(_inventoryView);
        }

        private void Awake()
        {
            if (_weaponRotation == null || _movementComponent == null
                || _pickableInteractor == null || _axeInteractor == null || _flipComponent == null)
            {
                Debug.LogError($"[PlayerController] Component is null!", this);
                return;
            }
        }

        private void Update()
        {
            if(_inputManager == null || _weaponRotation == null || 
                _movementComponent == null || _pickableInteractor == null || 
                _axeInteractor == null || _flipComponent == null) 
                return; 

            _moveVectorNormalized = _inputManager.GetMovement().normalized;

            if (_moveVectorNormalized.x != 0f)
            {
                _facing = Mathf.Sign(_moveVectorNormalized.x);
                _flipComponent.SetFacing(_facing);
            }

            if (_inputManager.GetAttack())
            {
                _axeInteractor.TryUse(_facing);
            }

            if(_inputManager.GetInteraction()) _pickableInteractor.Interact(_facing);

            if(_inputManager.GetOpenInventory()) 
            {
                bool value = _inventoryView.gameObject.activeInHierarchy;
                _inventoryView.gameObject.SetActive(!value);
            }

            Vector2 mousePositionConverted = _inputManager.GetMousePositionConverted();
            _weaponRotation.RotateWeapon(mousePositionConverted);
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
