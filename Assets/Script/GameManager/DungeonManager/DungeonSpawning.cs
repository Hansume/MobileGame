using UnityEngine;

public class DungeonSpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject currentEnemy;
    public Transform leftPos;
    public Transform rightPos;

    private bool hasCalled = false;
    private Vector2 leftPoint;
    private Vector2 rightPoint;

    void Start()
    {
        leftPoint = new Vector2(leftPos.position.x, leftPos.position.y);
        rightPoint = new Vector2(rightPos.position.x, rightPos.position.y);   
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(leftPoint, rightPoint);
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
            currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            hasCalled = true;
        }
        else if (!playerInArea && currentEnemy != null)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
            hasCalled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector2 center = (leftPoint + rightPoint) / 2;
        Vector2 size = new Vector2(Mathf.Abs(rightPoint.x - leftPoint.x), Mathf.Abs(rightPoint.y - leftPoint.y));

        Gizmos.DrawWireCube(center, size);
    }
}
