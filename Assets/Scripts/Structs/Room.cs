using System.Collections.Generic;
using UnityEngine;

public struct Room
{
    public List<Vector2Int> tiles;
    public Vector2Int center;
    public RoomType roomType;

    public int EnemyAmount;
    public int treasureAmount;
    public int currentEnemyAmount;
    public int currentTreasureAmount;

    public Room(Vector2Int center, List<Vector2Int> tiles)
    {
        this.center = center;
        this.tiles = tiles;
        
        roomType = RoomType.Normal;
        
        EnemyAmount = 0;
        treasureAmount = 0;
        currentEnemyAmount = 0;
        currentTreasureAmount = 0;
    }

    public void RandomizeEnemyAmount(int minEnemyAmount, int MaxEnemyAmount)
    {
        EnemyAmount = Random.Range(minEnemyAmount, MaxEnemyAmount);
    }

    public void RandomizeTreasureAmount(int minTreasureAmount, int MaxTreasureAmount)
    {
        treasureAmount = Random.Range(minTreasureAmount, MaxTreasureAmount);
    }
}
