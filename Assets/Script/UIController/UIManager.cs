using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject loginView;
    public GameObject registerView;
    public GameObject forgotPasswordView;
    public GameObject resetPasswordView;
    public GameObject dashboard;

    // Start is called before the first frame update
    private void Start()
    {
        ShowLoginView();
    }

    public void ShowLoginView()
    {
        loginView.SetActive(true);
        registerView.SetActive(false);
        forgotPasswordView.SetActive(false);
        resetPasswordView.SetActive(false);
        dashboard.SetActive(false);
    }

    public void ShowRegisterView()
    {
        loginView.SetActive(false);
        registerView.SetActive(true);
        forgotPasswordView.SetActive(false);
        resetPasswordView.SetActive(false);
        dashboard.SetActive(false);
    }

    public void ShowForgotPasswordView()
    {
        loginView.SetActive(false);
        registerView.SetActive(false);
        forgotPasswordView.SetActive(true);
        resetPasswordView.SetActive(false);
        dashboard.SetActive(false);
    }

    public void ShowResetPasswordView()
    {
        loginView.SetActive(false);
        registerView.SetActive(false);
        forgotPasswordView.SetActive(false);
        resetPasswordView.SetActive(true);
        dashboard.SetActive(false);
    }

    public void ShowDashboard()
    {
        loginView.SetActive(false);
        registerView.SetActive(false);
        forgotPasswordView.SetActive(false);
        resetPasswordView.SetActive(false);
        dashboard.SetActive(true);
    }
}