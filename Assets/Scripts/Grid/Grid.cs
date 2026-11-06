using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2Int cellSize;
    [SerializeField] private LayerMask unwalkableLayer;

    private Vector2 originMin;
    private Vector2Int gridSize;

    private Dictionary<Vector2Int, GridCell> gridCells = new();

    private void Awake()
    {
        originMin = spriteRenderer.bounds.min;

        gridSize = new Vector2Int(
            Mathf.FloorToInt(spriteRenderer.bounds.size.x / cellSize.x),
            Mathf.FloorToInt(spriteRenderer.bounds.size.y / cellSize.y)
        );

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                Vector2 cellPosition = new Vector2(
                    originMin.x + (x * cellSize.x) + (cellSize.x / 2f),
                    originMin.y + (y * cellSize.y) + (cellSize.y / 2f)
                );

                bool isWalkable = CheckWalkable(cellPosition, gridPosition);

                float tempCost = isWalkable ? 1f : float.MaxValue; // Assign a high cost for unwalkable cells

                GridCell cell = new GridCell(cellPosition, gridPosition, isWalkable, tempCost);
                gridCells.Add(gridPosition, cell);
            }
        }
    }

    public void FindPath(Vector2 startWorldPosition, Vector2 endWorldPosition, out List<Vector2> path)
    {
        Vector2Int startGridPosition = WorldToGridPosition(startWorldPosition);
        Vector2Int endGridPosition = WorldToGridPosition(endWorldPosition);

        if (!gridCells.TryGetValue(startGridPosition, out GridCell startCell) || !gridCells.TryGetValue(endGridPosition, out GridCell endCell))
        {
            Debug.LogError($"[Grid] Start {startWorldPosition} or end {endWorldPosition} is outside the grid.");
            path = null;
            return;
        }

        AStar aStar = new AStar(this);
        List<GridCell> cellPath = aStar.FindPath(startCell, endCell);

        if (cellPath != null)
        {
            path = new List<Vector2>();
            foreach (var cell in cellPath)
            {
                path.Add(cell.position);
            }
        }
        else
        {
            path = null;
            Debug.LogError("No path found.");
        }
    }

    private Vector2Int WorldToGridPosition(Vector2 worldPosition)
    {
        int x = Mathf.Clamp(Mathf.FloorToInt((worldPosition.x - originMin.x) / cellSize.x), 0, gridSize.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((worldPosition.y - originMin.y) / cellSize.y), 0, gridSize.y - 1);
        return new Vector2Int(x, y);
    }


    private bool CheckWalkable(Vector2 position, Vector2Int gridPosition)
    {
        // Бокс чуть меньше клетки, чтобы коллайдер на границе не блокировал соседнюю клетку.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, (Vector2)cellSize * 0.9f, 0f, unwalkableLayer);

        return colliders.Length == 0; // If no collider is found, the cell is walkable
    }

    public bool TryGetCell(Vector2Int gridPosition, out GridCell cell)
    {
        return gridCells.TryGetValue(gridPosition, out cell);
    }

    public IEnumerable<GridCell> GetAllCells()
    {
        return gridCells.Values;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                if (gridCells.TryGetValue(gridPosition, out var cell))
                {
                    Gizmos.color = cell.isWalkable ? Color.green : Color.red;
                    Gizmos.DrawWireCube(cell.position, new Vector3(cellSize.x, cellSize.y, 0f));
                }
            }
        }
    }

}
