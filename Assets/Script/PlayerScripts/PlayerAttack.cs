using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    PlayerMovement playerMovement;

    public GameObject arrowPrefab;

    private bool isFiring = false;
    public float horizontalMove, verticalMove;
    private Vector2 shootDirection;

    public Transform firePoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        GetShootingDirection();
        if (playerMovement.moveDirection.x == 0 && playerMovement.moveDirection.y == 0)
        {
            horizontalMove = playerMovement.prevMoveDirection.x;
            verticalMove = playerMovement.prevMoveDirection.y;
        }
        else
        {
            horizontalMove = playerMovement.moveDirection.x;
            verticalMove = playerMovement.moveDirection.y;
        }
    }

    /*void GetShootingDirection()
    {
        if (playerMovement.moveDirection.x == 0 && playerMovement.moveDirection.y == 0)
        {
            horizontalMove = playerMovement.prevMoveDirection.x;
            verticalMove = playerMovement.prevMoveDirection.y;
        }
        else
        {
            horizontalMove = playerMovement.moveDirection.x;
            verticalMove = playerMovement.moveDirection.y;
        }

        if (verticalMove == 0 && horizontalMove == 1)
        {
            Debug.Log("RIGHT");
            firePoint.position = firePoint_Right.position;
            arrowRotation = Quaternion.Euler(0, 0, -90);
        }

        if (verticalMove == 0 && horizontalMove == -1)
        {
            Debug.Log("LEFT");
            firePoint.position = firePoint_Left.position;
            arrowRotation = Quaternion.Euler(0, 0, 90);
        }

        if (verticalMove == 1 && horizontalMove == -1)
        {
            Debug.Log("UP");
            firePoint.position = firePoint_Up.position;
            arrowRotation = Quaternion.Euler(0, 0, 0);
        }

        if (verticalMove == -1)
        {
            Debug.Log("DOWN");
            firePoint.position = firePoint_Down.position;
            arrowRotation = Quaternion.Euler(0, 0, 180);
        }
    }*/

    void GetShootingDirection()
    {
        Vector3 aim = new Vector3(playerMovement.moveDirection.x, playerMovement.moveDirection.y, 0.0f);
        shootDirection = new Vector2(horizontalMove, verticalMove);
        if (aim.magnitude > 0.0f && !isFiring)
        {
            aim.Normalize();
            shootDirection.Normalize();
            firePoint.transform.localPosition = aim;
        }
    }

    public void Attack()
    {
        animator.SetBool("isAttacking", true);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = shootDirection * 3f;
        arrow.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
        GetComponent<PlayerMovement>().enabled = false;
        isFiring = true;
    }

    void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
        GetComponent<PlayerMovement>().enabled = true;
        isFiring = false;
    }
}