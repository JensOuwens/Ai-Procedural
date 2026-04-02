using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationHandler : MonoBehaviour
{
    private Cell[,] grid;
    private List<Room> roomList = new List<Room>();

    [SerializeField] private DungeonConfig config;

    [Header("references")]
    [SerializeField] private GridHandler gridHandler;
    [SerializeField] private RandomWalk randomWalk;
    [SerializeField] private RoomPlacement roomPlacement;
    [SerializeField] private StartAndExitPlacement startAndExitPlacement;
    [SerializeField] private ContentPlacement contentPlacement;
    [SerializeField] private DisplayGridData displayGridData;

    private void Start()
    {
        ApplyConfig();
        Generate();
    }

    public void ApplyConfig()
    {
        Random.InitState(config.seed);

        gridHandler.SetConfig(config);
        randomWalk.SetConfig(config);
        roomPlacement.SetConfig(config);
        contentPlacement.SetConfig(config);
    }

    public void Generate()
    {
        ApplyConfig();

        grid = gridHandler.CreateGrid();
        grid = randomWalk.RandomlyWalk(grid);

        roomList = roomPlacement.generateRooms(grid);
        roomList = startAndExitPlacement.Place(grid, roomList);

        roomList = contentPlacement.DetermineRooms(roomList);
        grid = contentPlacement.populateGrid(grid, roomList);

        displayGridData.RenderGrid(grid, roomList);
    }

    public void RandomizeSeed()
    {
        config.seed = Random.Range(0, 999999);
    }
}