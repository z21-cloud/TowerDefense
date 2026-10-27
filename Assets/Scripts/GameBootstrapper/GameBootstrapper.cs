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

        private void Awake()
        {
            // Input initialization
            _inputManager.Initialize(_inputHandler);

            //
            _playerController.Initialize(_inputManager);

            _logPool.Initialize();
            _treeFactory.Initialize();
            _treePool.Initialize();
            _treeLootSpawner.Initialize(_channel);
        }
    }
}
