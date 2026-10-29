using UnityEngine;

namespace TowerDefence.Core
{
    [CreateAssetMenu(fileName = "PickableInteractorConfig", menuName = "TowerDefense/Pickable Interactor Config")]
    public class PickableInteractorConfig : ScriptableObject
    {
        [SerializeField] private Vector2 _offset = Vector2.right;
        [SerializeField] private Vector2 _size = Vector2.one;
        [SerializeField] private bool _debug;

        public Vector2 Offset => _offset;
        public Vector2 Size => _size;
        public bool Debug => _debug;
    }
}
