using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    public GameObject loginView;
    public GameObject dashboard;
    public TMP_InputField inUsername;
    public TMP_InputField inEmail;
    public TMP_InputField inPassword;
    public TMP_InputField inConfirmPassword;
    public TMP_Text messageText;
    public Button btnLogin;
    public Button btnRegister;
    private const string endpoint = "26.123.150.88:8080/api/auth/register";

    private void Start()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);

        btnRegister.onClick.AddListener(() =>
        {
            if (ValidateInput())
            {
                StartCoroutine(Register(
                    inUsername.text,
                    inEmail.text,
                    inPassword.text,
                    inConfirmPassword.text
                ));
            }
        });

        btnLogin.onClick.AddListener(() =>
        {
            loginView.SetActive(true);
            Reset();
            gameObject.SetActive(false);
        });
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(inUsername.text))
        {
            ShowMessage("Please enter username", Color.red);
            return false;
        }

        if (string.IsNullOrEmpty(inEmail.text))
        {
            ShowMessage("Please enter email", Color.red);
            return false;
        }

        if (string.IsNullOrEmpty(inPassword.text))
        {
            ShowMessage("Please enter password", Color.red);
            return false;
        }

        if (string.IsNullOrEmpty(inConfirmPassword.text))
        {
            ShowMessage("Please confirm your password", Color.red);
            return false;
        }

        if (inPassword.text != inConfirmPassword.text)
        {
            ShowMessage("Passwords do not match", Color.red);
            return false;
        }

        return true;
    }

    [Serializable]
    public class RegisterReq
    {
        public string username;
        public string email;
        public string password;
        public string confirmPassword;
    }

    private IEnumerator Register(string username, string email, string password, string confirmPassword)
    {
        var registerData = new RegisterReq
        {
            username = username,
            email = email,
            password = password,
            confirmPassword = confirmPassword
        };

        string jsonData = JsonUtility.ToJson(registerData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            ShowMessage(request.error, Color.red);
            Debug.LogError("Connection error: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("Response: " + response);

            ApiResponse<AuthenticationResponse> apiResponse = JsonUtility.FromJson<ApiResponse<AuthenticationResponse>>(response);

            if (apiResponse.success)
            {
                ShowMessage("Registration successful!", Color.green);
            }
            else
            {
                ShowMessage("Registration failed: " + apiResponse.message, Color.red);
                Debug.LogError("Registration failed: " + apiResponse.message);
            }
        }

        btnRegister.interactable = true;
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        messageText.color = color;
    }

    private void Reset()
    {
        inUsername.text = "";
        inEmail.text = "";
        inPassword.text = "";
        inConfirmPassword.text = "";
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }
}