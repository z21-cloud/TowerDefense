using UnityEngine;

namespace TowerDefence.Core
{
    public class FlipComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _spriteRenderers;

        private void Awake()
        {
            if (_spriteRenderers == null || _spriteRenderers.Length == 0)
            {
                Debug.LogError("[FlipComponent] No SpriteRenderers assigned.", this);
                enabled = false;
                return;
            }

            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                if (spriteRenderer == null)
                {
                    Debug.LogError("[FlipComponent] A SpriteRenderer reference is missing.", this);
                    enabled = false;
                    return;
                }
            }
        }

        public void SetFacing(float horizontalDirection)
        {
            if (!enabled || horizontalDirection == 0f)
            {
                return;
            }

            bool flipX = horizontalDirection < 0f;
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.flipX = flipX;
            }
        }
    }
}
