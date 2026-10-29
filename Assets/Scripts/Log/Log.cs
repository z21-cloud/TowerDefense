using UnityEngine;

namespace TowerDefence.Core
{
    public class Log : MonoBehaviour, IPickable
    {
        public PickableType Type { get; private set; }

        private void Awake()
        {
            Type = PickableType.Log;
        }

        public void Initialize()
        {
            
        }

        public void PickUp()
        {
            Debug.Log($"[Log] Picked up!");
            gameObject.SetActive(false);
        }
    }
}
