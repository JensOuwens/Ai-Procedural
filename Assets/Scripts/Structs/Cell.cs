using UnityEngine;

public struct Cell
{
    public Vector2Int position;
    
    public TileType tileType;
    public WallType wallType;
    
    public ContentType contentType;

    public Cell(int x, int y)
    {
        this.position = new Vector2Int(x, y);
        
        //Defaults
        tileType = TileType.Wall;
        wallType = WallType.None;
        contentType = ContentType.None;
    }
}
