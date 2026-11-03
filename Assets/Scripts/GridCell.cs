using UnityEngine;

public class GridCell
{
    public Vector2 position;
    public Vector2Int gridPosition;
    public bool isWalkable;

    public GridCell(Vector2 position, Vector2Int gridPosition, bool isWalkable)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.isWalkable = isWalkable;
    }
}
