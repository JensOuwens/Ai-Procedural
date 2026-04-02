using System.Collections.Generic;
using UnityEngine;

public class RoomPlacement : MonoBehaviour
{
    private DungeonConfig config;

    public void SetConfig(DungeonConfig cfg) => config = cfg;

    private List<Room> rooms = new List<Room>();
    private HashSet<Vector2Int> occupied = new HashSet<Vector2Int>();

    public List<Room> generateRooms(Cell[,] grid)
    {
        int minRooms = config.minRooms;
        int maxRooms = config.maxRooms;
        Vector2Int minRoomSize = config.minRoomSize;
        Vector2Int maxRoomSize = config.maxRoomSize;
        int spawnChance = config.roomSpawnChance;
        int maxAttempts = config.roomMaxAttempts;

        rooms.Clear();
        occupied.Clear();

        int attempts = 0;

        while (attempts < maxAttempts && rooms.Count < maxRooms)
        {
            attempts++;

            foreach (Cell cell in grid)
            {
                if (cell.tileType != TileType.Floor)
                    continue;

                if (Random.Range(0f, 100f) > spawnChance)
                    continue;

                Vector2Int size = new Vector2Int(
                    Random.Range(minRoomSize.x, maxRoomSize.x),
                    Random.Range(minRoomSize.y, maxRoomSize.y)
                );

                int halfWidth = size.x / 2;
                int halfHeight = size.y / 2;

                List<Vector2Int> roomCells = new List<Vector2Int>();
                bool invalid = false;

                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        int gridX = cell.position.x - halfWidth + x;
                        int gridY = cell.position.y - halfHeight + y;

                        Vector2Int pos = new Vector2Int(gridX, gridY);

                        if (!GridUtils.IsInsideGrid(grid, pos) ||
                            grid[gridX, gridY].wallType == WallType.Indestructible ||
                            occupied.Contains(pos))
                        {
                            invalid = true;
                            break;
                        }

                        roomCells.Add(pos);
                    }

                    if (invalid) break;
                }

                if (invalid) continue;

                Room room = new Room
                {
                    center = cell.position,
                    roomType = RoomType.Normal,
                    tiles = roomCells
                };

                rooms.Add(room);

                foreach (Vector2Int pos in room.tiles)
                    occupied.Add(pos);

                if (rooms.Count >= maxRooms)
                    return rooms;
            }
        }

        return rooms;
    }
}