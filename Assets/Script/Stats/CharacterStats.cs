using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public int damage;

    public bool canHit = true;
    public bool isDead = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        
    }
}
