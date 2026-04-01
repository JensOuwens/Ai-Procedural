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
    
    public List<Room> DetermineRooms(List<Room> roomList)
    {
        int roomNumber = 0;
        
        foreach (Room room in roomList)
        {
            if (room.roomType == RoomType.Boss || room.roomType == RoomType.Start)
            {
                roomNumber++;
                if (room.roomType == RoomType.Boss)
                {
                    room.RandomizeTreasureAmount(0,4);
                    room.RandomizeEnemyAmount(1,8);
                }
                continue;
            }
            
            int chanceRoll = Random.Range(0, 100);

            Room currentRoom = room;

            if (chanceRoll < hiddenRoomChance)
            {
                currentRoom.roomType = RoomType.Hidden;
                currentRoom.RandomizeTreasureAmount(2,4);
                currentRoom.RandomizeEnemyAmount(0,0);
            }
            //will break if exceeding 100 percent
            else if (chanceRoll < treasureRoomChance + hiddenRoomChance)
            {
                currentRoom.roomType = RoomType.Treasure;
                currentRoom.RandomizeTreasureAmount(4,6);
                currentRoom.RandomizeEnemyAmount(0,3);
            }
            else
            {
                currentRoom.roomType = RoomType.Normal;
                currentRoom.RandomizeTreasureAmount(0,2);
                currentRoom.RandomizeEnemyAmount(1,3);
            }
            
            roomList[roomNumber] = currentRoom;
            roomNumber++;
        }
        
        return roomList;
    }
    
    public Cell[,] populateGrid(Cell[,] grid, List<Room> roomList)
    {
        foreach (Cell cell in grid)
        {
            bool cellIsInRoom = false;
            RoomType cellRoomType = RoomType.Normal;
            
            if (cell.tileType != TileType.Floor) continue;

            foreach (Room room in roomList)
            {
                foreach (Vector2Int cellRoomPos in room.tiles)
                {
                    if (cell.position == cellRoomPos)
                    {
                        cellIsInRoom = true;
                        cellRoomType = room.roomType;
                        break;
                    }
                }
            }

            Cell currentCell = cell;
            if (!cellIsInRoom)
            {
                int chanceRoll = Random.Range(0, 100);

                if (chanceRoll < TreasureCorridorSpawnChance)
                {
                    currentCell.contentType = ContentType.Loot;
                }
                else if (chanceRoll < TreasureCorridorSpawnChance + EnemyCorridorSpawnChance)
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
                //switch spawn percentages
                int enemySpawnChance = 0;
                int TreasureSpawnChance = 0;
                
                switch (cellRoomType)
                {
                    case RoomType.Normal:
                        enemySpawnChance = 40;
                        TreasureSpawnChance = 10;
                        break;
                    case RoomType.Hidden:
                        enemySpawnChance = 10;
                        TreasureSpawnChance = 40;
                        break;
                    case  RoomType.Treasure:
                        enemySpawnChance = 20;
                        TreasureSpawnChance = 50;
                        break;
                    case RoomType.Boss:
                        enemySpawnChance = 60;
                        TreasureSpawnChance = 20;
                        break;
                    case RoomType.Start:
                        enemySpawnChance = 0;
                        TreasureSpawnChance = 0;
                        break;
                }
                
                //loop actually attempt to spawn stuff
                
            }
            
            grid[currentCell.position.x, currentCell.position.y] = currentCell;
            
        }
        
        //check if all rooms have reached the required amount
        //if so return
        //if not loop through rooms
        //check every tile
        //if tile is occupied, ignore it
        //if tile is not occupied, force spawn
        //repeat for certain attemps amount to prevent infinite loop
        //return
        
        return grid;
    }
    

}
