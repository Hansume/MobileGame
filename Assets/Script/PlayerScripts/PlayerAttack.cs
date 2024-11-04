using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    PlayerMovement playerMovement;

    public GameObject arrowPrefab;
    public Transform firePoint;

    public Vector2 shootDirection = new Vector2(0,0);
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Aiming();
    }

    void Aiming()
    {
        Vector3 aim = new Vector3(playerMovement.movement.x, playerMovement.movement.y, 0.0f);
        shootDirection = new Vector2(playerMovement.movement.x, playerMovement.movement.y);
        if (shootDirection == Vector2.zero)
        {
            shootDirection = firePoint.transform.position - transform.position;
        }

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            shootDirection.Normalize();
            firePoint.transform.localPosition = aim;
        }
    }

    private void Attack()
    {
        animator.SetBool("isShooting", true);
        playerMovement.canMove = false;
        animator.SetFloat("HorShoot", shootDirection.x);
        animator.SetFloat("VerShoot", shootDirection.y);
    }

    private void FireArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = shootDirection * 4f;
        arrow.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
    }

    private void ResetAttack()
    {
        animator.SetBool("isShooting", false);
        playerMovement.canMove = true;
    }
}