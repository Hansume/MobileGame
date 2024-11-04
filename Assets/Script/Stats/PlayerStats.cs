using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    protected override void Die()
    {
        PlayerInstance.instance.KillPlayer();
    }
}
