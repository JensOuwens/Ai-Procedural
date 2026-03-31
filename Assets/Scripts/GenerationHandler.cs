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

        Debug.Log(width + " x " + height);

        // clear previous
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject[,] spawned = new GameObject[width, height];

        // spawn cubes
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];

                Vector3 pos = new Vector3(x, 0, y);
                GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity, transform);

                Renderer r = cube.GetComponent<Renderer>();
                if (r == null) continue;

                // IMPORTANT: create instance material
                r.material = new Material(r.material);

                // base color
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

        // overlay rooms
        if (roomList == null) return;

        foreach (Room room in roomList)
        {
            if (room.tiles == null) continue;

            foreach (Cell cell in room.tiles)
            {
                int x = cell.position.x;
                int y = cell.position.y;

                if (x < 0 || x >= width || y < 0 || y >= height)
                    continue;

                GameObject cube = spawned[x, y];
                if (cube == null) continue;

                Renderer r = cube.GetComponent<Renderer>();
                if (r == null) continue;

                r.material.color = Color.red;
            }
        }
    }
}
