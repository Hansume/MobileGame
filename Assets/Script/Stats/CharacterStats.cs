using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    public bool isDead = false;
    private void Awake()
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
