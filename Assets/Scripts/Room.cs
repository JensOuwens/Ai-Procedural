using System.Collections.Generic;
using UnityEngine;

public struct Room
{
    List<Vector3> tiles;
    public Vector2Int center;
    public RoomType roomType;
}
