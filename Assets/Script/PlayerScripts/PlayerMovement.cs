using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerAttack playerAttack;
    public AudioSource moveSound;

    public GameObject playerSprite;

    public Vector3 movement;
    private Vector3 shootDirection;
    public bool canMove = true;
    private bool canDash = true;

    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();

        movement = new Vector3(1, 0);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            shootDirection = playerAttack.shootDirection;

            float speed = GetComponent<PlayerStats>().moveSpeed;
            movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

            MoveSoundEffects();

            animator.SetFloat("HorMove", movement.x);
            animator.SetFloat("VerMove", movement.y);
            animator.SetFloat("MagMove", movement.magnitude);

            rigidBody.velocity = movement * speed;

            if ((movement.x == 0 && movement.y == 0) && (shootDirection.x != 0 || shootDirection.y != 0))
            {
                animator.SetFloat("HorIdle", shootDirection.x);
                animator.SetFloat("VerIdle", shootDirection.y);
                animator.SetFloat("MagIdle", shootDirection.magnitude);
            }
        }
    }

    private void MoveSoundEffects()
    {
        if (movement.magnitude > 0f)
        {
            if (!moveSound.isPlaying)
            {
                moveSound.Play();
            }
        }
        else
        {
            moveSound.Stop();
        }
    }

    public void Dash()
    {
        if (canDash)
        {
            canMove = false;
            canDash = false;
            if (movement.magnitude == 0)
            {
                rigidBody.velocity = shootDirection * 10f;
                animator.SetFloat("HorMove", shootDirection.x);
                animator.SetFloat("VerMove", shootDirection.y);
                animator.SetFloat("MagMove", shootDirection.magnitude);
            }
            else
            {
                rigidBody.velocity = movement * 10f;
            }
            StartCoroutine(DashCooldown(0.2f, 3f));
        }
    }

    private IEnumerator DashCooldown(float duration, float cooldown)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
        yield return new WaitForSeconds(cooldown);
        canDash = true;
    }
}