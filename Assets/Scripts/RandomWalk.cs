using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField] private int numberOfSteps;
    int attempts = 0;
    private int maxAttempts;
    

    public Cell[,] RandomlyWalk(Cell[,] grid)
    {
        int currentStep = 0;
        attempts = 0;
        maxAttempts = numberOfSteps * 10;
        
        //grab random position in the grid
        Vector2Int maxGridPos = new Vector2Int(grid.GetLength(0) - 1, grid.GetLength(1) - 1);

        Vector2Int randomStartPos;

        do
        {
            randomStartPos = new Vector2Int(
                Random.Range(0, maxGridPos.x+1),
                Random.Range(0, maxGridPos.y+1)
            );
        }
        while (
            grid[randomStartPos.x, randomStartPos.y].tileType == TileType.Wall &&
            grid[randomStartPos.x, randomStartPos.y].wallType == WallType.Indestructible
        );
        
        
        Cell startCell =  grid[randomStartPos.x, randomStartPos.y];
        startCell.tileType = TileType.Floor;
        grid[randomStartPos.x, randomStartPos.y] = startCell;
        
        Cell currentCell = startCell;

        while (currentStep < numberOfSteps && attempts < maxAttempts)
        {
            attempts++;
            //take a step
            int StepDirectionID = Random.Range(0, 4);
            Vector2Int stepDirection = Vector2Int.zero;
            
            switch (StepDirectionID)
            {
                case 0: stepDirection = Vector2Int.left; break;
                case 1: stepDirection = Vector2Int.right; break;
                case 2: stepDirection = Vector2Int.down; break;
                case 3: stepDirection = Vector2Int.up; break;
            }
            
            Vector2Int nextPos = currentCell.position + stepDirection;

            if (!GridUtils.IsInsideGrid(grid, nextPos))
                continue;

            Cell nextCell = grid[nextPos.x, nextPos.y];

            //check if step is in bounds and if its not indestructable
            if (nextCell.wallType == WallType.Indestructible)
            {
                continue;
            }

            //if so, add to the step counter
            currentStep++;
            
            //save the step into the grid
            currentCell = nextCell;
            currentCell.tileType = TileType.Floor;
            grid[currentCell.position.x, currentCell.position.y] = currentCell;
        }

        //change the cells in the path, any cell that isnt in the path becomes a diggable wall (only if it isnt indestructable), any cell that is a path becomes a floor
        foreach (Cell cell in grid)
        {
            if (cell.tileType != TileType.Floor && cell.wallType != WallType.Indestructible)
            {
                currentCell = cell;
                
                currentCell.tileType = TileType.Wall;
                currentCell.wallType = WallType.Diggable;
                currentCell.contentType = ContentType.None;
                
                grid[cell.position.x, cell.position.y] = currentCell;
            }
        }

        return grid;
    }
}
