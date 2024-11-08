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
            playerInstance.gameObject.GetComponent<PlayerMovement>().canMove = false;
            playerInstance.gameObject.GetComponent<PlayerAttack>().enabled = false;
            StartCoroutine(DestunnedPlayer());
        }
    }

    private IEnumerator DestunnedPlayer()
    {
        yield return new WaitForSeconds(3);
        playerInstance.gameObject.GetComponent<PlayerMovement>().canMove = true;
        playerInstance.gameObject.GetComponent<PlayerAttack>().enabled = true;
    }
}