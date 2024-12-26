using System.Collections;
using UnityEngine;

public class StoneBlock : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 rawDirection = (transform.position - collision.transform.position).normalized;
            Vector3 forceDirection = SnapToFourDirections(rawDirection);

            rb.velocity = forceDirection * forceMagnitude;
            StartCoroutine(StopMoving());
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
    }

    private Vector2 SnapToFourDirections(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return new Vector2(Mathf.Sign(direction.x), 0f);
        }
        else
        {
            return new Vector2(0f, Mathf.Sign(direction.y));
        }
    }
}
