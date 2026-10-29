using UnityEngine;

public class Weapon : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Transform _bulletPivot;
    [SerializeField] private int _damage = 10;

    public void DealDamage(float direction = 0)
    {
        Shoot();
    }

    private void Shoot()
    {
        Bullet bullet = _bulletPool.Get();

        bullet.SetConfiguration(_bulletPivot.position, _bulletSpeed, _damage);
        bullet.transform.position = _bulletPivot.position;

        bullet.gameObject.SetActive(true);

        Physics2D.SyncTransforms();
    }
}
