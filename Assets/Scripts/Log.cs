using UnityEngine;

namespace TowerDefence.Core
{
    public class Log : MonoBehaviour, IPickable
    {
        public void Initialize()
        {
            
        }

        public bool TryPickUp()
        {
            Debug.Log($"[Log] Picked up!");
            gameObject.SetActive(false);
            return true;
        }
    }
}
