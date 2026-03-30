using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationHandler : MonoBehaviour
{
    private Cell[,] grid;
    private List<Room> roomList;

    [SerializeField]private int seed;
    
    [Header("references")]
    [SerializeField] private GridHandler gridHandler;

    private void Start()
    {
        Random.InitState(seed);
        Generate();
    }

    public void Generate()
    {
        grid = gridHandler.CreateGrid();
        GridSizeDebug();
    }

    public void RandomizeSeed()
    {
        seed = Random.Range(0, 999999);
    }

    private void GridSizeDebug()
    {
        int gridWidth = grid.GetLength(0);
        int gridLength = grid.GetLength(1);
        Debug.Log(gridWidth + " x " + gridLength);
    }
}
