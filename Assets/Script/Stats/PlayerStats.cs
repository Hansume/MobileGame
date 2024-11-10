using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private Slider healthbar;

    private void Awake()
    {
        healthbar.maxValue = maxHealth;
    }

    private void Update()
    {
        healthbar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    protected override void Die()
    {
        PlayerInstance.instance.KillPlayer();
    }
}
