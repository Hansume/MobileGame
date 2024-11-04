using System.Collections;
using UnityEngine;

public class BossController: EnemiesController
{
    public GameObject prefabs;
    private float timer = 10f;
    private bool canRange;

    protected override void Start()
    {
        base.Start();
        canRange = true;
    }

    protected override void Update()
    {
        base.Update();
        if (canRange)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                canMove = false;
                canMelee = false;
                canRange = false;
                state = enemyState.Attack2;
            }
        }
    }

    IEnumerator SpawnBullets()
    {
        int i = 10;
        while (i > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8,16), 77.77f, 0);
            Instantiate(prefabs, spawnPos, Quaternion.Euler(0, 0, 90));
            i--;
            yield return new WaitForSeconds(1);
        }
        canRange = true;
        timer = 10f;
        ResetAttack();
    }
}