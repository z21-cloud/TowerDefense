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
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private FlipComponent _flipComponent;
        [SerializeField] private Health _playerHealth;
        [SerializeField] private PlayerHealthUI _playerHealthUI;

        [Header("Weapon")]
        [SerializeField] private WeaponRotation _weaponRotation;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private AxeInteractor _axeInteractor;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private BulletFactory _bulletFactory;

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

        [Header("Enemy")]
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemyFactory _enemyFactory;

        private Inventory _inventory;

        private void Awake()
        {
            // Порядок инициализации важен: каждый блок зависит от предыдущих.

            // 1. Фабрики — первыми: пулы при наполнении создают объекты через фабрики.
            _bulletFactory.Initialize();
            _treeFactory.Initialize();
            _enemyFactory.Initialize();

            // 2. Пулы — сразу после фабрик: к этому моменту фабрики уже готовы.
            _logPool.Initialize();
            _treePool.Initialize();
            _bulletPool.Initialize();
            _enemyPool.Initialize();

            // 3. Ввод — до игрока: контроллер пользуется InputManager уже при инициализации.
            _inputManager.Initialize(_inputHandler);

            // 4. Спавнер лута деревьев — независимая подсистема, нужен только канал событий.
            _treeLootSpawner.Initialize(_channel);

            // 5. Инвентарь — до интеракторов и контроллера игрока: они получают его ссылкой.
            _inventory = new Inventory();
            _inventoryView.Initialize(_inventory);

            // 6. Интерактор подбора — до контроллера игрока, которому он передаётся готовым.
            _pickableInteractor.Initialize(_inventory);

            // 7. Контроллер игрока — последним из геймплейных: собирает все зависимости
            //    и выставляет стартовое оружие (топор активен, оружие скрыто).
            //    Пулы к этому моменту уже инициализированы — оружие можно включать безопасно.
            _playerController.Initialize(
                _inputManager,
                _pickableInteractor,
                _inventoryView,
                _weaponRotation,
                _movementComponent,
                _axeInteractor,
                _flipComponent,
                _weapon);

            // 8. UI здоровья — в конце: подписывается на уже инициализированное здоровье.
            _playerHealthUI.Initialize(_playerHealth);
        }
    }
}
