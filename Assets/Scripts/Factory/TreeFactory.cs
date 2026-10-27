using UnityEngine;

namespace TowerDefence.Core
{
    public class TreeFactory : MonoBehaviour, IFactory<Tree>
    {
        [SerializeField] private TreePool _treePool;
        [SerializeField] private TreeChoppedEventChannelSO _channel;
        [SerializeField] private Tree _prefab;
        [SerializeField] private Transform _parent;

        private Factory<Tree> _factory;

        // public void Initalize(LogPool logPool)
        // {

        // }

        public void Initialize()
        {
            _factory = new Factory<Tree>(_prefab, _parent);
        }

        public Tree Create()
        {
            var obj = _factory.Create();
            obj.Initialize(_channel, _treePool);
            return obj;
        }
    }
}
