using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Core
{
    public class TreeFactory : MonoBehaviour, IFactory<Tree>
    {
        [SerializeField] private LogPool _logPool;
        [SerializeField] private Tree _prefab;
        [SerializeField] private Transform _parent;

        private Factory<Tree> _factory;

        // public void Initalize(LogPool logPool)
        // {

        // }

        private void Awake()
        {
            _factory = new Factory<Tree>(_prefab, _parent);
        }

        public Tree Create()
        {
            var obj = _factory.Create();
            obj.Initialize(_logPool);
            return obj;
        }
    }
}
