using UnityEngine;

namespace TowerDefence.Core
{
    [CreateAssetMenu(fileName = "AxeInteractorConfig", menuName = "TowerDefense/Axe Interactor Config")]
    public class AxeInteractorConfig : ScriptableObject
    {
        [SerializeField] private float _axeUsageTime = 0.2f;
        [SerializeField] private float _axeHitDelay = 0.5f;
        [SerializeField] private Vector2 _offset = Vector2.right;
        [SerializeField] private Vector2 _size = Vector2.one;
        [SerializeField] private bool _debug;

        public float AxeUsageTime => _axeUsageTime;
        public float AxeHitDelay => _axeHitDelay;
        public Vector2 Offset => _offset;
        public Vector2 Size => _size;
        public bool Debug => _debug;
    }
}
