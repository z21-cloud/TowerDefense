using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private List<InventorySlotView> _slots;

    private Inventory _inventory;

    public void Initalize(Inventory inventory)
    {
        _inventory = inventory;
        Refresh();
    }

    public void Refresh()
    {
        int slotsCount = _slots.Count;
        for(int i = 0; i < slotsCount; i++)
        {
            InventoryCell cell = _inventory.GetCell(i);

            if(cell == null || cell.Type == CellType.Empty) continue;

            _slots[i].SetText(cell.Type.ToString() + " " + cell.Count.ToString()); 
        }
    }
}
