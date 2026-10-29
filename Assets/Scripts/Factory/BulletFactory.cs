using UnityEngine;

namespace TowerDefence.Core
{
    public class BulletFactory : MonoBehaviour, IFactory<Bullet>
    {
        [SerializeField] private BulletPool _bulletPool;
        // [SerializeField] private
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _parent;

        private Factory<Bullet> _factory;

        public void Initialize()
        {
            _factory = new Factory<Bullet>(_prefab, _parent);
        }

        public Bullet Create()
        {
            var obj = _factory.Create();
            obj.Initialzie(_bulletPool);
            return obj;
        }
    }
}
