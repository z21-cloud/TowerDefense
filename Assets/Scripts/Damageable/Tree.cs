using UnityEngine;

namespace TowerDefence.Core
{
    // Note: May be in future I'll change this interface and then change IDamageable to IHittable
    public class Tree : MonoBehaviour, IDamageable
    {
        [SerializeField] private TreeConfig _treeConfig;

        private int _maxHits;
        private int _currentHits;

        private TreeChoppedEventChannelSO _channel;
        private TreePool _treePool;

        public void Initialize(TreeChoppedEventChannelSO channel, TreePool treePool)
        {
            _channel = channel;
            _treePool = treePool;
        }

        private void OnEnable()
        {
            // Инициализация параметров дерева, параметры будут определяться из 3 ScriptabeObject
            _maxHits = _treeConfig.MaxHits;

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

            TreeChoppedData treeChoppedData = new TreeChoppedData(_treeConfig, transform.position);
            _channel.RaiseEvent(treeChoppedData);

            _treePool.Return(this);
        }
    }
}
