using UnityEngine;

namespace TowerDefence.Core
{
    public class Log : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log($"[Log] Picked up!");
            gameObject.SetActive(false);
        }
    }
}
