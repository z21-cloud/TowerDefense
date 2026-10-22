namespace TowerDefence.Core
{
    // Note: Maybe change this interface to IDamageable TryTakeDamage(int damage), 
    // and create another interface for HittableObject IHittable
    public interface IDamageable
    {
        public bool TryTakeHit();
    }
}
