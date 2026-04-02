using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    private DungeonConfig config;

    public void SetConfig(DungeonConfig cfg) => config = cfg;

    int attempts = 0;
    private int maxAttempts;

    public Cell[,] RandomlyWalk(Cell[,] grid)
    {
        int numberOfSteps = config.numberOfSteps;
        float unvisitedBias = config.unvisitedBias;

        int currentStep = 0;
        attempts = 0;
        maxAttempts = numberOfSteps * 10;

        Vector2Int maxGridPos = new Vector2Int(grid.GetLength(0) - 1, grid.GetLength(1) - 1);

        Vector2Int randomStartPos;

        do
        {
            randomStartPos = new Vector2Int(
                Random.Range(0, maxGridPos.x + 1),
                Random.Range(0, maxGridPos.y + 1)
            );
        }
        while (
            grid[randomStartPos.x, randomStartPos.y].tileType == TileType.Wall &&
            grid[randomStartPos.x, randomStartPos.y].wallType == WallType.Indestructible
        );

        Cell startCell = grid[randomStartPos.x, randomStartPos.y];
        startCell.tileType = TileType.Floor;
        grid[randomStartPos.x, randomStartPos.y] = startCell;

        Cell currentCell = startCell;

        while (currentStep < numberOfSteps && attempts < maxAttempts)
        {
            attempts++;

            Vector2Int nextPos;
            Cell nextCell;

            int safety = 0;

            do
            {
                Vector2Int stepDirection;

                if (Random.value < unvisitedBias)
                {
                    stepDirection = GetBiasedDirection(currentCell, grid);
                }
                else
                {
                    int StepDirectionID = Random.Range(0, 4);

                    stepDirection = StepDirectionID switch
                    {
                        0 => Vector2Int.left,
                        1 => Vector2Int.right,
                        2 => Vector2Int.down,
                        _ => Vector2Int.up
                    };
                }

                nextPos = currentCell.position + stepDirection;

                safety++;

                if (safety > 6)
                {
                    nextPos = GetRandomFloor(grid);
                    break;
                }

            }
            while (!GridUtils.IsInsideGrid(grid, nextPos) ||
                   grid[nextPos.x, nextPos.y].wallType == WallType.Indestructible);

            nextCell = grid[nextPos.x, nextPos.y];

            currentStep++;

            currentCell = nextCell;
            currentCell.tileType = TileType.Floor;
            grid[currentCell.position.x, currentCell.position.y] = currentCell;
        }

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

    private Vector2Int GetBiasedDirection(Cell currentCell, Cell[,] grid)
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.up
        };

        Vector2Int best = directions[Random.Range(0, directions.Length)];
        int bestScore = -1;

        foreach (var dir in directions)
        {
            Vector2Int pos = currentCell.position + dir;

            if (!GridUtils.IsInsideGrid(grid, pos))
                continue;

            Cell c = grid[pos.x, pos.y];
            int score = (c.tileType == TileType.Floor) ? 0 : 1;

            if (score > bestScore)
            {
                bestScore = score;
                best = dir;
            }
        }

        return best;
    }

    private Vector2Int GetRandomFloor(Cell[,] grid)
    {
        int w = grid.GetLength(0);
        int h = grid.GetLength(1);

        for (int i = 0; i < 20; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);

            if (grid[x, y].tileType == TileType.Floor)
                return new Vector2Int(x, y);
        }

        return new Vector2Int(0, 0);
    }
}