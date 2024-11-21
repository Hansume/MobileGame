using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAndChain : EnemiesController
{
    //Root (Disable Movement)
    protected override void Effects()
    {
        float stunRate = Random.value;
        if (stunRate <= 0.2f)
        {
            playerInstance.playerStats.isStun = true;
        }
    }
}