using UnityEngine;

namespace TowerDefence.Core
{
    public interface IInteractable
    {
        public bool CanAccept(PickableType type);
        public void Interact();
    }
}
