using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        Cell startCell = grid[startPos.x, startPos.y];
        Node startNode = new Node(startPos, null, 0, 0);
        Cell endCell = grid[endPos.x, endPos.y];
        Cell currentCell;
        Node currentNode;
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        if (startCell.gridPosition == endCell.gridPosition)
        {
            Debug.Log("allready at target location");
            return null;
        }
        
        openList.Add(startNode);
        currentCell = startCell;
        currentNode = startNode;
        
        while (openList.Count > 0)
        {
            bool isFirstNeighbour = true;
            float currentFScore;
            Node currentLowestNode = null;
            
            foreach (Node neighbourNode in openList)
            {
                if (isFirstNeighbour)
                {
                    currentFScore = neighbourNode.FScore;
                    currentLowestNode = neighbourNode;
                    isFirstNeighbour = false;
                }
                
                if (neighbourNode.FScore < currentLowestNode.FScore)
                {
                    currentFScore = neighbourNode.FScore;
                    currentLowestNode =  neighbourNode;
                }
            }
            
            openList.Remove(currentLowestNode);
            closedList.Add(currentLowestNode);
            currentNode = currentLowestNode;
            currentCell = grid[currentLowestNode.position.x, currentLowestNode.position.y];
            
            if (currentNode.position == endPos)
            {
                return CalculateRoute(currentNode);
                return null;
            }
            
            //get neighbours
            List<Cell> neighbours = currentCell.GetNeighbours(grid);

            foreach (Cell neighbour in neighbours)
            {
                if (!WallCheck(currentCell, neighbour)) continue;
                
                int gsCcore = CalculateGScore(currentNode, neighbour.gridPosition);
                int hScore = CalculateHScore(endPos, neighbour.gridPosition);

                Node neighbourNode = new Node(neighbour.gridPosition, currentNode, gsCcore, hScore);

                bool isDuplicate = false;
                
                foreach (Node ClosedNode in closedList)
                {
                    if (ClosedNode.position == neighbourNode.position) isDuplicate = true; continue;
                }
                
                foreach (Node compareNode in openList)
                {
                    if (compareNode.position == neighbourNode.position)
                    {
                        if (neighbourNode.GScore < compareNode.GScore)
                        {
                            compareNode.GScore = gsCcore;
                            compareNode.parent = currentNode;
                            isDuplicate = true;
                        }
                        else
                        {
                            isDuplicate = true;
                        }

                        break;
                    }
                }
                
                if (!isDuplicate) openList.Add(neighbourNode);
            }
        }
        return null;
    }
    
    //check all 4 directions for walls
    private bool WallCheck(Cell currentCell, Cell neighbour)
    {
        Vector2Int direction = (neighbour.gridPosition - currentCell.gridPosition);

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

    private int CalculateGScore(Node currentNode, Vector2Int currentPosition)
    {
        float gscore = currentNode.GScore + (currentPosition - currentNode.position).magnitude;
        return (int)gscore;
    }
    
    private int CalculateHScore(Vector2Int targetPosition, Vector2Int neighbourPosition)
    {
        return Math.Abs(neighbourPosition.x - targetPosition.x) + Math.Abs(neighbourPosition.y - targetPosition.y);
    }
    
    private List<Vector2Int> CalculateRoute(Node endNode){
        List<Vector2Int> path = new List<Vector2Int>();
        Node current = endNode;
        
        while (current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }
        
        path.Reverse();
        return path;
    }
    
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
