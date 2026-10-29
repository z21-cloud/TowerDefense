using TowerDefence.Core;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _initialiCount = 100;
    [SerializeField] private BulletFactory _bulletFactory;

    private ObjectPooling<Bullet> _pool;

    public void Initialize()
    {
        _pool = new ObjectPooling<Bullet>(_bulletFactory, _initialiCount);
    }

    public Bullet Get()
    {
        Bullet bullet = _pool.Get();

        if (bullet == null)
        {
            Debug.LogError($"[BulletPool] Pool is Empty! Return null...");
            return null;
        }

        // GameObject is not active!
        return bullet;
    }

    public void Return(Bullet bullet)
    {
        _pool.Return(bullet);
    }
}
