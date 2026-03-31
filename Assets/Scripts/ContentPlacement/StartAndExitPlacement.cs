using System.Collections.Generic;
using UnityEngine;

public class StartAndExitPlacement : MonoBehaviour
{
    public List<Room> Place(Cell[,] grid, List<Room> rooms)
    {
        if (rooms.Count < 2)
            return rooms;

        int maxDistSqr = -1;
        int furthestI = -1;
        int furthestJ = -1;

        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = i + 1; j < rooms.Count; j++)
            {
                int distSqr = (rooms[i].center - rooms[j].center).sqrMagnitude;

                if (distSqr > maxDistSqr)
                {
                    maxDistSqr = distSqr;
                    furthestI = i;
                    furthestJ = j;
                }
            }
        }

        Room a = rooms[furthestI];
        Room b = rooms[furthestJ];

        a.roomType = RoomType.Start;
        b.roomType = RoomType.Boss;

        rooms[furthestI] = a;
        rooms[furthestJ] = b;

        Vector2Int startPos = a.center;

        Cell startCell = grid[startPos.x, startPos.y];
        startCell.contentType = ContentType.Start;
        grid[startPos.x, startPos.y] = startCell;

        Vector2Int exitPos = b.tiles[Random.Range(0, b.tiles.Count)];

        Cell exitCell = grid[exitPos.x, exitPos.y];
        exitCell.contentType = ContentType.Exit;
        grid[exitPos.x, exitPos.y] = exitCell;

        return rooms;
    }
}