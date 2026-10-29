using System.Collections.Generic;
using TowerDefenсe.Core;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PickableInteractor : MonoBehaviour
    {
        [SerializeField] private PickableInteractorConfig _config;

        private Inventory _inventory;
        private Vector2 _debugCenter;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Interact(float direction = 1f)
        {
            if (_config == null)
            {
                Debug.LogError("[PickableInteractor] PickableInteractorConfig is not assigned.", this);
                return;
            }

            Collider2D[] colliders = PickableZone(direction);

            Collider2D nearestCol = null;

            float nearestSqrDistance = float.MaxValue;

            foreach (var col in colliders)
            {
                if (!col.TryGetComponent<IInteractable>(out var _) && !col.TryGetComponent<IPickable>(out var _)) continue;

                float sqrDistance = ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

                if (sqrDistance < nearestSqrDistance)
                {
                    nearestCol = col;
                    nearestSqrDistance = sqrDistance;
                }
            }

            if (nearestCol == null) return;

            if (nearestCol.TryGetComponent<IPickable>(out var pickable))
            {
                if (!_inventory.TryPickUp(pickable)) return;

                Debug.Log($"[PickableInteractor] PickUping... {nearestCol.name}");
            }

            else if (nearestCol.TryGetComponent<IInteractable>(out var interactable))
            {
                if(!interactable.CanAccept(PickableType.Log)) return;
                if (!_inventory.TryRemoveLog()) return;

                Debug.Log($"[PickableInteractor] Interacting... {nearestCol.name}");

                interactable.Interact();
            }
        }

        private Collider2D[] PickableZone(float direction)
        {
            Vector2 center = (Vector2)transform.position + _config.Offset * direction;
            _debugCenter = center;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, _config.Size, 0f);
            return colliders;
        }

        private void OnDrawGizmos()
        {
            if (_config == null || !_config.Debug) return;

            Gizmos.DrawWireCube(_debugCenter, _config.Size);
        }
    }
}
