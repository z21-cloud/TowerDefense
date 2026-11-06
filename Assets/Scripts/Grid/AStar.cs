using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private Grid _grid;

    public AStar(Grid grid)
    {
        _grid = grid;
    }

    public List<GridCell> FindPath(GridCell startNode, GridCell endNode)
    {
        ResetNodes();

        if (startNode == null || endNode == null)
        {
            Debug.LogError("Start or End node is null.");
            return null;
        }

        if (endNode.isWalkable == false)
        {
            // Цель может стоять на коллайдере (например, костёр) — ищем ближайшую проходимую клетку.
            endNode = FindNearestWalkable(endNode);
            if (endNode == null)
            {
                Debug.LogError("[AStar] End node is not walkable and no walkable cell found nearby.");
                return null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = Heuristic(startNode, endNode);

        PriorityQueue<GridCell> queue = new();
        HashSet<GridCell> visited = new();

        queue.Enqueue(startNode, startNode.fCost);

        while (queue.Count > 0)
        {
            GridCell currentNode = queue.Dequeue();
            visited.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (GridCell neighbor in FindNeighbors(currentNode))
            {
                if (neighbor.isWalkable == false || visited.Contains(neighbor))
                    continue;

                float tentativeGCost = currentNode.gCost + GetMovementCost(currentNode, neighbor);
                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = Heuristic(neighbor, endNode);
                    neighbor.parent = currentNode;
                    queue.Enqueue(neighbor, neighbor.fCost);
                }
            }
        }

        Debug.LogError("No path found.");
        return null;
    }

    private void ResetNodes()
    {
        foreach (var cell in _grid.GetAllCells())
        {
            cell.gCost = float.MaxValue;
            cell.hCost = 0f;
            cell.parent = null;
        }
    }

    private float Heuristic(GridCell a, GridCell b)
    {
        int dx = Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
        int dy = Mathf.Abs(a.gridPosition.y - b.gridPosition.y);

        float diagonal = 1.4f;
        float straight = 1.0f;

        return straight * (dx + dy) + (diagonal - 2 * straight) * Mathf.Min(dx, dy);
    }

    private List<GridCell> FindNeighbors(GridCell node)
    {
        List<GridCell> neighbors = new List<GridCell>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),   // Up
            new Vector2Int(1, 0),   // Right
            new Vector2Int(0, -1),  // Down
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(1, 1),   // Up-Right
            new Vector2Int(1, -1),  // Down-Right
            new Vector2Int(-1, -1), // Down-Left
            new Vector2Int(-1, 1)   // Up-Left
        };

        foreach (var dir in directions)
        {
            Vector2Int neighborPos = node.gridPosition + dir;
            if (_grid.TryGetCell(neighborPos, out GridCell neighbor) && neighbor.isWalkable)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    private float GetMovementCost(GridCell fromNode, GridCell toNode)
    {
        int dx = Mathf.Abs(fromNode.gridPosition.x - toNode.gridPosition.x);
        int dy = Mathf.Abs(fromNode.gridPosition.y - toNode.gridPosition.y);

        bool isDiagonal = dx == 1 && dy == 1;
        return isDiagonal ? toNode.cost * 1.4f : toNode.cost * 1.0f; // диагональ дороже  прямо — обычная стоимость
    }

    private GridCell FindNearestWalkable(GridCell node)
    {
        const int maxRadius = 5;

        for (int radius = 1; radius <= maxRadius; radius++)
        {
            for (int dx = -radius; dx <= radius; dx++)
            {
                for (int dy = -radius; dy <= radius; dy++)
                {
                    // Проверяем только кольцо текущего радиуса, внутренние клетки уже проверены.
                    if (Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy)) != radius)
                        continue;

                    Vector2Int position = node.gridPosition + new Vector2Int(dx, dy);
                    if (_grid.TryGetCell(position, out GridCell cell) && cell.isWalkable)
                        return cell;
                }
            }
        }

        return null;
    }

    private List<GridCell> RetracePath(GridCell startNode, GridCell endNode)
    {
        List<GridCell> path = new List<GridCell>();
        GridCell currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }
}
