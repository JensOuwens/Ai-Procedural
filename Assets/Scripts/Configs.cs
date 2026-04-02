using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Config")]
public class DungeonConfig : ScriptableObject
{
    [Header("General")]
    public int seed;

    [Header("Grid")]
    public Vector2Int minGridSize;
    public Vector2Int maxGridSize;

    [Header("Random Walk")]
    public int numberOfSteps;
    [Range(0f, 1f)] public float unvisitedBias;

    [Header("Rooms")]
    public int minRooms;
    public int maxRooms;
    public Vector2Int minRoomSize;
    public Vector2Int maxRoomSize;
    public int roomSpawnChance;
    public int roomMaxAttempts;

    [Header("Content - Rooms")]
    public int treasureRoomChance;
    public int hiddenRoomChance;

    [Header("Content - Corridors")]
    public int enemyCorridorSpawnChance;
    public int treasureCorridorSpawnChance;
}
