using System.Collections.Generic;
using UnityEngine;

public struct Room
{
    public List<Vector2Int> tiles;
    public Vector2Int center;
    public RoomType roomType;

    public Room(Vector2Int center, List<Vector2Int> tiles)
    {
        this.center = center;
        this.tiles = tiles;
        roomType = RoomType.Normal;
    }
}
