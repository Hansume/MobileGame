using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private void Update()
    {
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
