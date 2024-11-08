using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public float skillCooldown;
    private float skillTimer;

    protected bool canMove;
    private bool canAttack;

    public Vector2 attackRange;
    public Vector2 skillRange;

    public Transform center;
    private Transform playerTransform;
    private Collider2D[] colliders2D;

    protected PlayerInstance playerInstance;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CharacterStats characterStats;

    public enum enemyState { Run, Attack, Attack2, Death }
    public enemyState state;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();

        playerInstance = PlayerInstance.instance;
        playerTransform = playerInstance.gameObject.transform;

        skillTimer = skillCooldown;
        canMove = true;
        canAttack = true;
    }

    protected virtual void Update()
    {
        animator.SetInteger("state", (int)state);

        if (!characterStats.isDead)
        {
            if (canMove)
            {
                Movement();
            }

            if (canAttack)
            {
                BasicAttack();
            }

            skillTimer -= Time.deltaTime;
            if (skillTimer < 0)
            {
                SkillAttack();
            }
        }
        else
        {
            state = enemyState.Death;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Movement()
    {
        Vector3 targetPosition;

        if (transform.position.x >= playerTransform.position.x)
        {
            spriteRenderer.flipX = true;
            targetPosition = playerTransform.position + new Vector3(1.5f, .5f, 0);
        }
        else
        {
            spriteRenderer.flipX = false;
            targetPosition = playerTransform.position + new Vector3(-1.5f, .5f, 0);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, GetComponent<CharacterStats>().moveSpeed * Time.deltaTime);
    }

    #region basicAttack
    private void BasicAttack()
    {
        colliders2D = Physics2D.OverlapBoxAll(center.position, attackRange, 0);
        foreach (Collider2D playerCollider in colliders2D)
        {
            if (playerCollider.gameObject.tag == "Player")
            {
                state = enemyState.Attack;
                canMove = false;
            }
        }
    }

    private void ResetAttack()
    {
        state = enemyState.Run;
        canMove = true;
    }

    protected virtual void BasicDamage()
    {
        playerInstance.DamagePlayer(1);
    }
    #endregion

    #region SkillAttack
    protected virtual void SkillAttack()
    {
        colliders2D = Physics2D.OverlapBoxAll(center.position, skillRange, 0);
        foreach (Collider2D playerCollider in colliders2D)
        {
            if (playerCollider.gameObject.tag == "Player")
            {
                canMove = false;
                canAttack = false;
                state = enemyState.Attack2;
            }
        }
    }

    private void ResetSkill()
    {
        state = enemyState.Run;
        canMove = true;
        canAttack = true;
        skillTimer = skillCooldown;
    }

    protected virtual void SkillDamage()
    {
        playerInstance.DamagePlayer(2);
    }
    #endregion

    protected virtual IEnumerator StatusEffects(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            characterStats.TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center.position, skillRange);
    }
}