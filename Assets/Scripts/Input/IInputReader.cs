using UnityEngine;

namespace TowerDefence.PlayerInput
{
    public interface IInputReader
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public Vector2 MousePosition { get; }
        public bool LMB { get; }
        public bool R { get; } // R button
        public bool TAB { get; }
    }
}
