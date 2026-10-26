using UnityEngine;

namespace TowerDefence.Core
{
    // Note: May be in future I'll change this interface and then change IDamageable to IHittable
    public class Tree : MonoBehaviour, IDamageable
    {
        [SerializeField] private TreeConfig _treeConfig;
        [SerializeField] private LogPool _logPool;

        private int _maxHits;
        private int _logsCount;
        private int _currentHits;

        public void Initialize()
        {
            
        }

        private void Awake()
        {
            // Инициализация параметров дерева, параметры будут определяться из 3 ScriptabeObject
            _maxHits = _treeConfig.MaxHits;
            _logsCount = _treeConfig.LogsCount;
            
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

            for(int i = 0; i < _logsCount; i++)
            {
                Vector3 offset = GenerateRandomOffset();
                Log log = _logPool.Get();
                log.gameObject.transform.position = transform.position + offset;
                log.gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        private Vector3 GenerateRandomOffset()
        {
            float x = UnityEngine.Random.Range(-1f, 1f);
            float y = UnityEngine.Random.Range(-1f, 1f);
            float z = 0f;

            return new Vector3(x, y, z);
        }
    }
}
