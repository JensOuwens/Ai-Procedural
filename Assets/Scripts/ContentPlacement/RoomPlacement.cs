using System.Collections.Generic;
using UnityEngine;

public class RoomPlacement : MonoBehaviour
{
    [SerializeField] private int minRooms;
    [SerializeField] private int maxRooms;
    [SerializeField] private Vector2Int minRoomSize;
    [SerializeField] private Vector2Int maxRoomSize;
    [SerializeField] private int spawnChance;
    [SerializeField] private int maxAttempts;
    [SerializeField] private int minOffsetValue;
    [SerializeField] private int maxOffsetValue;
    
    private List<Room> rooms = new List<Room>();
    private HashSet<Vector2Int> occupied = new HashSet<Vector2Int>();

    private int attempts = 0;
    private Cell[,] path;
    private Cell[,] grid;

    public List<Room> generateRooms(Cell[,] grid)
    {
        attempts = 0;
        rooms.Clear();
        occupied.Clear();

        this.grid = grid;
        path = new Cell[grid.GetLength(0), grid.GetLength(1)];
        
        while (attempts < maxAttempts && rooms.Count < maxRooms)
        {
            attempts++;

            foreach (Cell cell in grid)
            {
                if (cell.tileType != TileType.Floor)
                    continue;

                path[cell.position.x, cell.position.y] = cell;

                if (Random.Range(0f, 100f) > spawnChance)
                    continue;

                Vector2Int size = new Vector2Int(
                    Random.Range(minRoomSize.x, maxRoomSize.x),
                    Random.Range(minRoomSize.y, maxRoomSize.y)
                );

                int roomWidth = size.x;
                int roomHeight = size.y;

                int halfWidth = roomWidth / 2;
                int halfHeight = roomHeight / 2;

                Cell[,] roomCells = new Cell[roomWidth, roomHeight];

                bool invalid = false;

                for (int x = 0; x < roomWidth; x++)
                {
                    for (int y = 0; y < roomHeight; y++)
                    {
                        int gridX = cell.position.x - halfWidth + x;
                        int gridY = cell.position.y - halfHeight + y;

                        Vector2Int pos = new Vector2Int(gridX, gridY);

                        if (!GridUtils.IsInsideGrid(grid, pos))
                        {
                            invalid = true;
                            break;
                        }

                        if (grid[gridX, gridY].wallType == WallType.Indestructible)
                        {
                            invalid = true;
                            break;
                        }

                        if (occupied.Contains(pos))
                        {
                            invalid = true;
                            break;
                        }

                        roomCells[x, y] = grid[gridX, gridY];
                    }

                    if (invalid) break;
                }

                if (invalid)
                    continue;

                Room roomAttempt = new Room(cell.position, roomCells);

                rooms.Add(roomAttempt);

                // mark occupied tiles
                foreach (Cell c in roomAttempt.tiles)
                {
                    occupied.Add(c.position);
                }

                if (rooms.Count >= maxRooms)
                    return rooms;
            }
        }

        int safety = 0;
        int maxSafety = 1000;

        if (rooms.Count < minRooms)
        {
            do
            {
                safety++;
                if (safety > maxSafety)
                    break;

                foreach (Cell cell in grid)
                {
                    if (cell.tileType != TileType.Floor)
                        continue;

                    path[cell.position.x, cell.position.y] = cell;

                    Vector2Int size = new Vector2Int(
                        Random.Range(minRoomSize.x, maxRoomSize.x),
                        Random.Range(minRoomSize.y, maxRoomSize.y)
                    );

                    int roomWidth = size.x;
                    int roomHeight = size.y;

                    int halfWidth = roomWidth / 2;
                    int halfHeight = roomHeight / 2;

                    Cell[,] roomCells = new Cell[roomWidth, roomHeight];

                    bool invalid = false;

                    for (int x = 0; x < roomWidth; x++)
                    {
                        for (int y = 0; y < roomHeight; y++)
                        {
                            int gridX = cell.position.x - halfWidth + x;
                            int gridY = cell.position.y - halfHeight + y;

                            Vector2Int pos = new Vector2Int(gridX, gridY);

                            if (!GridUtils.IsInsideGrid(grid, pos))
                            {
                                invalid = true;
                                break;
                            }

                            if (grid[gridX, gridY].wallType == WallType.Indestructible)
                            {
                                invalid = true;
                                break;
                            }

                            if (occupied.Contains(pos))
                            {
                                invalid = true;
                                break;
                            }

                            roomCells[x, y] = grid[gridX, gridY];
                        }

                        if (invalid) break;
                    }

                    if (invalid)
                        continue;

                    Room roomAttempt = new Room(cell.position, roomCells);

                    rooms.Add(roomAttempt);

                    foreach (Cell c in roomAttempt.tiles)
                    {
                        occupied.Add(c.position);
                    }

                    if (rooms.Count >= maxRooms)
                        return rooms;
                }

            } while (rooms.Count < minRooms);
        }

        return rooms;
    }
}