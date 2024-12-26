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
    public GameObject changePasswordView;

    private void Start()
    {
        btnPlay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        btnLogout.onClick.AddListener(() =>
        {
            Debug.Log("Before DeleteAll - Username: " + PlayerPrefs.GetString("Username"));

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("After DeleteAll - Username: " + PlayerPrefs.GetString("Username"));
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

        btnChangePassword.onClick.AddListener(() =>
        {
            changePasswordView.SetActive(true);
            gameObject.SetActive(false);
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void OnEnable()
    {
        string username = PlayerPrefs.GetString("Username");
        txtUsername.text = username;
    }
}