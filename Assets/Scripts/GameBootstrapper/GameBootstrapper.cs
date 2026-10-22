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
        [SerializeField] private PlayerMovement _playerMovement;
        // [SerializeField] private PlayerRotation _playerRotation;

        [Header("Weapon")]
        [SerializeField] private WeaponRotation _weaponRotation;

        [Header("Camera")]
        [SerializeField] private CameraFollow _cameraFollow;

        private void Awake()
        {
            // Input initialization
            _inputManager.Initialize(_inputHandler);

            //
            _playerMovement.Initalize(_inputManager);

            //
            _weaponRotation.Initalize(_inputManager);
        }
    }
}
