using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputManager inputManager;
        [SerializeField] private InputHandler inputHandler;

        private void Awake()
        {
            // Input initialization
            inputManager.Initialize(inputHandler);

            //
        }
    }
}
