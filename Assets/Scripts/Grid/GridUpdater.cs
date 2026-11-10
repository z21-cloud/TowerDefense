using UnityEngine;

namespace TowerDefence.Core
{
    // Пересчитывает клетки сетки по событию рубки дерева: без этого клетка
    // срубленного дерева осталась бы непроходимой до конца сессии.
    public class GridUpdater : MonoBehaviour
    {
        [SerializeField] private Grid _grid;

        private TreeChoppedEventChannelSO _channel;

        public void Initialize(TreeChoppedEventChannelSO channel)
        {
            if (_grid == null || channel == null)
            {
                Debug.LogError("[GridUpdater] Grid or event channel reference is not set.");
                enabled = false;
                return;
            }

            _channel = channel;
            _channel.OnEventRaised += HandleTreeChopped;
        }

        private void OnDestroy()
        {
            if (_channel != null)
                _channel.OnEventRaised -= HandleTreeChopped;
        }

        private void HandleTreeChopped(TreeChoppedData data)
        {
            _grid.UpdateCellAt(data.Position);
        }
    }
}
