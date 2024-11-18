using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public bool isSlow = false;
    public bool isStun = false;
    public bool isBurn = false;

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        
        if (isSlow)
        {
            moveSpeed = .5f;
            StartCoroutine(Slow(2f));
        }

        if (isStun)
        {
            moveSpeed = 0;
            StartCoroutine(Stun(2f));
        }

        if (isBurn)
        {
            StartCoroutine(Burn(4f));
        }
    }

    protected override void Die()
    {
        PlayerInstance.instance.KillPlayer();
    }

    #region Effects
    private IEnumerator Slow(float duration)
    {
        isSlow = false;
        yield return new WaitForSeconds(duration);
        moveSpeed = 2f;
    }

    private IEnumerator Stun (float duration)
    {
        isStun = false;
        yield return new WaitForSeconds(duration);
        moveSpeed = 2f;
    }

    private IEnumerator Burn(float duration)
    {
        isBurn = false;
        int elapsed = 0;
        while (elapsed < duration)
        {
            currentHealth -= 0.5f;
            yield return new WaitForSeconds(1f);
            elapsed += 1;
        }
    }
    #endregion
}
