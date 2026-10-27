using System;
using UnityEngine;

namespace TowerDefence.Core
{
    public class TreeLootSpawner : MonoBehaviour
    {
        [SerializeField] private LogPool _logPool;

        private TreeChoppedEventChannelSO _channel;

        public void Initialize(TreeChoppedEventChannelSO channel)
        {
            _channel = channel;
            _channel.OnEventRaised += HandleChopped;
        }

        private void OnDestroy()
        {
            if (_channel != null)
                _channel.OnEventRaised -= HandleChopped;
        }

        private void HandleChopped(TreeChoppedData data)
        {
            //
            Debug.Log($"[TreeLootSpawner] Spawning loot...");

            for (int i = 0; i < data.Config.LogsCount; i++)
            {
                Log log = _logPool.Get();

                if (log == null) continue;

                log.gameObject.transform.position = data.Position + GenerateRandomOffset();
                log.gameObject.SetActive(true);
            }
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
