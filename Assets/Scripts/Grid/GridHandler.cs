using Unity.VisualScripting;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] Vector2Int minGridSize;
    [SerializeField] Vector2Int maxGridSize;

    public Cell[,] CreateGrid()
    {
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
        
        return grid;
    }
}
