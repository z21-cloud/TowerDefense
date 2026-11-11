using TowerDefence.Core;
using UnityEngine;

public class HealthPack : MonoBehaviour, IPickable
{
    [SerializeField] private int pointsToHeal = 20;

    public PickableType Type { get; private set; }

    private void Awake()
    {
        Type = PickableType.HealPack;
    }

    public void Initialize()
    {

    }

    public void PickUp()
    {
        Debug.Log($"[HealhPack] Picked up!");
        gameObject.SetActive(false);
    }
}
