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
    }

    public void RandomizeSeed()
    {
        seed = Random.Range(0, 999999);
    }
}
