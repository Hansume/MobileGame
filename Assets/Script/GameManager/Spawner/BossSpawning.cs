using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawning : DungeonSpawning
{
    public GameObject bossPrefab;
    private GameObject bossGameobject;
    [SerializeField] private GameObject bossEntrance;
    public GameObject chestPrefab;

    public Transform[] firePositions = new Transform[15];
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Slider bossHealthbar;

    private CharacterStats bossStats = null;
    private bool bossSpawned = false;

    protected override void Update()
    {
        base.Update();

        if (dungeonEnemies == 0 && inBound)
        {
            bossEntrance.SetActive(true);
            if (!bossSpawned)
            {
                StartCoroutine(SpawnBoss());
                bossSpawned = true;
            }
        }

        if (bossGameobject != null)
        {
            bossHealthbar.value = bossStats.currentHealth;

            if (bossGameobject.GetComponent<BossController>().canFire)
            {
                bossGameobject.GetComponent<BossController>().canFire = false;
                StartCoroutine(SpawnRock());
            }

            if (bossStats.isDead)
            {
                Instantiate(chestPrefab, transform.position, Quaternion.identity);
                bossEntrance.SetActive(false);
                bossGameobject = null;
            }
        }
    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(5f);
        bossGameobject = Instantiate(bossPrefab);
        bossHealthbar.gameObject.SetActive(true);
        bossStats = bossGameobject.GetComponent<CharacterStats>();
        bossHealthbar.maxValue = bossStats.maxHealth;
    }

    private IEnumerator SpawnRock()
    {
        int bulletsFired = 0;
        while (bulletsFired < firePositions.Length)
        {
            Pooler.instance.SpawnFromPool("Boss Rock", firePositions[bulletsFired].position, Quaternion.Euler(0, 0, 90));

            bulletsFired++;

            yield return new WaitForSeconds(.3f);
        }
    }
}