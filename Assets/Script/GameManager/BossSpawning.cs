using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawning : DungeonSpawning
{
    public GameObject bossPrefab;
    private GameObject bossGameobject;
    [SerializeField] private GameObject bossEntrance;
    public GameObject gemPrefab;

    public Transform[] firePositions = new Transform[15];
    [SerializeField] private GameObject bulletPrefab;
    private int bulletsFired = 0;

    [SerializeField] private Slider bossHealthbar;

    private CharacterStats bossStats = null;
    private bool bossSpawned = false;

    protected override void Update()
    {
        base.Update();

        if (dungeonEnemies == 0)
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
                Instantiate(gemPrefab);
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
        while (bulletsFired < firePositions.Length)
        {
            Instantiate(bulletPrefab, firePositions[bulletsFired].position, Quaternion.Euler(0, 0, 90));

            bulletsFired++;

            yield return new WaitForSeconds(.3f);
        }
        bulletsFired = 0;
    }
}