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
    public TMP_Dropdown dropdown;

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

    private const string API_URL = "http://26.123.150.88:8080/api/game/ranking";

    private void Start()
    {
        //dropdown.onValueChanged.AddListener(OnLevelChanged);

        btnDashboard.onClick.AddListener(() =>
        {
            dashboardView.SetActive(true);
            gameObject.SetActive(false);
        });

        dropdown.value = 0;
        OnLevelChanged(0);
    }

    private string GetApiUrl(int level)
    {
        return $"http://localhost:8080/api/game/ranking?level={level}";
    }

    //private void OnEnable()
    //{
    //    FetchRankingData();
    //}

    public void OnLevelChanged(int index)
    {
        //Debug.Log($"Dropdown index changed: {index}");
        //dropdown.value = index;
        //int level = index + 1;
        //Debug.Log($"Selected Level: {level}");
        //StartCoroutine(GetRankingData(level));
        dropdown.value = index;
        if (index == 0)
        {
            StartCoroutine(GetRankingData(1));
        }
        else if (index == 1)
        {
            StartCoroutine(GetRankingData(2));
        }
        else if (index == 2)
        {
            StartCoroutine(GetRankingData(3));
        }
    }

    private IEnumerator GetRankingData(int level)
    {
        ClearCurrentRanking();

        using (UnityWebRequest www = UnityWebRequest.Get(GetApiUrl(level)))
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

        Color rankColor = position switch
        {
            1 => firstPlaceColor,
            2 => secondPlaceColor,
            3 => thirdPlaceColor,
            _ => defaultColor
        };

        rankText.color = rankColor;

        if (position <= 3)
        {
            Image background = item.GetComponent<Image>();
            if (background != null)
            {
                Color bgColor = rankColor;
                bgColor.a = 0.1f;
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