using UnityEngine;

public static class GridUtils
{
    public static bool IsInsideGrid(Cell[,] grid, Vector2Int point)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        return point.x >= 0 && point.y >= 0 &&
               point.x < width && point.y < height;
    }
}
