using UnityEngine;

namespace TowerDefence.Core
{
    public readonly struct TreeChoppedData
    {
        public readonly TreeConfig Config;
        public readonly Vector3 Position;

        public TreeChoppedData(TreeConfig config, Vector3 position)
        {
            Config = config;
            Position = position;
        }
    }
}
