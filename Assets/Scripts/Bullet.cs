using TowerDefence.Core;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private int _damage;
    private float _speed;
    private float _timer;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private BulletPool _bulletPool;
    private bool _isEnemyOwner = false;
    // private GameObject _owner;

    private readonly float _lifeTime = 20f;

    public void Initialzie(BulletPool bulletPool)
    {
        if(!TryGetComponent<Rigidbody2D>(out var rigidbody2D))
        {
            Debug.LogError($"[Bullet] RigidBody2D is null! Error expected");
            return; 
        }

        if(bulletPool == null)
        {
            Debug.LogError($"[Bullet] Bullet pool is not assigned!");
            return; 
        }

        _rb = rigidbody2D;
        _timer = 0f;
        _bulletPool = bulletPool;
    }

    public void SetConfiguration(
        Vector2 direction, 
        float speed, 
        int damage)
        // GameObject owner)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        //  _owner = owner;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction * _speed;

        _timer += Time.fixedDeltaTime;

        if(_timer >= _lifeTime)
        {
            _timer = 0f;
            _bulletPool.Return(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out _) && !_isEnemyOwner) return;

        if(collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_damage);
        }
    }
}
