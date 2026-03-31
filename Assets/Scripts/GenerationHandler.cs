using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationHandler : MonoBehaviour
{
    private Cell[,] grid;
    private List<Room> roomList = new List<Room>();

    private Color red = Color.red;
    private Color green = Color.green;
    private Color yellow = Color.yellow;

    [SerializeField] private int seed;

    [Header("references")]
    [SerializeField] private GridHandler gridHandler;
    [SerializeField] private RandomWalk randomWalk;
    [SerializeField] private RoomPlacement roomPlacement;
    [SerializeField] private StartAndExitPlacement startAndExitPlacement;
    [SerializeField] private GameObject cubePrefab;

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
        roomList = startAndExitPlacement.Place(grid, roomList);

        GridDebug();
    }

    public void RandomizeSeed()
    {
        seed = Random.Range(0, 999999);
    }

    private void GridDebug()
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GameObject[,] spawned = new GameObject[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];

                GameObject cube = Instantiate(cubePrefab, new Vector3(x, 0, y), Quaternion.identity, transform);

                Renderer r = cube.GetComponent<Renderer>();
                if (r == null) continue;

                r.material = new Material(r.material);

                if (cell.tileType == TileType.Floor)
                {
                    r.material.color = Color.white;
                }
                else
                {
                    if (cell.wallType == WallType.Indestructible)
                        r.material.color = Color.black;
                    else if (cell.wallType == WallType.Diggable)
                        r.material.color = new Color(0.4f, 0.2f, 0.1f);
                }

                spawned[x, y] = cube;
            }
        }

        if (roomList == null) return;
        
        foreach (Room room in roomList)
        {
            Color useColor = room.roomType switch
            {
                RoomType.Start => green,
                RoomType.Boss => red,
                _ => yellow
            };

            foreach (Vector2Int pos in room.tiles)
            {
                if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height)
                    continue;

                GameObject cube = spawned[pos.x, pos.y];
                if (cube == null) continue;

                Renderer r = cube.GetComponent<Renderer>();
                if (r == null) continue;

                Cell cell = grid[pos.x, pos.y];

                if (cell.contentType == ContentType.Start)
                {
                    r.material.color = Color.blue;
                    continue;
                }

                if (cell.contentType == ContentType.Exit)
                {
                    r.material.color = Color.magenta;
                    continue;
                }

                r.material.color = useColor;
            }
        }
    }
}