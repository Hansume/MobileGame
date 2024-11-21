using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController: EnemiesController
{
    private bool canCount = false;
    private bool phaseTwo = false;
    public bool canFire = false;

    protected override void Start()
    {
        base.Start();
        skillTimer = skillCooldown;
    }

    protected override void Update()
    {
        base.Update();

        if (characterStats.currentHealth <= (characterStats.maxHealth / 2) && !phaseTwo)
        {
            phaseTwo = true;
            canAttack = false;
            characterStats.canHit = false;
            animator.SetTrigger("activatePhaseTwo");
        }

        if (canCount)
        {
            skillTimer -= Time.deltaTime;
            if (skillTimer < 0 && canCount)
            {
                state = enemyState.Attack2;
                canAttack = false;
            }
        }
    }

    private void ImmuneToSkill()
    {
        StartCoroutine(TransitionToSkill());
    }
    
    private IEnumerator TransitionToSkill()
    {
        yield return new WaitForSeconds(3f);
        state = enemyState.Attack2;
    }

    private IEnumerator FireBullets()
    {
        characterStats.canHit = false;
        canFire = true;
        yield return new WaitForSeconds(3f);
        Reset();
    }

    private void Reset()
    {
        characterStats.canHit = true;
        skillTimer = skillCooldown;
        canAttack = true;
        canCount = true;
        animator.ResetTrigger("activatePhaseTwo");
        state = enemyState.Run;
    }
}