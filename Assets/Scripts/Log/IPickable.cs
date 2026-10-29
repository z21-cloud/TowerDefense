namespace TowerDefence.Core
{
    public interface IPickable
    {
        public PickableType Type {get; }
        public void PickUp();
    }
}
