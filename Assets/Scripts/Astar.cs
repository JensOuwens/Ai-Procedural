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
        //get neighbours
        //while (expression)
        //{
            List<Cell> neighbors = startCell.GetNeighbours(grid);
        //}
        
        //Debug.Log(startCell.gridPosition);
        //Debug.Log(endCell.gridPosition);

        foreach (Cell neighbor in neighbors)
        {
            Debug.Log(neighbor.gridPosition);
        }
        
        
        return null;
    }

    //calculate fscor for neighbours
    //calculate hscore for neighbours
    private void calculateCellScores(Cell currentCell)
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
