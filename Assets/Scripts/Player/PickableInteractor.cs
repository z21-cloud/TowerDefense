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
        private InventoryView _inventoryView;
        private Vector2 _debugCenter;
        // private readonly HashSet<IPickable> _pickables = new();

        public void Initialize(InventoryView inventoryView)
        {
            _inventory = new Inventory();

            _inventoryView = inventoryView;
            _inventoryView.Initalize(_inventory);
        }

        // private void Update()
        // {
        //     if(_inventory == null) return;

        //     if(Input.GetKeyDown(KeyCode.J))
        //     {
        //         _inventory.TryRemoveLog();
        //     }
        // }

        // public void PickUp(float direction = 1f)
        // {
        //     // _pickables.Clear();
        //     Collider2D[] colliders = PickableZone(direction);

        //     IPickable nearest = null;
        //     Collider2D nearestCol = null;
        //     float nearestSqrDistance = float.MaxValue;

        //     foreach (var col in colliders)
        //     {
        //         // if (col.TryGetComponent<IPickable>(out var pickable)
        //         //     /*&& _pickables.Add(pickable)*/
        //         //      && Vector2.Distance(transform.position, col.transform.position) < nearestSqrDistance)
        //         // {
        //         //     nearest = pickable;
        //         //     nearestCol = col;
        //         //     nearestSqrDistance = Vector2.Distance(transform.position, col.transform.position);
        //         // }

        //         if (!col.TryGetComponent<IPickable>(out var pickable)) continue;

        //         float sqrDistance = ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

        //         if (sqrDistance < nearestSqrDistance)
        //         {
        //             nearest = pickable;
        //             nearestCol = col;
        //             nearestSqrDistance = sqrDistance;
        //         }
        //     }

        //     if (nearest == null) return;

        //     if (!_inventory.TryAddLog()) return;

        //     _inventoryView.Refresh();
        //     Debug.Log($"[PickableInteractor] PickUping... {nearestCol.name}");
        //     nearest.TryPickUp();
        // }

        public void Interact(float direction = 1f)
        {
            // _pickables.Clear();
            Collider2D[] colliders = PickableZone(direction);

            IInteractable nearestInteractable = null;
            IPickable nearestPickable = null;
            Collider2D nearestCol = null;
            float nearestInteractableSqrDistance = float.MaxValue;
            float nearestPickableSqrDistance = float.MaxValue;


            foreach (var col in colliders)
            {
                // if (col.TryGetComponent<IPickable>(out var pickable)
                //     /*&& _pickables.Add(pickable)*/
                //      && Vector2.Distance(transform.position, col.transform.position) < nearestSqrDistance)
                // {
                //     nearest = pickable;
                //     nearestCol = col;
                //     nearestSqrDistance = Vector2.Distance(transform.position, col.transform.position);
                // }

                if (!col.TryGetComponent<IInteractable>(out var interactable)) continue;

                float sqrDistance = ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

                if (sqrDistance < nearestInteractableSqrDistance)
                {
                    nearestInteractable = interactable;
                    nearestCol = col;
                    nearestInteractableSqrDistance = sqrDistance;
                }
            }

            foreach (var col in colliders)
            {
                if (!col.TryGetComponent<IPickable>(out var pickable)) continue;

                float sqrDistance = ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

                if (sqrDistance < nearestPickableSqrDistance)
                {
                    nearestPickable = pickable;
                    nearestCol = col;
                    nearestPickableSqrDistance = sqrDistance;
                }
            }

            if (nearestPickable == null && nearestInteractable == null) return;

            // at first player tries to pick up object, after interact
            // 1. nearest pickable != null && nearestPickableSqrDistance <= nearestInteractableSqrDistance
            // 1.1 Pick up logic
            if(nearestPickable != null && nearestPickableSqrDistance <= nearestInteractableSqrDistance)
            {
                if (!_inventory.TryAddLog()) return;

                Debug.Log($"[PickableInteractor] PickUping... {nearestCol.name}");
                
                nearestPickable.TryPickUp();
            }

            // 2. nearestInteractable != null && nearestInteractableSqrDistance < nearestPickableSqrDistance
            // 2.1 Interact logic
            else if(nearestInteractable != null && nearestInteractableSqrDistance < nearestPickableSqrDistance)
            {
                // Interact logic    
                if (!_inventory.TryRemoveLog()) return;

                nearestInteractable.Interact();

                Debug.Log($"[PickableInteractor] Interacting... {nearestCol.name}");
            }

            _inventoryView.Refresh();
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
