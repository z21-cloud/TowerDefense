using UnityEngine;

namespace TowerDefence.Core
{
    public interface IPool<T> where T : MonoBehaviour
    {
        public T Get();
        public void Return(T obj);
    }
}
