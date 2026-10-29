using TowerDefence.Core;
using TowerDefence.PlayerInput;
using TowerDefence.PlayerView;
using UnityEngine;

namespace TowerDefence.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private InputHandler _inputHandler;

        [Header("Player")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PickableInteractor _pickableInteractor;
        [SerializeField] private Health _health;
        [SerializeField] private PlayerHealthUI _playerHealthUI;

        [Header("Weapon")]
        [SerializeField] private WeaponRotation _weaponRotation;

        [Header("Camera")]
        [SerializeField] private CameraFollow _cameraFollow;

        [Header("Tree Loot Spawner")]
        [SerializeField] private TreeLootSpawner _treeLootSpawner;
        [SerializeField] private TreeChoppedEventChannelSO _channel;
        [SerializeField] private TreePool _treePool;
        [SerializeField] private TreeFactory _treeFactory;
        [SerializeField] private LogPool _logPool;

        [Header("Inventory")]
        [SerializeField] private InventoryView _inventoryView;
        private Inventory _inventory;

        private void Awake()
        {
            // Input initialization
            _inputManager.Initialize(_inputHandler);

            //
            _logPool.Initialize();
            _treeFactory.Initialize();
            _treePool.Initialize();
            _treeLootSpawner.Initialize(_channel);

            //
            _inventory = new Inventory();
            _inventoryView.Initialize(_inventory);
            
            //
            _pickableInteractor.Initialize(_inventory);
            _playerController.Initialize(_inputManager, _pickableInteractor, _inventoryView);

            // 
            _playerHealthUI.Initialize(_health);
        }
    }
}
