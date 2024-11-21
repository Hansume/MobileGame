using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterBot : EnemiesController
{
    //Burn
    protected override void Effects()
    {
        float burnRate = Random.value;
        if (burnRate <= 0.2f)
        {
            playerInstance.playerStats.isBurn = true;
        }
    }
}
