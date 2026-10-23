using UnityEngine;

namespace TowerDefence.Core
{
    public class LogPool : MonoBehaviour
    {
        [SerializeField] private Log _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private int _initialiCount = 100;

        private ObjectPooling<Log> _pool;

        // Change Awake() -> Initialize() method for game bootstrapper
        private void Awake()
        {
            IFactory<Log> factory = new Factory<Log>(_prefab, _parent);
            _pool = new ObjectPooling<Log>(factory, _initialiCount);
        }

        public Log Get()
        {
            Log log = _pool.Get();

            if(log == null)
            {
                Debug.LogError($"[LogPool] Pool is Empty! Return null...");
                return null;
            }

            // GameObject is not active!
            return log;
        }

        public void Return(Log log)
        {
            _pool.Return(log);
        }
    }
}
