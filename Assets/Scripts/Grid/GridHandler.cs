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
        
        for(int i=0;i < gridHeight;i++)
        {
            grid[0 , i].tileType = TileType.Wall;
            grid[0 , i].wallType = WallType.Indestructible;
            grid[0, i].contentType = ContentType.None;
            
            grid[gridHeight , i].tileType = TileType.Wall;
            grid[gridHeight , i].wallType = WallType.Indestructible;
            grid[gridHeight, i].contentType = ContentType.None;
        }
        

        for(int i=0;i < gridWidth;i++)
        {
            grid[i , 0].tileType = TileType.Wall;
            grid[i , 0].wallType = WallType.Indestructible;
            grid[i, 0].contentType = ContentType.None;
            
            grid[i , gridWidth].tileType = TileType.Wall;
            grid[i , gridWidth].wallType = WallType.Indestructible;
            grid[i, gridWidth].contentType = ContentType.None;
        }
        
        return grid;
    }
}
