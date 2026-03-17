using UnityEngine;

public interface IDamageable
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    public bool isDead { get; set; }
    
    public void DealDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
