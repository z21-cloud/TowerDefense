using UnityEngine;

namespace TowerDefence.Core
{
    public class TreePool : MonoBehaviour
    {
        [SerializeField] private Tree _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private int _initialiCount = 100;

        private ObjectPooling<Tree> _pool;

        // Change Awake() -> Initialize() method for game bootstrapper

        // public void Initailize()
        // {
        //      IFactory<Log> factory = new Factory<Log>(_prefab, _parent);
        //      _pool = new ObjectPooling<Log>(factory, _initialiCount);
        // }

        private void Awake()
        {
            IFactory<Tree> factory = new Factory<Tree>(_prefab, _parent);
            _pool = new ObjectPooling<Tree>(factory, _initialiCount);
        }

        public Tree Get()
        {
            Tree tree = _pool.Get();

            if(tree == null)
            {
                Debug.LogError($"[TreePool] Pool is Empty! Return null...");
                return null;
            }

            // GameObject is not active!
            return tree;
        }

        public void Return(Tree tree)
        {
            _pool.Return(tree);
        }
    }
}
