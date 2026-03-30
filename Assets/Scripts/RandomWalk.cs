using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField] private int numberOfSteps;

    public void RandomlyWalk(Cell[,] grid)
    {
        //grab random position in the grid
        //take a step
        //check if step is in bounds
        //if not repeat, and return to last position
        //if so, add to the step counter
        //save the step into a list of steps
        //check if we are over the step counter
        //if not repeat
        //if so exit loop
        
        //change the cells in the path, any cell that isnt in the path becomes a diggable wall (only if it isnt indestructable), any cell that is a path becomes a floor
    }
}
