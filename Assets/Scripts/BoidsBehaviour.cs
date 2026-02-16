using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// TODO: change boids bounds to reflect the camera
/// </summary>
public class BoidsBehaviour : MonoBehaviour
{
    [Header("Boids settings")]
    [SerializeField] private int amountOfBoids;
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private float seperationDistance;
    private List<Boid> listOfBoids;
    
    [Header("Bounds Settings")]
    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;

    private Vector3 percievedCenter;

    

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
        for (int i = 0; i < amountOfBoids; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(minBounds.x, maxBounds.x),Random.Range(minBounds.y, maxBounds.y),Random.Range(minBounds.z, maxBounds.z));
            
            Boid boid = Instantiate(boidPrefab, randomPos, Quaternion.identity) .GetComponent<Boid>();
            
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
            boid.gameObject.transform.position += boid.boidVelocity.normalized;
        }
    }
    
    private Vector3 CohesionRule(Boid boid)
    {
        Vector3 percievedCenter = Vector3.zero;

        foreach (Boid focusBoid in  listOfBoids )
        {
            if (focusBoid != boid)
            {
                percievedCenter += focusBoid.transform.position;
            }
        }
        
        percievedCenter = percievedCenter / (listOfBoids.Count - 1);
            
        return (percievedCenter - boid.gameObject.transform.position) / 100;
    }
    
    private Vector3 SeperationRule(Boid boid)
    {
        Vector3 c = Vector3.zero;

        foreach (Boid focusBoid in listOfBoids)
        {
            if (focusBoid != boid)
            {
                if (Vector3.Distance(focusBoid.transform.position, boid.gameObject.transform.position) <= seperationDistance) 
                {
                    c -= (focusBoid.transform.position - boid.gameObject.transform.position);
                }
            }
        }
        
        return c;
    }
    
    private Vector3 AlingementRule(Boid boid)
    {
        return new Vector3();
    }
    
}
