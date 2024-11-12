using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public static int deathCount = 0;
    public static float timer = 0;

    [SerializeField] private Slider healthbar;
    [SerializeField] private TMP_Text deathText;
    [SerializeField] private TMP_Text timerText;
    private void Awake()
    {
        healthbar.maxValue = maxHealth;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        healthbar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
        timer += Time.deltaTime;
        UpdateTimerUI();

        deathText.text = "Deaths: " + deathCount.ToString();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    protected override void Die()
    {
        deathCount++;
        PlayerInstance.instance.KillPlayer();
    }
}
