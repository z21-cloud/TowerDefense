using UnityEngine;

public class GridCell
{
    public Vector2 position; // The world position of the cell
    public Vector2Int gridPosition; // The position of the cell in the grid (row, column)
    
    public GridCell parent; // The Node that this cell came from in the pathfinding process

    public bool isWalkable; // Whether the cell can be traversed or not

    // A* algorithm costs
    public float cost; // The cost to move through this cell (e.g., terrain cost)
    public float gCost; // The cost from the start node to this cell
    public float hCost; // The heuristic cost from this cell to the end node
    public float fCost => gCost + hCost; // The total cost of the cell (gCost + hCost)

    public GridCell(Vector2 position, Vector2Int gridPosition, bool isWalkable, float cost, GridCell parent = null)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.isWalkable = isWalkable;
        this.cost = cost;
        this.parent = parent;

        gCost = float.MaxValue;
        hCost = 0f;
    }
}
