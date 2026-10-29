using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    public class PickableInteractor : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private bool _debug;

        private Inventory _inventory;
        private Vector2 _debugCenter;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Interact(float direction = 1f)
        {
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
            Vector2 center = (Vector2)transform.position + _offset * direction;
            _debugCenter = center;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, _size, 0f);
            return colliders;
        }

        private void OnDrawGizmos()
        {
            if (!_debug) return;

            Gizmos.DrawWireCube(_debugCenter, _size);
        }
    }
}
