using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public float skillCooldown;
    protected float skillTimer;

    protected bool canAttack = false;
    private bool playerFound = false;

    public Vector2 attackRange;
    public Vector2 skillRange;

    public Transform center;
    private Collider2D[] colliders2D;

    protected PlayerInstance playerInstance;
    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected CharacterStats characterStats;

    public enum enemyState { Run, Attack, Attack2, Death }
    public enemyState state;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();

        playerInstance = PlayerInstance.instance;

        skillTimer = skillCooldown;
    }

    protected virtual void Update()
    {
        animator.SetInteger("state", (int)state);

        if (!characterStats.isDead)
        {
            if (state == enemyState.Run)
            {
                Movement();
            }

            skillTimer -= Time.deltaTime;
            if (skillTimer > 0)
            {
                PerformAttack(attackRange);
                if (canAttack)
                {
                    state = enemyState.Attack;
                    canAttack = false;
                }
            }
            else
            {
                PerformAttack(skillRange);
                if (canAttack)
                {
                    state = enemyState.Attack2;
                    canAttack = false;
                }
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
        Transform playerTransform = playerInstance.gameObject.transform;

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

    private void PerformAttack(Vector2 range)
    {
        playerFound = false;

        colliders2D = Physics2D.OverlapBoxAll(center.position, range, 0);
        foreach (Collider2D playerCollider in colliders2D)
        {
            if (playerCollider.gameObject.tag == "Player")
            {
                playerFound = true;
                canAttack = true;
                break;
            }
        }

        if (!playerFound)
        {
            canAttack = false;
        }
    }

    #region basicAttack
    private void ResetAttack()
    {
        state = enemyState.Run;
    }

    protected virtual void BasicDamage()
    {
        if (playerFound)
        {
            playerInstance.DamagePlayer(characterStats.damage);
        }
    }
    #endregion

    #region SkillAttack
    private void ResetSkill()
    {
        state = enemyState.Run;
        skillTimer = skillCooldown;
    }

    protected virtual void SkillDamage()
    {
        if (playerFound)
        {
            playerInstance.DamagePlayer(characterStats.damage);
            Effects();
        }
    }

    protected virtual void Effects()
    {

    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            if (characterStats.canHit)
            {
                characterStats.TakeDamage(playerInstance.playerStats.damage);
                playerInstance.HealPlayer(playerInstance.playerStats.damage);
            }
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