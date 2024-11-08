using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterBot : EnemiesController
{
    //Slow Shooting Speed
    protected override void BasicDamage()
    {
        base.BasicDamage();
        playerInstance.gameObject.GetComponent<Animator>().SetFloat("shootingSpeed", 0.25f);
        StartCoroutine(StatusEffects(3f));
    }

    protected override IEnumerator StatusEffects(float time)
    {
        yield return base.StatusEffects(time);
        playerInstance.gameObject.GetComponent<Animator>().SetFloat("shootingSpeed", 1f);
    }
}
