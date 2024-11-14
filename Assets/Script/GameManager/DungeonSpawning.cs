using UnityEngine;

public class DungeonSpawning : MonoBehaviour
{
    public static int dungeonEnemies = 3;

    public AudioSource backgroundMusic;
    public GameObject enemyPrefab;
    protected GameObject currentEnemy;

    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    private Transform playerTransform;

    private bool hasCalled = false;
    private bool canCall = true;
    protected bool inBound = false;
    private Vector2 center;
    private Vector3 size;

    private Bounds areaBounds;
    private void Start()
    {
        center = (leftPoint.position + rightPoint.position) / 2;
        size = new Vector2(Mathf.Abs(rightPoint.position.x - leftPoint.position.x), Mathf.Abs(rightPoint.position.y - leftPoint.position.y));

        areaBounds = new Bounds(center, size);

        playerTransform = PlayerInstance.instance.transform;
    }

    protected virtual void Update()
    {
        if (areaBounds.Contains(playerTransform.position))
        {
            inBound = true;
            if (!hasCalled && canCall)
            {
                Spawning();
                backgroundMusic.Play();
                hasCalled = true;
            }
        }
        else
        {
            inBound = false;
            backgroundMusic.Stop();
            DeSpawning();
        }

        if (currentEnemy != null)
        {
            if (currentEnemy.GetComponent<CharacterStats>().isDead)
            {
                canCall = false;
                backgroundMusic.Stop();
                dungeonEnemies--;
                currentEnemy = null;
            }
        }
    }

    private void Spawning()
    {
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        //currentEnemy = Pooler.instance.SpawnFromPool("Monster", transform.position, Quaternion.identity);
    }

    private void DeSpawning()
    {
        Destroy(currentEnemy);
        hasCalled = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}