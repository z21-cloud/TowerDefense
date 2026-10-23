using TowerDefence.Core;
using TowerDefence.PlayerInput;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IInteractable>(out var interactable) && _inputManager.GetInteraction())
        {
            interactable.Interact();
        }
    }
}
