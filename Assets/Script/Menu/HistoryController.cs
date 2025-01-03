using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    public GameObject dashboardView;
    public Button btnDashboard;
    public TMP_Dropdown levelDropdown;
    public TMP_Dropdown criteriaDropdown;

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

    private string baseApiUrl = "http://26.123.150.88:8080/api/game/history/";
    private string API_URL;

    private void Start()
    {
        levelDropdown.gameObject.SetActive(false);
        btnDashboard.onClick.AddListener(() =>
        {
            dashboardView.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        int userId = PlayerPrefs.GetInt("UserId");
        baseApiUrl = "http://26.123.150.88:8080/api/game/history/" + userId;
        API_URL = baseApiUrl;
        FetchRankingData();
    }

    public void OnCriteriaChanged(int index)
    {
        API_URL = baseApiUrl;
        if (index == 0)
        {
            API_URL += "?type=true";
            Debug.Log("API_URL before fetching api: " + API_URL);
            levelDropdown.gameObject.SetActive(false);
            FetchRankingData();
        }
        else
        {
            API_URL += "?type=false&level=1";
            Debug.Log("API_URL before fetching api: " + API_URL);
            FetchRankingData();
            levelDropdown.gameObject.SetActive(true);
        }
    }

    public void OnLevelChanged(int index)
    {
        API_URL = baseApiUrl + "?type=false";
        if (index == 0)
        {
            API_URL += "&level=1";
            Debug.Log("API_URL before fetching api: " + API_URL);
            FetchRankingData();
        }
        else if (index == 1)
        {
            API_URL += "&level=2";
            Debug.Log("API_URL before fetching api: " + API_URL);
            FetchRankingData();
        }
        else if (index == 2)
        {
            API_URL += "&level=3";
            Debug.Log("API_URL before fetching api: " + API_URL);
            FetchRankingData();
        }
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