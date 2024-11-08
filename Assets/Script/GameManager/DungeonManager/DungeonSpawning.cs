using UnityEngine;

public class DungeonSpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    protected GameObject currentEnemy;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool hasCalled = false;
    private bool canCall = true;
    private Vector2 leftCorner;
    private Vector2 rightCorner;

    void Start()
    {
        leftCorner = new Vector2(leftPoint.position.x, leftPoint.position.y);
        rightCorner = new Vector2(rightPoint.position.x, rightPoint.position.y);   
    }

    protected virtual void Update()
    {
        if (canCall)
        {
            Collider2D[] colliders = Physics2D.OverlapAreaAll(leftCorner, rightCorner);
            bool playerInArea = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    playerInArea = true;
                    break;
                }
            }

            if (playerInArea && currentEnemy == null && !hasCalled)
            {
                Spawning();
                hasCalled = true;
            }
            else if (!playerInArea && currentEnemy != null)
            {
                if (currentEnemy.GetComponent<CharacterStats>().isDead)
                {
                    canCall = false;
                }
                else
                {
                    DeSpawning();
                }
            }
        }
    }

    private void Spawning()
    {
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private void DeSpawning()
    {
        Destroy(currentEnemy);
        currentEnemy = null;
        hasCalled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector2 center = (leftCorner + rightCorner) / 2;
        Vector2 size = new Vector2(Mathf.Abs(rightCorner.x - leftCorner.x), Mathf.Abs(rightCorner.y - leftCorner.y));

        Gizmos.DrawWireCube(center, size);
    }
}
