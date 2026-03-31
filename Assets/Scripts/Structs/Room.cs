using System.Collections.Generic;
using UnityEngine;

public struct Room
{
    public Cell[,] tiles;
    public Vector2Int center;
    public RoomType roomType;

    public Room(Vector2Int center, Cell[,] tiles)
    {
        this.center = center;
        this.tiles = tiles;
        roomType = RoomType.Normal;
    }
}
