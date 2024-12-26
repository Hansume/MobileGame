using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangePasswordController : MonoBehaviour
{
    public TMP_InputField inPassword;
    public TMP_InputField inNewPassword;
    public TMP_InputField inConfirmPassword;
    public TMP_Text messageText;
    public Button btnSubmit;
    public Button btnBack;
    public GameObject dashboardView;
    private const string endpoint = "http://26.123.150.88:8080/api/auth/change-password";

    // Start is called before the first frame update
    private void Start()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);

        btnBack.onClick.AddListener(() =>
        {
            dashboardView.SetActive(true);
            gameObject.SetActive(false);
        });

        btnSubmit.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(inPassword.text) || string.IsNullOrEmpty(inNewPassword.text) || string.IsNullOrEmpty(inConfirmPassword.text))
            {
                ShowMessage("Please enter all fields", Color.red);
                return;
            }

            if (!inNewPassword.text.Equals(inConfirmPassword.text))
            {
                ShowMessage("New password and confirm password must be the same", Color.red);
                return;
            }

            string password = inPassword.text;
            string newPassword = inNewPassword.text;
            string confirmPassword = inConfirmPassword.text;
            StartCoroutine(ChangePassword(password, newPassword, confirmPassword));
        });
    }

    [Serializable]
    public class ChangePasswordReq
    {
        public long userId;
        public string oldPassword;
        public string newPassword;
        public string confirmPassword;
    }

    private IEnumerator ChangePassword(string password, string newPassword, string confirmPassword)
    {
        int userId = PlayerPrefs.GetInt("UserId");
        var changePasswordData = new ChangePasswordReq
        {
            userId = (int)userId,
            oldPassword = password,
            newPassword = newPassword,
            confirmPassword = confirmPassword
        };
        string jsonData = JsonUtility.ToJson(changePasswordData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(endpoint, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        string response = request.downloadHandler.text;
        ApiResponse<string> apiResponse = JsonUtility.FromJson<ApiResponse<string>>(response);
        if (apiResponse.success)
        {
            ShowMessage("Password changed successfully", Color.green);
        }
        else
        {
            ShowMessage(apiResponse.message, Color.red);
        }
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        messageText.color = color;
    }
}