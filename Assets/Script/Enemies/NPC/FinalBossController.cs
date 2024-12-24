using System.Collections;
using UnityEngine;

public class FinalBossController : EnemiesController
{
    [SerializeField] private RuntimeAnimatorController secondAnimator;
    [SerializeField] private Sprite secondSprite;
    [SerializeField] private GameObject thunder;
    private int numberOfThunder = 5;

    private void Update()
    {
        base.Update();
        if(state == enemyState.Attack2)
        {
            characterStats.canHit = false;
        }
    }

    protected override void ResetSkill()
    {
        base.ResetSkill();
        characterStats.canHit = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Arrow" && characterStats.canHit)
        {
            animator.SetTrigger("isHit");
        }
    }

    protected override void Effects()
    {
        characterStats.Heal(3);
    }

    private void ChangeForm()
    {
        animator.runtimeAnimatorController = secondAnimator;
        animator.Rebind();
        animator.Update(0);
        spriteRenderer.sprite = secondSprite;

        state = enemyState.Run;

        skillTimer = skillCooldown;
        GetComponent<Collider2D>().enabled = true;
        characterStats.isDead = false;
        characterStats.currentHealth = 10;
    }

    private void Form2Skill()
    {
        StartCoroutine(SpawnThunder());
    }

    private IEnumerator SpawnThunder()
    {
        int hasSpawned = 0;
        SpawnThunderEffect();
        while (hasSpawned < numberOfThunder)
        {
            Spawning();
            yield return new WaitForSeconds(.5f);
            hasSpawned++;
        }
        ResetSpawnThunderEffect();
    }

    private void Spawning()
    {
        Vector2 spawnPoint = new Vector2(playerInstance.transform.position.x, playerInstance.transform.position.y + 2f);
        Pooler.instance.SpawnFromPool("Boss Thunder", spawnPoint, Quaternion.identity);
    }

    private void SpawnThunderEffect()
    {
        animator.speed = 0;
        characterStats.canHit = false;
    }

    private void ResetSpawnThunderEffect()
    {
        animator.speed = 1;
        characterStats.canHit = true;
    }
}
