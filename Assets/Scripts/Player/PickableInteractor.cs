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
        // private readonly HashSet<IPickable> _pickables = new();

        public void Initialize()
        {
            _inventory = new Inventory();
        }

        private void Update()
        {
            if(_inventory == null) return;

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                _inventory.TryRemoveLog();
            }
        }

        public void PickUp(float direction = 1f)
        {
            // _pickables.Clear();
            Collider2D[] colliders = PickableZone(direction);

            IPickable nearest = null;
            Collider2D nearestCol = null;
            float nearestSqrDistance = float.MaxValue;

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

                if (!col.TryGetComponent<IPickable>(out var pickable)) continue;

                float sqrDistance = ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

                if (sqrDistance < nearestSqrDistance)
                {
                    nearest = pickable;
                    nearestCol = col;
                    nearestSqrDistance = sqrDistance;
                }
            }

            if (nearest == null) return;

            if(!_inventory.TryAddLog()) return;

            Debug.Log($"[PickableInteractor] PickUping... {nearestCol.name}");
            nearest.TryPickUp();
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
