using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ForgotPasswordController : MonoBehaviour
{
    public TMP_InputField inEmail;
    public Button btnSubmit;
    public Button btnBack;
    public GameObject loginView;
    public GameObject resetView;
    public TMP_Text messageText;
    private const string endpoint = "http://26.123.150.88:8080/api/auth/forgot-password";

    private void Start()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);

        btnBack.onClick.AddListener(() =>
        {
            loginView.SetActive(true);
            gameObject.SetActive(false);
        });

        btnSubmit.onClick.AddListener(() =>
        {
            if (ValidateInput())
            {
                StartCoroutine(ForgotPassword(inEmail.text));
            }
        });
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(inEmail.text))
        {
            ShowMessage("Please enter email", Color.red);
            return false;
        }

        return true;
    }

    [Serializable]
    public class ForgotPasswordReq
    {
        public string email;
    }

    private IEnumerator ForgotPassword(string email)
    {
        var forgotData = new ForgotPasswordReq
        {
            email = email,
        };

        string jsonData = JsonUtility.ToJson(forgotData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();

        ShowMessage("Password has been sent to your email", Color.green);

        yield return new WaitForSeconds(2);

        PlayerPrefs.SetString("fgEmail", email);
        PlayerPrefs.Save();
        resetView.SetActive(true);
        gameObject.SetActive(false);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Connection error: " + request.error);
        }
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        messageText.color = color;
    }
}