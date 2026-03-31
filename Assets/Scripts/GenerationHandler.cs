using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationHandler : MonoBehaviour
{
    private Cell[,] grid;
    private List<Room> roomList = new List<Room>();
    private Cell[,] path;

    [SerializeField]private int seed;
    
    [Header("references")]
    [SerializeField] private GridHandler gridHandler;
    [SerializeField] private RandomWalk randomWalk;
    [SerializeField] private RoomPlacement roomPlacement;

    private void Start()
    {
        Random.InitState(seed);
        Generate();
    }

    public void Generate()
    {
        grid = gridHandler.CreateGrid();
        grid = randomWalk.RandomlyWalk(grid);
        roomList = roomPlacement.generateRooms(grid);
        GridDebug();
    }

    public void RandomizeSeed()
    {
        seed = Random.Range(0, 999999);
    }

    private void GridDebug()
    {
        int gridWidth = grid.GetLength(0);
        int gridLength = grid.GetLength(1);
        Debug.Log(gridWidth + " x " + gridLength);

        foreach (Cell cell in grid)
        {
             Debug.Log(cell.position + " " + cell.wallType + " " + cell.tileType);
        }
    }
}
