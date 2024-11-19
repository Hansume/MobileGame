using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DashboardController : MonoBehaviour
{
    public Button btnPlay;
    public Button btnHistory;
    public Button btnRanking;
    public Button btnLogout;
    public Button btnQuit;
    public Button btnChangePassword;
    public TMP_Text txtUsername;
    public GameObject loginView;
    public GameObject rankingView;
    public GameObject historyView;

    private void Start()
    {
        string username = PlayerPrefs.GetString("Username");
        txtUsername.text = username;

        btnPlay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        btnLogout.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            loginView.SetActive(true);
            gameObject.SetActive(false);
        });

        btnRanking.onClick.AddListener(() =>
        {
            rankingView.SetActive(true);
            gameObject.SetActive(false);
        });

        btnHistory.onClick.AddListener(() =>
        {
            historyView.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}