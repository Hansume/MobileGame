using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    PlayerStats playerStats;

    public static int deathCount = 0;
    public static float timer = 0;

    [SerializeField] private Slider healthbar;
    [SerializeField] private TMP_Text deathText;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        playerStats = PlayerInstance.instance.playerStats;
        healthbar.maxValue = playerStats.maxHealth;
    }

    void Update()
    {
        healthbar.value = playerStats.currentHealth;
        timer += Time.deltaTime;
        UpdateTimerUI();
        deathText.text = "Deaths: " + deathCount.ToString();
        if (playerStats.currentHealth <= 0 )
        {
            deathCount++;
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
