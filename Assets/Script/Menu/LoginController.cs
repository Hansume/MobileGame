using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;

public class LoginController : MonoBehaviour
{
    public GameObject registerView;
    public GameObject forgotPasswordView;
    public GameObject dashboard;
    public TMP_InputField inUsername;
    public TMP_InputField inPassword;
    public TMP_Text messageText;
    public Button btnLogin;
    public Button btnRegister;
    public Button btnForgotPassword;
    private const string endpoint = "http://26.123.150.88:8080/api/auth/login";

    private void Start()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);

        btnLogin.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(inUsername.text) || string.IsNullOrEmpty(inPassword.text))
            {
                ShowMessage("Please enter username and password", Color.red);
                return;
            }
            string username = inUsername.text;
            string password = inPassword.text;
            StartCoroutine(Login(username, password));
        });

        btnRegister.onClick.AddListener(() =>
        {
            registerView.SetActive(true);
            gameObject.SetActive(false);
        });

        btnForgotPassword.onClick.AddListener(() =>
        {
            forgotPasswordView.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    [Serializable]
    public class LoginReq
    {
        public string username;
        public string password;
    }

    private IEnumerator Login(string username, string password)
    {
        var loginData = new LoginReq
        {
            username = username,
            password = password
        };

        string jsonData = JsonUtility.ToJson(loginData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }

        string response = request.downloadHandler.text;
        Debug.Log("Response: " + response);

        ApiResponse<AuthenticationResponse> apiResponse = JsonUtility.FromJson<ApiResponse<AuthenticationResponse>>(response);

        if (apiResponse.success)
        {
            PlayerPrefs.SetInt("UserId", (int)apiResponse.data.User.id);
            PlayerPrefs.SetString("Username", apiResponse.data.User.username);
            PlayerPrefs.SetString("Email", apiResponse.data.User.email);
            PlayerPrefs.Save();

            Debug.Log("UserId: " + PlayerPrefs.GetString("UserId"));

            ShowMessage("Login successful!", Color.green);

            dashboard.SetActive(true);
            Reset();
            gameObject.SetActive(false);
        }
        else
        {
            ShowMessage("Login failed: " + apiResponse.message, Color.red);
            Debug.LogError("Login failed: " + apiResponse.message);
        }

        btnLogin.interactable = true;
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        messageText.color = color;
    }

    public void Reset()
    {
        inUsername.text = "";
        inPassword.text = "";
        messageText.text = "";
    }
}