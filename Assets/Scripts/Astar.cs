using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{
    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path from the startPos to the endPos
    /// Note that you will probably need to add some helper functions
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        Cell startCell = grid[startPos.x, startPos.y];
        Cell endCell = grid[endPos.x, endPos.y];
        List<Cell> closedList = new List<Cell>();
        List<Cell> openList = new List<Cell>();
        closedList.Add(startCell);
        
        //get neighbours
        //while ()
        {
            List<Cell> neighbors = new  List<Cell>();
            
            foreach (Cell cell in closedList)
            {
                if (WallCheck(cell)) continue;
                
                neighbors = cell.GetNeighbours(grid);
                
            }
            

        }
        
        //Debug.Log(startCell.gridPosition);
        //Debug.Log(endCell.gridPosition);
        
        
        
        return null;
    }
    
    //check all 4 directions for walls
    private bool WallCheck(Cell currentCell)
    {
        Debug.Log(currentCell.HasWall(Wall.RIGHT));
        if (!currentCell.HasWall(Wall.UP) && !currentCell.HasWall(Wall.DOWN) && !currentCell.HasWall(Wall.LEFT) &&  !currentCell.HasWall(Wall.RIGHT))
        {
            return true;
        }
        return false;
        //Debug.Log("this cell has no walls at coords" + currentCell.gridPosition);
    }

    //calculate fscor for neighbours
    //calculate hscore for neighbours
    private void CalculateCellScores(Cell currentCell)
    {
        
    }
    
    //get neighbours
    //calculate fscor for neighbours
    //calculate hscore for neighbours
    //decide the best option
    //save best option in the list
    //repeat
    

    /// <summary>
    /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
    /// </summary>
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}
