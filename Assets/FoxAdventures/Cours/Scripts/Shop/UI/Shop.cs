using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public string shopName = "";
    public GameObject shopEntryPrefab = null;
    //
    private List<ShopEntry> shopEntries = new List<ShopEntry>();

    // Start is called before the first frame update
    void OnEnable()
    {
        // Deactivate prefab in case
        if (this.shopEntryPrefab != null)
            this.shopEntryPrefab.gameObject.SetActive(false);

        // Refresh prefab
        this.RefreshLeaderboard();
    }

    // Refresh
    public void RefreshLeaderboard()
    {
        // Check prefab
        if (this.shopEntryPrefab == null)
            return;

        // Trigger news show if logged in
        if (PlayfabAuth.IsLoggedIn == true)
        {
            // TODO: Retrieve item from our playfab inventory to know what we already bought
            this.OnGetCatalogItemsSuccess(); // Fake
        }
    }

    private void OnGetCatalogItemsSuccess()
    {
        // Clear existing entries
        this.ClearExistingEntries();

        // TODO: Retrieve data
        List<ShopItem> items = new List<ShopItem>();

        // Go through scores
        if (items != null)
        {
            for (int i = 0; i < items.Count; i++)
            {
                ShopItem shopItem = items[i];
                if (shopItem != null)
                {
                    // Instantiate object copy
                    GameObject shopEntryGameobjectCopy = GameObject.Instantiate(this.shopEntryPrefab, this.shopEntryPrefab.transform.parent);
                    if (shopEntryGameobjectCopy != null)
                    {
                        // Activate at our prefab is deactivated
                        shopEntryGameobjectCopy.gameObject.SetActive(true);

                        // set name
                        shopEntryGameobjectCopy.name = ("ShopItemEntry (" + shopItem.itemDisplayName + ")");

                        // Get leaderboard entry
                        ShopEntry shopEntry = shopEntryGameobjectCopy.GetComponent<ShopEntry>();
                        if (shopEntry != null)
                        {
                            // Set value
                            shopEntry.SetValue(shopItem);

                            // Add to list
                            if (this.shopEntries == null)
                                this.shopEntries = new List<ShopEntry>();
                            this.shopEntries.Add(shopEntry);
                        }
                        // Else, destroy object we just spawned
                        else
                        {
                            GameObject.Destroy(shopEntryGameobjectCopy);
                        }
                    }
                }
            }
        }
    }

    private void OnGetCatalogItemsError()
    {
        // TODO
        Debug.LogError("Shop.OnGetCatalogItemsError() - Error: TODO");
    }

    // Clear existing entries
    public void ClearExistingEntries()
    {
        if (this.shopEntries != null)
        {
            while (this.shopEntries.Count > 0)
            {
                if (this.shopEntries[0] != null)
                {
                    GameObject.Destroy(this.shopEntries[0].gameObject);
                }

                // Remove first entry
                this.shopEntries.RemoveAt(0);
            }
        }
    }
}
