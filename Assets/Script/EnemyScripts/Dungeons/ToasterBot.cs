using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterBot : EnemiesController
{
    //Burning
    protected override void BasicDamage()
    {
        base.BasicDamage();
        float burnRate = Random.value;
        if (burnRate <= 0.2f)
        {
            playerInstance.playerStats.isBurn = true;
        }
    }
}
