using UnityEngine;

public class BossThunder : MonoBehaviour
{
    public Vector2 hitbox;
    private Vector2 hitboxPosition;

    private void DealDamage()
    {
        hitboxPosition = new Vector2(transform.position.x - .25f, transform.position.y - 1.5f);
        Collider2D[] collider = Physics2D.OverlapBoxAll(hitboxPosition, hitbox, 0);
        foreach (Collider2D playerCollider in collider)
        {
            if (playerCollider.gameObject.tag == "Player")
            {
                PlayerInstance.instance.DamagePlayer(1);
                break;
            }
        }
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitboxPosition, hitbox);
    }
}
