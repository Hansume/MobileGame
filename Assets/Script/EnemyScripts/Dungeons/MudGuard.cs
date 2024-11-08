using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudGuard : EnemiesController
{
    //Slow Moving Speed
    protected override void SkillDamage()
    {
        base.SkillDamage();
        playerInstance.playerStats.moveSpeed = .5f;
        StartCoroutine(StatusEffects(3f));
    }
    protected override IEnumerator StatusEffects(float time)
    {
        yield return base.StatusEffects(time);
        playerInstance.playerStats.moveSpeed = 2f;
    }
}
