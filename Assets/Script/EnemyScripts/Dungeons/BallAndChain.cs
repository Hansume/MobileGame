using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAndChain : EnemiesController
{
    //Stun (Disable Movement)
    protected override void BasicDamage()
    {
        base.BasicDamage();
        float stunRate = Random.value;
        if (stunRate <= 0.2f){
            playerInstance.playerStats.isStun = true;
        }
    }
}