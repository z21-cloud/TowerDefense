using UnityEngine;

namespace TowerDefence.Core
{
    public class Factory<T> : IFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        public Factory(T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Create() => Object.Instantiate(_prefab, _parent);
    }
}