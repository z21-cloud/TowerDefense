using System;
using UnityEngine;

public class Inventory
{
    private readonly int _cellsCount = 16;
    private readonly InventoryCell _axe;
    private readonly InventoryCell _shotgun;
    private readonly InventoryCell[] _inventory;
    private int _logsCount;

    public int LogsCount => _logsCount;
    public int CellsCount => _inventory.Length;

    public Inventory()
    {
        _inventory = new InventoryCell[_cellsCount];

        _shotgun = new InventoryCell(CellType.Shotgun, 1);
        _axe = new InventoryCell(CellType.Axe, 1);

        _inventory[0] = _axe;
        _inventory[1] = _shotgun;

        for (int i = 2; i < _inventory.Length; i++)
        {
            InventoryCell emptyCell = new InventoryCell(CellType.Empty, 0);
            _inventory[i] = emptyCell;
        }

        // Debug.Log($"[Inventory] Inventory created! Initialized cells: {_inventory.Length}");

        // for (int i = 0; i < _inventory.Length; i++)
        // {
        //     Debug.Log($"[Inventory] Cell index : {i}. Cell Type : {_inventory[i].Type}");
        // }
    }

    public bool TryAddLog()
    {
        for (int i = 2; i < _inventory.Length; i++)
        {
            if (_inventory[i].Type == CellType.Empty || 
                (_inventory[i].Type == CellType.Log && _inventory[i].Count < 2))
            {
                AddLog(i);
                return true;
            }
        }

        Debug.Log($"[Inventory] Inventory is full!");
        return false;
    }

    public bool TryRemoveLog()
    {
        for (int i = _inventory.Length - 1; i > 1; i--)
        {
            if (_inventory[i].Type == CellType.Log && _inventory[i].Count > 0)
            {
                RemoveLog(i);
                return true;
            }
        }

        Debug.Log($"[Inventory] Inventory is empty!");
        return false;
    }

    private void AddLog(int index)
    {
        _logsCount++;

        _inventory[index].Type = CellType.Log;
        _inventory[index].Count++;
        
        Debug.Log($"[Inventory] Log added successfully! Index - {index}, Count - {_inventory[index].Count}");
        Debug.Log($"[Inventory] Current logs count: {LogsCount}");
    }

    private void RemoveLog(int index)
    {
        _logsCount--;
        
        _inventory[index].Count--;

        if(_inventory[index].Count <= 0) _inventory[index].Type = CellType.Empty;

        Debug.Log($"[Inventory] Log removed successfully! Index - {index}, Count - {_inventory[index].Count}");
        Debug.Log($"[Inventory] Current logs count: {LogsCount}");
    }

    public InventoryCell GetCell(int index)
    {
        if(index >= 0 && index < _inventory.Length) return _inventory[index];

        Debug.LogWarning($"[Inventory] Wrong index: {index}!");
        return null;
    }
}