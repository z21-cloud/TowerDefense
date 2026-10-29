using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Core
{
    public class AxeInteractor : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private AxeInteractorConfig _config;

        private float _nextHitTime;
        private Vector2 _debugCenter;

        public void DealDamage(float direction)
        {
            TryUseAxe(direction);
        }

        private void TryUseAxe(float facing)
        {
            if (_config == null)
            {
                Debug.LogError("[AxeInteractor] AxeInteractorConfig is not assigned.", this);
                return;
            }

            if (Time.time < _nextHitTime) return;

            _nextHitTime = Time.time + _config.AxeHitDelay;

            AxeHit(facing);

            StartCoroutine(AxeHitEffect());
        }

        private void AxeHit(float direction)
        {
            Collider2D[] colliders = AxeZone(direction);

            Collider2D nearestCol = null;

            float nearestSqrDistance = float.MaxValue;

            foreach (var col in colliders)
            {
                if ((!col.TryGetComponent<IHittable>(out _) && !col.TryGetComponent<IDamageable>(out _)) 
                    || col.TryGetComponent<PlayerController>(out _)) continue;

                float sqrDistance = ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

                if (sqrDistance < nearestSqrDistance)
                {
                    nearestCol = col;
                    nearestSqrDistance = sqrDistance;
                }
            }

            if (nearestCol == null) return;

            if (nearestCol.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(10);

                Debug.Log($"[AxeInteractor] Damaging... {nearestCol.name}");
            }

            else if (nearestCol.TryGetComponent<IHittable>(out var hittable))
            {
                hittable.TryTakeHit();

                Debug.Log($"[AxeInteractor] Hitting... {nearestCol.name}");
            }
        }

        private Collider2D[] AxeZone(float direction)
        {
            Vector2 center = (Vector2)transform.position + _config.Offset * direction;
            _debugCenter = center;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, _config.Size, 0f);
            return colliders;
        }

        private IEnumerator AxeHitEffect()
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
