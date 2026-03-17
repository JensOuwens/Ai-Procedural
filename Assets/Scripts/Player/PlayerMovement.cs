using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Health")] 
    [SerializeField] private int setMaxHealth;
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    public bool isDead { get; set; }

    [Header("Movement")] 
    [SerializeField]
    private float moveSpeed;

    private void OnValidate()
    {
        maxHealth = setMaxHealth;
        currentHealth = setMaxHealth;
    }
    
    
}
