using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    public class AxeInteractor : MonoBehaviour
    {
        [SerializeField] private AxeInteractorConfig _config;

        private float _nextHitTime;
        private Vector2 _debugCenter;
        private readonly HashSet<IHittable> _hitThisSwing = new();

        public void TryUse(float facing)
        {
            if (_config == null)
            {
                Debug.LogError("[AxeInteractor] AxeInteractorConfig is not assigned.", this);
                return;
            }

            if (Time.time < _nextHitTime) return;

            _nextHitTime = Time.time + _config.AxeHitDelay;

            AxeUsage(facing);

            StartCoroutine(AxeUsageEffect());
        }

        private void AxeUsage(float direction)
        {
            _hitThisSwing.Clear();
            Collider2D[] colliders = AxeZone(direction);

            foreach (var col in colliders)
            {
                if (col.TryGetComponent<IHittable>(out var damageable)
                    && _hitThisSwing.Add(damageable))
                {
                    Debug.Log($"[Axe] Hitting {col.name}");
                    damageable.TryTakeHit();
                }
            }
        }

        private Collider2D[] AxeZone(float direction)
        {
            Vector2 center = (Vector2)transform.position + _config.Offset * direction;
            _debugCenter = center;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, _config.Size, 0f);
            return colliders;
        }

        private IEnumerator AxeUsageEffect()
        {
            // effects, animation, sound

            yield return new WaitForSeconds(_config.AxeUsageTime);
        }

        private void OnDrawGizmos()
        {
            if (_config == null || !_config.Debug) return;

            Gizmos.DrawCube(_debugCenter, _config.Size);
        }
    }
}
