using System.Collections;
using System.Collections.Generic;
using TowerDefence.PlayerInput;
using UnityEngine;

namespace TowerDefence.Core
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private float _axeUsageTime = 0.2f;
        [SerializeField] private float _axeHitDelay = 0.5f;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;

        private float _timer;
        private readonly HashSet<IDamageable> _hitThisSwing = new();

        private void Awake()
        {
            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_inputManager.GetAttack() && _timer >= _axeHitDelay)
            {
                _timer = 0f;
                StartCoroutine(UseAxe());
            }
        }

        private IEnumerator UseAxe()
        {
            _hitThisSwing.Clear();

            Vector2 center = (Vector2)transform.position + _offset;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, _size, 0f);

            foreach (var col in colliders)
            {
                if (col.TryGetComponent<IDamageable>(out var damageable)
                    && _hitThisSwing.Add(damageable))
                {
                    Debug.Log($"[Axe] Hitting {col.name}");
                    damageable.TryTakeHit();
                }
            }

            yield return new WaitForSeconds(_axeUsageTime);
        }
    }
}
