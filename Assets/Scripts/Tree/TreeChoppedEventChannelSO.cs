using UnityEngine;

namespace TowerDefence.Core
{
    [CreateAssetMenu(fileName = "TreeChoppedEventChannelSO", menuName = "TowerDefense/Tree Chopped Event Channel")]
    public class TreeChoppedEventChannelSO : ScriptableObject
    {
        public event System.Action<TreeChoppedData> OnEventRaised;
        public void RaiseEvent(TreeChoppedData treeData) => OnEventRaised?.Invoke(treeData);
    }
}
