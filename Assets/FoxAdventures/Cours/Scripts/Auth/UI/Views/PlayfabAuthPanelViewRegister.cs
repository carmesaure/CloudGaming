using UnityEngine;
using UnityEngine.UI;

public class PlayfabAuthPanelViewRegister : PlayfabAuthPanelView
{
    [Header("Login View")]
    [SerializeField] protected InputField inputFieldUsername = null;
    [SerializeField] protected InputField inputFieldEmail = null;
    [SerializeField] protected InputField inputFieldPassword = null;
    [SerializeField] protected Toggle toggleRemember = null;


    protected void OnEnable()
    {
        // Load previously saved data
        {
            if (PlayerPrefs.HasKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername) == true)
            {
                if (this.inputFieldUsername != null)
                    this.inputFieldUsername.text = PlayerPrefs.GetString(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername);
            }

            if (PlayerPrefs.HasKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail) == true)
            {
                if (this.inputFieldEmail != null)
                    this.inputFieldEmail.text = PlayerPrefs.GetString(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail);
            }

            if (PlayerPrefs.HasKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword) == true)
            {
                if (this.inputFieldPassword != null)
                    this.inputFieldPassword.text = PlayerPrefs.GetString(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword);
            }
        }
    }

    public void OnRegisterButtonClicked()
    {
        this.TryRegister();
    }

    private void TryRegister()
    {
        // Check setup
        if (this.inputFieldUsername == null || this.inputFieldEmail == null || this.inputFieldPassword == null)
            return;

        // Get input
        string username = this.inputFieldUsername.text;
        string email = this.inputFieldEmail.text;
        string password = this.inputFieldPassword.text;

        // Check input
        if (string.IsNullOrWhiteSpace(email) == false && string.IsNullOrWhiteSpace(password) == false)
        {
            // Call API
            PlayfabAuth.TryRegisterWithEmail(email, password, username, this.OnRegisterSuccess, this.OnRegisterError);
        }
    }

    private void OnRegisterSuccess()
    {
        // Log
        Debug.LogWarning("PlayfabAuthPanelViewRegister.OnRegisterSuccess() - TODO");

        // Show login view
        if (this.PlayfabAuthPanel != null)
            this.PlayfabAuthPanel.ShowLogin();
    }

    private void OnRegisterError()
    {
        // Log
        Debug.LogError("PlayfabAuthPanelViewRegister.OnRegisterError() - Error: TODO");
    }
}
