using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private float moveSpeed = 3f;
    private float horizontalMove, verticalMove;

    public Vector2 moveDirection;
    public Vector2 prevMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;

        GetMoveDirection();

        SetMovementAnimation();
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    void GetMoveDirection()
    {
        if (horizontalMove >= .3f)
        {
            horizontalMove = 1;
        }
        else if (horizontalMove <= -.3f)
        {
            horizontalMove = -1;
        }
        else
        {
            horizontalMove = 0;
        }

        if (verticalMove >= .3f)
        {
            verticalMove = 1;
        }
        else if (verticalMove <= -.3f)
        {
            verticalMove = -1;
        }
        else
        {
            verticalMove = 0;
        }

        if ((horizontalMove == 0 && verticalMove == 0) && (moveDirection.x != 0 || moveDirection.y != 0))
        {
            prevMoveDirection = moveDirection;
        }

        moveDirection = new Vector2(horizontalMove, verticalMove).normalized;
    }

    void SetMovementAnimation()
    {
        animator.SetFloat("HorizontalMove", horizontalMove);
        animator.SetFloat("VerticalMove", verticalMove);

        animator.SetFloat("prevHorizontalMove", prevMoveDirection.x);
        animator.SetFloat("prevVerticalMove", prevMoveDirection.y);

        animator.SetFloat("MoveSpeed", moveDirection.sqrMagnitude);
    }
}