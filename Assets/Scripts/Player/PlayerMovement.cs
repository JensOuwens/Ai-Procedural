using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Vector2 input;

    private void OnValidate()
    {
        maxHealth = setMaxHealth;
        currentHealth = setMaxHealth;
        
    }

    private void Update()
    {
            gameObject.transform.position += new Vector3(input.x * moveSpeed, 0 , input.y * moveSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    
    
}
