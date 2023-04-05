using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerStatsView : MonoBehaviour
{
    // Content root
    public Transform contentRoot = null;

    // Content UI
    public Text usernameText = null;
    //
    public Text crystalsCountText = null;
    public Image crystalsIcon = null;

    void OnEnable()
    {
        // Hide
        this.Hide();

        // Trigger news show if logged in
        if (PlayfabAuth.IsLoggedIn == true)
        {
            // Trigger immediate update
            this.UpdateView();
        }
    }

    // Update View
    public void UpdateView()
    {
        this.UpdateUserAccountInfos();
        this.UpdateUserStats();
    }

    // Show / Hide
    void Show()
    {
        if (this.contentRoot != null)
            this.contentRoot.gameObject.SetActive(true);
    }

    void Hide()
    {
        if (this.contentRoot != null)
            this.contentRoot.gameObject.SetActive(false);
    }

    // Update Account Infos
    private void UpdateUserAccountInfos()
    {
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnUpdateUserAccountInfosSuccess, OnUpdateUserAccountInfosError);
    }

    private void OnUpdateUserAccountInfosSuccess(GetAccountInfoResult result)
    {
        // Log
        Debug.LogError("PlayerStatsView.OnUpdateUserAccountInfosSuccess() - Error: TODO");

        if (this.usernameText != null)
        {
            this.usernameText.gameObject.SetActive(true);
            this.usernameText.text = result.AccountInfo.Username;
        }

        // Show
        this.Show();
    }

    private void OnUpdateUserAccountInfosError(PlayFabError error)
    {
        // Log
        Debug.LogError("PlayerStatsView.OnUpdateUserAccountInfosError() - Error: TODO");
    }

    // Update User Stats
    private void UpdateUserStats()
    {
        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, OnGetUserInventorySuccess, OnGetUserInventoryError);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        // Crystals we have
        int crystalsCount = 0;

        // Get crystals count from result
        if (result.VirtualCurrency.TryGetValue("CR", out int crystals))
        {
            crystalsCount = crystals;
        }

        // Update crystals count
        if (this.crystalsCountText != null)
        {
            this.crystalsCountText.gameObject.SetActive(true);
            this.crystalsCountText.text = crystalsCount.ToString();
        }

        if (this.crystalsIcon != null)
        {
            this.crystalsIcon.gameObject.SetActive(true);
        }

        // Show
        this.Show();
    }

    private void OnGetUserInventoryError(PlayFabError error)
    {
        // Update crystals count
        {
            // Update crystal text
            if (this.crystalsCountText != null)
            {
                this.crystalsCountText.gameObject.SetActive(true);
                this.crystalsCountText.text = "???";
            }

            // Show crystal icon
            if (this.crystalsIcon != null)
                this.crystalsIcon.gameObject.SetActive(true);
        }
    }
}
