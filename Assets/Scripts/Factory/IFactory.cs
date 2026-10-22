using UnityEngine;

namespace TowerDefence.Core
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        T Create();
    }
}
