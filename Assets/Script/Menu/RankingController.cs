using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class RankingController : MonoBehaviour
{
    public GameObject dashboardView;
    public Button btnDashboard;

    [Header("UI References")]
    [SerializeField] private GameObject rankingItemPrefab;

    [SerializeField] private Transform contentContainer;

    [Header("Styling")]
    [SerializeField] private Color firstPlaceColor = new Color(1f, 0.84f, 0f); // Gold

    [SerializeField] private Color secondPlaceColor = new Color(0.75f, 0.75f, 0.75f); // Silver
    [SerializeField] private Color thirdPlaceColor = new Color(0.8f, 0.5f, 0.2f); // Bronze
    [SerializeField] private Color defaultColor = Color.white;

    [Header("Animation")]
    [SerializeField] private float itemSpawnDelay = 0.1f;

    [SerializeField] private float fadeInDuration = 0.5f;

    private const string API_URL = "localhost:8080/api/game/ranking";

    private void Start()
    {
        FetchRankingData();

        btnDashboard.onClick.AddListener(() =>
        {
            dashboardView.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    private void FetchRankingData()
    {
        StartCoroutine(GetRankingData());
    }

    private IEnumerator GetRankingData()
    {
        ClearCurrentRanking();

        using (UnityWebRequest www = UnityWebRequest.Get(API_URL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                ApiResponse<List<History>> rankingData = JsonUtility.FromJson<ApiResponse<List<History>>>(jsonResponse);
                Debug.Log("Data: " + rankingData.data);
                if (rankingData.success)
                {
                    StartCoroutine(PopulateRanking(rankingData.data));
                }
            }
        }
    }

    private void ClearCurrentRanking()
    {
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator PopulateRanking(List<History> rankings)
    {
        for (int i = 0; i < rankings.Count; i++)
        {
            GameObject rankItem = Instantiate(rankingItemPrefab, contentContainer);
            SetupRankItem(rankItem, rankings[i], i + 1);

            yield return new WaitForSeconds(itemSpawnDelay);
        }
    }

    //private IEnumerator FadeInItem(CanvasGroup canvasGroup)
    //{
    //    float elapsedTime = 0f;

    //    while (elapsedTime < fadeInDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float alpha = elapsedTime / fadeInDuration;
    //        canvasGroup.alpha = alpha;
    //        yield return null;
    //    }

    //    canvasGroup.alpha = 1f;
    //}

    private void SetupRankItem(GameObject item, History rank, int position)
    {
        TextMeshProUGUI rankText = item.transform.Find("RankText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI usernameText = item.transform.Find("UsernameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI levelText = item.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI timeText = item.transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI deathText = item.transform.Find("DeathText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI createdDateText = item.transform.Find("CreatedDateText").GetComponent<TextMeshProUGUI>();

        rankText.text = $"#{position}";
        usernameText.text = rank.username;
        levelText.text = $"Level {rank.level}";
        timeText.text = FormatTime(rank.time);
        deathText.text = $"{rank.death} deaths";
        createdDateText.text = FormatCreatedDate(rank.createdAt);

        // Set color based on ranking
        Color rankColor = position switch
        {
            1 => firstPlaceColor,
            2 => secondPlaceColor,
            3 => thirdPlaceColor,
            _ => defaultColor
        };

        // Apply color to rank number
        rankText.color = rankColor;

        // Add highlight effect for top 3
        if (position <= 3)
        {
            Image background = item.GetComponent<Image>();
            if (background != null)
            {
                Color bgColor = rankColor;
                bgColor.a = 0.1f; // Subtle background
                background.color = bgColor;
            }
        }
    }

    private string FormatTime(int seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:D2}";
    }

    private String FormatCreatedDate(long epochTime)
    {
        DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(epochTime).DateTime;
        return dateTime.ToString();
    }
}