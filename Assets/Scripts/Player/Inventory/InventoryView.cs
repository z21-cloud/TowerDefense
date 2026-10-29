using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private List<InventorySlotView> _slots;

    private Inventory _inventory;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void OpenInventory(bool value)
    {
        Refresh();

        gameObject.SetActive(value);
    }


    private void Refresh()
    {
        int slotsCount = _slots.Count;
        for(int i = 0; i < slotsCount; i++)
        {
            InventoryCell cell = _inventory.GetCell(i);

            if(cell == null) continue;

            _slots[i].SetText(cell.Type.ToString() + " " + cell.Count.ToString()); 
        }
    }
}
