using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: change boids bounds to reflect the camera
/// </summary>
public class BoidsBehaviour : MonoBehaviour
{
    [Header("Boids settings")]
    [SerializeField] private int AmountOfBoids;
    [SerializeField] private GameObject BoidPrefab;
    private List<Boid> listOfBoids;
    
    [Header("Bounds Settings")]
    [SerializeField] private Vector3 MinBounds;
    [SerializeField] private Vector3 MaxBounds;

    

    private void Start()
    {
        listOfBoids = new List<Boid>();
        InitialisePositions();
    }
    private void Update()
    {
        MoveAllBoidsToNewPosition();
    }
    
    //randomizes void spawn location, the instantiates the gameobject into a list
    private void InitialisePositions()
    {
        for (int i = 0; i < AmountOfBoids; i++)
        {
            Vector3 RandomPos = new Vector3(Random.Range(MinBounds.x, MaxBounds.x),Random.Range(MinBounds.y, MaxBounds.y),Random.Range(MinBounds.z, MaxBounds.z));
            
            Boid boid = Instantiate(BoidPrefab, RandomPos, Quaternion.identity) .GetComponent<Boid>();
            
            listOfBoids.Add(boid);
        }
    }
    
    //rule application
    private void MoveAllBoidsToNewPosition()
    {
        Vector3 v1, v2, v3;

        foreach (Boid boid in listOfBoids)
        {
            v1 = CohesionRule(boid);
            v2 = SeperationRule(boid);
            v3 = AlingementRule(boid);
            
            boid.boidVelocity += v1 + v2 + v3;
            boid.gameObject.transform.position += boid.boidVelocity;
        }
    }
    
    private Vector3 CohesionRule(Boid boid)
    {
        return new Vector3();
    }
    
    private Vector3 SeperationRule(Boid boid)
    {
        return new Vector3();
    }
    
    private Vector3 AlingementRule(Boid boid)
    {
        return new Vector3();
    }
    
}
