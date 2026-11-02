using UnityEngine;

namespace TowerDefence.Core
{
    public class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private Transform _parent;

        private Factory<Enemy> _factory;

        public event System.Action<Enemy> OnCreated;

        public void Initialize()
        {
            _factory = new Factory<Enemy>(_prefab, _parent);
        }

        public Enemy Create()
        {
            var obj = _factory.Create();
            OnCreated?.Invoke(obj);
            return obj;
        }
    }
}
