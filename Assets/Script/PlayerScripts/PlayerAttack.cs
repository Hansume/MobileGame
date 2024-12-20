using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    public AudioSource fireArrowAudio;
    private PlayerMovement playerMovement;

    public Transform firePoint;
    public GameObject playerSprite;

    public Vector2 shootDirection = new Vector2(1,0);

    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
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
            firePoint.transform.localPosition = aim * 0.5f;
        }
    }

    private void Attack()
    {
        animator.SetBool("isShooting", true);
        playerMovement.canMove = false;
        rigidBody.velocity = Vector3.zero;
        animator.SetFloat("HorShoot", shootDirection.x);
        animator.SetFloat("VerShoot", shootDirection.y);
    }

    public void FireArrow()
    {
        fireArrowAudio.Play();
        GameObject arrow = Pooler.instance.SpawnFromPool("Player arrow", firePoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = shootDirection * 7.5f;
        arrow.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
    }

    public void ResetAttack()
    {
        animator.SetBool("isShooting", false);
        playerMovement.canMove = true;
    }
}