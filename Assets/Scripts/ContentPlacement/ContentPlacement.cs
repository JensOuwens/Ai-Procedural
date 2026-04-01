using System.Collections.Generic;
using UnityEngine;

public class ContentPlacement : MonoBehaviour
{
    [SerializeField] private int minEnemiesPerRoom;
    [SerializeField] private int maxEnemiesPerRoom;
    [SerializeField] private int minTreasurePerRoom;
    [SerializeField] private int maxTreasurePerRoom;

    [SerializeField] private int treasureRoomChance;
    [SerializeField] private int hiddenRoomChance;

    [SerializeField] private int EnemyCorridorSpawnChance;
    [SerializeField] private int TreasureCorridorSpawnChance;
    
    private Dictionary<Vector2Int, int> cellToRoomIndex = new Dictionary<Vector2Int, int>();

    public List<Room> DetermineRooms(List<Room> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Room currentRoom = roomList[i];

            if (currentRoom.roomType == RoomType.Boss || currentRoom.roomType == RoomType.Start)
            {
                if (currentRoom.roomType == RoomType.Boss)
                {
                    currentRoom.RandomizeTreasureAmount(0, 4);
                    currentRoom.RandomizeEnemyAmount(1, 8);
                }

                roomList[i] = currentRoom;
                continue;
            }

            int chanceRoll = Random.Range(0, 100);

            if (chanceRoll < hiddenRoomChance)
            {
                currentRoom.roomType = RoomType.Hidden;
                currentRoom.RandomizeTreasureAmount(2, 4);
                currentRoom.RandomizeEnemyAmount(0, 0);
            }
            else if (chanceRoll < treasureRoomChance + hiddenRoomChance)
            {
                currentRoom.roomType = RoomType.Treasure;
                currentRoom.RandomizeTreasureAmount(4, 6);
                currentRoom.RandomizeEnemyAmount(0, 3);
            }
            else
            {
                currentRoom.roomType = RoomType.Normal;
                currentRoom.RandomizeTreasureAmount(0, 2);
                currentRoom.RandomizeEnemyAmount(1, 3);
            }

            currentRoom.currentEnemyAmount = 0;
            currentRoom.currentTreasureAmount = 0;

            roomList[i] = currentRoom;
        }

        return roomList;
    }

    public Cell[,] populateGrid(Cell[,] grid, List<Room> roomList)
    {
        cellToRoomIndex.Clear();
        for (int i = 0; i < roomList.Count; i++)
        {
            foreach (Vector2Int pos in roomList[i].tiles)
            {
                cellToRoomIndex[pos] = i;
            }
        }

        foreach (Cell cell in grid)
        {
            if (cell.tileType != TileType.Floor) continue;
            if (cell.contentType == ContentType.Start) continue;
            if (cell.contentType == ContentType.Exit) continue;

            Cell currentCell = cell;

            bool isInRoom = cellToRoomIndex.TryGetValue(cell.position, out int roomIndex);

            if (!isInRoom)
            {
                int roll = Random.Range(0, 100);

                if (roll < TreasureCorridorSpawnChance)
                {
                    currentCell.contentType = ContentType.Loot;
                }
                else if (roll < TreasureCorridorSpawnChance + EnemyCorridorSpawnChance)
                {
                    currentCell.contentType = ContentType.Enemy;
                }
                else
                {
                    currentCell.contentType = ContentType.None;
                }
            }
            else
            {
                Room room = roomList[roomIndex];

                int enemyChance = 0;
                int treasureChance = 0;

                switch (room.roomType)
                {
                    case RoomType.Normal:
                        enemyChance = 40;
                        treasureChance = 10;
                        break;
                    case RoomType.Hidden:
                        enemyChance = 10;
                        treasureChance = 40;
                        break;
                    case RoomType.Treasure:
                        enemyChance = 20;
                        treasureChance = 50;
                        break;
                    case RoomType.Boss:
                        enemyChance = 60;
                        treasureChance = 20;
                        break;
                    case RoomType.Start:
                        enemyChance = 0;
                        treasureChance = 0;
                        break;
                }

                int roll = Random.Range(0, 100);

                if (roll < treasureChance)
                {
                    if (room.currentTreasureAmount < room.treasureAmount)
                    {
                        currentCell.contentType = ContentType.Loot;

                        room.currentTreasureAmount++;
                        roomList[roomIndex] = room;
                    }
                }
                else if (roll < treasureChance + enemyChance)
                {
                    if (room.currentEnemyAmount < room.EnemyAmount)
                    {
                        currentCell.contentType = ContentType.Enemy;

                        room.currentEnemyAmount++;
                        roomList[roomIndex] = room;
                    }
                }
                else
                {
                    currentCell.contentType = ContentType.None;
                }
            }

            grid[currentCell.position.x, currentCell.position.y] = currentCell;
        }
        
        for (int i = 0; i < roomList.Count; i++)
        {
            Room room = roomList[i];

            foreach (Vector2Int pos in room.tiles)
            {
                if (room.currentEnemyAmount >= room.EnemyAmount &&
                    room.currentTreasureAmount >= room.treasureAmount)
                    break;

                Cell cell = grid[pos.x, pos.y];

                if (cell.tileType != TileType.Floor || cell.contentType != ContentType.None)
                    continue;

                if (room.currentEnemyAmount < room.EnemyAmount)
                {
                    cell.contentType = ContentType.Enemy;
                    room.currentEnemyAmount++;
                }
                else if (room.currentTreasureAmount < room.treasureAmount)
                {
                    cell.contentType = ContentType.Loot;
                    room.currentTreasureAmount++;
                }

                grid[pos.x, pos.y] = cell;
            }

            roomList[i] = room;
        }

        return grid;
    }
}