using Unity.VisualScripting;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] Vector2Int minGridSize;
    [SerializeField] Vector2Int maxGridSize;

    public Cell[,] CreateGrid()
    {
        //make grid
        Vector2Int randomGridSize = new Vector2Int(Random.Range(minGridSize.x, maxGridSize.x + 1),
            Random.Range(minGridSize.y, maxGridSize.y + 1));
        
        Cell[,] grid = new  Cell[randomGridSize.x, randomGridSize.y];
        
        for (int x = 0; x < randomGridSize.x; x++)
        {
            for (int y = 0; y < randomGridSize.y; y++)
            {
                grid[x, y] = new Cell(x, y);
            }
        }
        
        //make edges indestructable
        int gridWidth = grid.GetLength(0) - 1;
        int gridHeight = grid.GetLength(1) - 1;
        
        for (int y = 0; y <= gridHeight; y++)
        {
            grid[0, y].tileType = TileType.Wall;
            grid[0, y].wallType = WallType.Indestructible;
            grid[0, y].contentType = ContentType.None;

            grid[gridWidth, y].tileType = TileType.Wall;
            grid[gridWidth, y].wallType = WallType.Indestructible;
            grid[gridWidth, y].contentType = ContentType.None;
        }
        
        for (int x = 0; x <= gridWidth; x++)
        {
            grid[x, 0].tileType = TileType.Wall;
            grid[x, 0].wallType = WallType.Indestructible;
            grid[x, 0].contentType = ContentType.None;

            grid[x, gridHeight].tileType = TileType.Wall;
            grid[x, gridHeight].wallType = WallType.Indestructible;
            grid[x, gridHeight].contentType = ContentType.None;
        }
        
        return grid;
    }
}
