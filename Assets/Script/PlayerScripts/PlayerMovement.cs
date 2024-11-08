using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerAttack playerAttack;

    private float speed;
    public Vector3 movement;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);
            speed = GetComponent<PlayerStats>().moveSpeed;

            animator.SetFloat("HorMove", movement.x);
            animator.SetFloat("VerMove", movement.y);
            animator.SetFloat("MagMove", movement.magnitude);

            rigidBody.velocity = new Vector2(movement.x, movement.y) * speed;

            if ((movement.x == 0 && movement.y == 0) && (playerAttack.shootDirection.x != 0 || playerAttack.shootDirection.y != 0))
            {
                animator.SetFloat("HorIdle", playerAttack.shootDirection.x);
                animator.SetFloat("VerIdle", playerAttack.shootDirection.y);
                animator.SetFloat("MagIdle", playerAttack.shootDirection.magnitude);
            }
        }
    }
}