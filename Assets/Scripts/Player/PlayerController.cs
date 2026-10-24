using TowerDefence.Core;
using TowerDefence.PlayerInput;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponRotation _weaponRotation;
    [SerializeField] private PlayerMovement _playerMovement;

    private InputManager _inputManager;

    public void Initialize(InputManager inputManager)
    {
        _inputManager = inputManager;

        _playerMovement.Initialize(_inputManager);
        _weaponRotation.Initialize(_inputManager);
    }
}
