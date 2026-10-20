using UnityEngine;

namespace TowerDefence.PlayerInput
{
    public interface IInputReader
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public Vector2 MousePosition { get; }
        public bool Attack { get; }
        public bool Reload { get; }
        public bool Interact { get; }
        public bool OpenInventory { get; }
        public bool SelectPrimaryWeapon { get; }
        public bool SelectSecondaryWeapon { get; }
    }
}
