using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResetPasswordController : MonoBehaviour
{
    public GameObject forgotPasswordView;
    public GameObject loginView;
    public TMP_InputField inOTP;
    public TMP_InputField inEmail;
    public TMP_InputField inNewPassword;
    public TMP_InputField inConfirmPassword;
    public Button btnSubmit;
    public Button btnBack;
    public TMP_Text messageText;
    private const string endpoint = "26.123.150.88:8080/api/auth/reset-password";

    // Start is called before the first frame update
    private void Start()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);
        string email = PlayerPrefs.GetString("fgEmail");
        inEmail.text = email;

        btnSubmit.onClick.AddListener(() =>
        {
            StartCoroutine(ResetPassword(inOTP.text, inEmail.text, inNewPassword.text, inConfirmPassword.text));
        });

        btnBack.onClick.AddListener(() =>
        {
            forgotPasswordView.SetActive(true);
            Reset();
            gameObject.SetActive(false);
        });
    }

    [Serializable]
    public class ResetPasswordReq
    {
        public string email;
        public string token;
        public string newPassword;
        public string confirmPassword;
    }

    private IEnumerator ResetPassword(string otp, string email, string newPassword, string confirmPassword)
    {
        var resetPasswordData = new ResetPasswordReq
        {
            email = email,
            token = otp,
            newPassword = newPassword,
            confirmPassword = confirmPassword
        };

        string jsonData = JsonUtility.ToJson(resetPasswordData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            ShowMessage("Connection error: " + request.error, Color.red);
            Debug.LogError("Connection error: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("Response: " + response);
            ApiResponse<string> apiResponse = JsonUtility.FromJson<ApiResponse<string>>(response);
            if (apiResponse.success)
            {
                ShowMessage("Reset password successfully!", Color.green);
                loginView.SetActive(true);
                Reset();
                gameObject.SetActive(false);
            }
            else
            {
                ShowMessage("Reset password failed: " + apiResponse.message, Color.red);
                Debug.LogError("Reset password failed: " + apiResponse.message);
            }
        }
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        messageText.color = color;
    }

    private void Reset()
    {
        inOTP.text = "";
        inEmail.text = "";
        inNewPassword.text = "";
        inConfirmPassword.text = "";
    }
}