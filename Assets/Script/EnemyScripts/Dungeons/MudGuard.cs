using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudGuard : EnemiesController
{
    //Slow Moving Speed
    protected override void SkillDamage()
    {
        base.SkillDamage();
        playerInstance.playerStats.isSlow = true;
    }
}
