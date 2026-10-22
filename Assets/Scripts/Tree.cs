using UnityEngine;

namespace TowerDefence.Core
{
    // Note: May be in future I'll change this interface and then change IDamageable to IHittable
    public class Tree : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHits = 4;
        [SerializeField] private int _logsCount = 1;

        private int _currentHits;

        private void Awake()
        {
            // Инициализация параметров дерева, параметры будут определяться из 3 ScriptabeObject
            _currentHits = 0;
        }

        public bool TryTakeHit()
        {
            if (_currentHits < _maxHits)
            {
                TakeHit();
                return true;
            }
            else
            {
                Fall();
                return false;
            }
        }


        private void TakeHit()
        {
            _currentHits++;

            Debug.Log($"[Tree] Current hits - {_currentHits}. Hits needed - {_maxHits}");

            // effects
            // sound
        }

        private void Fall()
        {
            Debug.Log("$[Tree] Falling...");
            gameObject.SetActive(false);
        }
    }
}
