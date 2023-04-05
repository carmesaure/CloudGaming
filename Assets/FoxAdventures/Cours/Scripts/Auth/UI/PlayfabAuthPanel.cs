using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabAuthPanel : MonoBehaviour
{
    // Root of views
    public GameObject authenticatedUIRoot = null;
    public GameObject loginUIRoot = null;

    void OnEnable()
    {
        if (PlayfabAuth.IsLoggedIn == false)
        {
            // Show Login
            this.ShowLogin();

            // Show all login views
            this.ShowAll();
        }
        else
        {
            // Hide login views
            this.HideAll();
        }
    }

    // Show / Hide all
    public void ShowAll()
    {
        // Hide authenticated views
        if (this.authenticatedUIRoot != null)
            this.authenticatedUIRoot.SetActive(false);

        // Show login views
        if (this.loginUIRoot != null)
            this.loginUIRoot.SetActive(true);
    }
    public void HideAll()
    {
        // Show authenticated views
        if (this.authenticatedUIRoot != null)
            this.authenticatedUIRoot.SetActive(true);

        // Hide login views
        if (this.loginUIRoot != null)
            this.loginUIRoot.SetActive(false);
    }

    // Login
    public PlayfabAuthPanelViewLogin loginView = null;
    // Register
    public PlayfabAuthPanelViewRegister registerView = null;
    //
    private bool loginShown = true;

    // Views
    public void ShowLogin()
    {
        this.loginShown = true;
        this.ReorderTabs();
    }
    public void ShowRegistration()
    {
        this.loginShown = false;
        this.ReorderTabs();
    }

    // Reorder
    public void ReorderTabs()
    {
        if (this.loginShown == false)
        {
            this.loginView.transform.SetSiblingIndex(0);
            this.registerView.transform.SetSiblingIndex(1);
        }
        else
        {
            this.registerView.transform.SetSiblingIndex(0);
            this.loginView.transform.SetSiblingIndex(1);
        }
    }
}
