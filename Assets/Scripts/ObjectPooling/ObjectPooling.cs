using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    public class ObjectPooling<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly IFactory<T> _factory;
        private readonly List<T> _objects;
        public ObjectPooling(IFactory<T> factory, int initialiCount = 100)
        {
            _factory = factory;
            _objects = new(initialiCount);

            for (int i = 0; i < initialiCount; i++)
            {
                T obj = _factory.Create();
                obj.gameObject.SetActive(false);
                _objects.Add(obj);
            }
        }

        public T Get()
        {
            foreach (var obj in _objects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    // return not active object
                    return obj;
                }
            }

            Debug.LogError($"[ObjectPooling] Pool is empty! Return null...");
            return null;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
