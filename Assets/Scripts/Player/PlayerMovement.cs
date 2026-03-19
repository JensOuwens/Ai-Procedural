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
    [SerializeField] private float moveSpeed;
    private Vector2 input;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        maxHealth = setMaxHealth;
        currentHealth = setMaxHealth;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0, input.y) * moveSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    
    public void DealDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
        }
    }
}