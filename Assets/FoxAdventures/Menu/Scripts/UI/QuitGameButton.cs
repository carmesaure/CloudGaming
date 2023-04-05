using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    [SerializeField] private Button quitButton = null;
    public Button QuitButton
    {
        get
        {
            if (this.quitButton == null)
                this.quitButton = GetComponent<Button>();

            return this.quitButton;
        }
    }

    void OnEnable()
    {
        // Register to events
        if (this.QuitButton != null)
            this.QuitButton.onClick.AddListener(this.OnQuitGameClick);
    }

    void OnDisable()
    {
        // Register to events
        if (this.QuitButton != null)
            this.QuitButton.onClick.AddListener(this.OnQuitGameClick);
    }

    private void OnQuitGameClick()
    {
        // Logout
        PlayfabAuth.Logout(this.OnLogoutSuccess, this.OnLogoutError);

        // Quit
        Application.Quit();
    }

    private void OnLogoutError()
    {
        // Log
        Debug.LogError("QuitGameButton.OnLogoutError: TODO");
    }

    private void OnLogoutSuccess()
    {
        // Log
        Debug.Log("QuitGameButton.OnLogoutSuccess");
    }
}
