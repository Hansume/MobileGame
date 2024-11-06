using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public Slider healthbar;

    private void Start()
    {
        healthbar.maxValue = maxHealth;
    }

    private void Update()
    {
        healthbar.value = currentHealth;
        if (currentHealth <= 0)
        {
            PlayerInstance.instance.KillPlayer();
        }
    }
}
