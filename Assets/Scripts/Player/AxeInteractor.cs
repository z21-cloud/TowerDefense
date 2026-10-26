using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    public class AxeInteractor : MonoBehaviour
    {
        [SerializeField] private float _axeUsageTime = 0.2f;
        [SerializeField] private float _axeHitDelay = 0.5f;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private bool _debug;

        private float _nextHitTime;
        private Vector2 _debugCenter;
        private readonly HashSet<IDamageable> _hitThisSwing = new();

        public void TryUse(float facing)
        {
            if (Time.time < _nextHitTime) return;

            _nextHitTime = Time.time + _axeHitDelay;

            AxeUsage(facing);

            StartCoroutine(AxeUsageEffect());
        }

        private void AxeUsage(float direction)
        {
            _hitThisSwing.Clear();
            Collider2D[] colliders = AxeZone(direction);

            foreach (var col in colliders)
            {
                if (col.TryGetComponent<IDamageable>(out var damageable)
                    && _hitThisSwing.Add(damageable))
                {
                    Debug.Log($"[Axe] Hitting {col.name}");
                    damageable.TryTakeHit();
                }
            }
        }

        private Collider2D[] AxeZone(float direction)
        {
            Vector2 center = (Vector2)transform.position + _offset * direction;
            _debugCenter = center;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, _size, 0f);
            return colliders;
        }

        private IEnumerator AxeUsageEffect()
        {
            // effects, animation, sound

            yield return new WaitForSeconds(_axeUsageTime);
        }

        private void OnDrawGizmos()
        {
            if (!_debug) return;

            Gizmos.DrawCube(_debugCenter, _size);
        }
    }
}
