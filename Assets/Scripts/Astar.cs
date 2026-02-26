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
        closedList.Add(endCell);
        
        //while ()
        {
            //get neighbours
            int closedIndex = closedList.Count - 1;
            List<Cell> neighbours = closedList[closedIndex].GetNeighbours(grid);

            foreach (Cell neighbour in neighbours)
            {
                if (WallCheck(closedList[closedIndex], neighbour)) openList.Add(neighbour);
            }

            foreach (Cell cell in openList)
            {
                Debug.Log(cell.gridPosition);
            }
            

        }
        
        //Debug.Log(startCell.gridPosition);
        //Debug.Log("endcell " + endCell.gridPosition);
        
        
        
        return null;
    }
    
    //check all 4 directions for walls
    private bool WallCheck(Cell currentCell, Cell neighbour)
    {
        Vector2Int direction = (currentCell.gridPosition - neighbour.gridPosition);

        if (direction == Vector2Int.up)
        {
            if (currentCell.HasWall(Wall.UP))
            {
                return false;
            }
        }
        else if (direction == Vector2Int.down)
        {
            if (currentCell.HasWall(Wall.DOWN))
            {
                return false;
            }
        }
        else if (direction == Vector2Int.left)
        {
            if (currentCell.HasWall(Wall.LEFT))
            {
                return false;
            }
        }
        else if (direction == Vector2Int.right)
        {
            if (currentCell.HasWall(Wall.RIGHT))
            {
                return false;
            }
        }
        return true;
    }

    //calculate fscore for neighbours
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
