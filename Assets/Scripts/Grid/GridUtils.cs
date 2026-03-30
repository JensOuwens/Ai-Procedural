using UnityEngine;

public static class GridUtils
{
    public static bool IsInsideGrid(Cell[,] grid, Vector2Int point)
    {
        int gridWidth = grid.GetLength(0);
        int gridLength = grid.GetLength(1);

        if (point.x <= gridWidth - 1 && point.y <= gridLength - 1)
        {
            return true;
        }
        return false;
    }
}
