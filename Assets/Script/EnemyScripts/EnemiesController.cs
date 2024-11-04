using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public float speed;
    public float attackRange;
    protected bool canMove;
    protected bool canMelee;

    private Transform playerTransform;

    protected SpriteRenderer spriteRenderer;
    private Animator animator;
    protected CharacterStats characterStats;

    public enum enemyState { Run, Attack, Attack2, Death }
    public enemyState state;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();

        playerTransform = PlayerInstance.instance.gameObject.transform;
        canMove = true;
        canMelee = true;
    }

    protected virtual void Update()
    {
        animator.SetInteger("state", (int)state);

        if (!characterStats.isDead)
        {
            if(canMove)
            {
                Movement();
            }
            
            if(canMelee)
            {
                if (Vector2.Distance(transform.position, playerTransform.position) < attackRange)
                {
                    state = enemyState.Attack;
                    canMove = false;
                }
            }
        }
        else
        {
            state = enemyState.Death;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Movement()
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

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void BasicAttack()
    {

    }

    public void ResetAttack()
    {
        state = enemyState.Run;
        canMove = true;
        canMelee = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            characterStats.TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}