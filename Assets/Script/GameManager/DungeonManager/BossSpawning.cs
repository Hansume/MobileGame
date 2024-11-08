using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawning : DungeonSpawning
{
    public GameObject bossPrefab;

    private bool bossSpawned = false;
    protected override void Update()
    {
        base.Update();
        if (currentEnemy.GetComponent<CharacterStats>().isDead)
        {
            if (!bossSpawned)
            {
                Instantiate(bossPrefab);
                bossSpawned = true;
            }
        }
    }
}
