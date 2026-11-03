using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamageDealer
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Transform _bulletPivot;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _currentAmmo = 2;
    [SerializeField] private int _maxAmmo = 2;
    [SerializeField] private float _reloadingTime = 1f;

    private bool _isReloading;

    // События смены состояния: подписчикам не нужно знать о GameObject оружия
    public event Action Activated;
    public event Action Deactivated;

    // Компонент сам управляет своим объектом (он вынесен в отдельный дочерний объект)
    public void Activate()
    {
        gameObject.SetActive(true);
        Activated?.Invoke();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Deactivated?.Invoke();
    }

    private void OnEnable()
    {
        if(_bulletPool == null)
        {
            Debug.LogError($"[Weapon] Bullet pool is not assigned!");
            return; 
        }

        if(_currentAmmo == 0)
        {
            _currentAmmo = _maxAmmo;
            _isReloading = false;
        }
    }

    public void DealDamage(float direction = 0)
    {
        if(_isReloading) return;

        if(_currentAmmo <= 0)
        {
            Debug.Log($"[Weapon: DealDamage] No ammo left! Reloading...");
            StartCoroutine(Reload());
            return;
        }

        _currentAmmo--;
        Shoot();
    }

    private void Shoot()
    {
        Bullet bullet = _bulletPool.Get();

        bullet.SetConfiguration(_bulletPivot.right, _bulletSpeed, _damage);
        bullet.transform.position = _bulletPivot.position;

        bullet.gameObject.SetActive(true);

        Physics2D.SyncTransforms();
    }

    private IEnumerator Reload()
    {
        _isReloading = true;

        Debug.Log($"[Weapon: Reload] Reloading...");
        yield return new WaitForSeconds(_reloadingTime);
        _currentAmmo = _maxAmmo;

        _isReloading = false;
    }
}
