using UnityEngine;

namespace TowerDefence.PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        private IInputReader input;
        public void Initialize(IInputReader inputReader)
        {
            input = inputReader;
        }
    }
}
