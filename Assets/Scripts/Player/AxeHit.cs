using UnityEngine;

namespace TowerDefence.Core
{
    public class AxeHit : MonoBehaviour
    {
        // private void OnCollisionEnter2D(Collision2D other) 
        // {
        //     if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        //     {
        //         Debug.Log($"[AxeHit] Hitting {other.gameObject.name}");
        //         damageable.TryTakeHit();
        //     }    
        // }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.Log($"[AxeHit] Hitting {collision.name}");
                damageable.TryTakeHit();
            }  
        }
    }
}
