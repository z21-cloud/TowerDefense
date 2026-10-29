using UnityEngine;

namespace TowerDefence.Core
{
    [CreateAssetMenu(fileName = "TreeConfig", menuName = "TowerDefense/TreeConfig")]
    public class TreeConfig : ScriptableObject
    {
        [SerializeField] private int _maxHits;
        [SerializeField] private int _logsCount;

        public int MaxHits => _maxHits;
        public int LogsCount => _logsCount;
    }
}