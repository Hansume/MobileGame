using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public Slider healthbar;
    {
        healthbar.value = currentHealth;
        if (currentHealth <= 0)
        {
            PlayerInstance.instance.KillPlayer();
        }
    }
}
