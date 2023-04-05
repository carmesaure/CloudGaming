using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
    public string itemID = string.Empty;                // ID in the catalog
    public string itemDisplayName = string.Empty;       // Displayed name
    public string itemImageURL = string.Empty;          // Image to display (found in Resources)
    public bool isUnique = false;                       // Unique item or consumable ?
    public uint itemPrice = 0;                          // Prix 
}

public class ShopEntry : MonoBehaviour
{
    [Header("Data")]
    public ShopItem shopItem = null;

    [Header("UI Elements")]
    public Image itemSprite = null;
    public Text itemNameText = null;
    public Text itemValueText = null;
    public Image itemValueSprite = null;

    // OnEnable / OnDisable
    void OnEnable()
    {
        // Inventory is setup?
        if (PlayfabInventory.Instance != null)
        {
            // Register to events
            PlayfabInventory.Instance.OnInventoryUpdateSuccess.AddListener(this.OnInventoryUpdateSuccess);
            PlayfabInventory.Instance.OnInventoryUpdateError.AddListener(this.OnInventoryUpdateError);
        }

        //// Update view to init
        //this.UpdateView();
    }

    void OnDisable()
    {
        // Inventory is setup?
        if (PlayfabInventory.Instance != null)
        {
            // Unregister to events
            PlayfabInventory.Instance.OnInventoryUpdateSuccess.RemoveListener(this.OnInventoryUpdateSuccess);
            PlayfabInventory.Instance.OnInventoryUpdateError.RemoveListener(this.OnInventoryUpdateError);
        }
    }

    // Update View
    public void SetValue(ShopItem _shopItem)
    {
        // Save catalog item
        this.shopItem = _shopItem;

        // Update view
        this.UpdateView();
    }

    public void UpdateView()
    {
        // Check item is set
        if (this.shopItem != null)
        {
            // Determine some data from the catalog item itself
            bool isUnique = (this.shopItem.isUnique == true);
            bool isPossessed = false;

            // If unique & already in inventoryk, specific view
            if (isUnique == true && PlayfabInventory.Instance.Possess(this.shopItem.itemID) == true)
            {
                // Mark as possessed
                isPossessed = true;
            }

            // Get data
            string itemImageURL = this.shopItem.itemImageURL;
            string itemName = this.shopItem.itemDisplayName;
            uint itemPrice = this.shopItem.itemPrice;

            // Update sprite
            if (this.itemSprite != null)
            {
                Sprite sprite = (string.IsNullOrWhiteSpace(itemImageURL) == false ? Resources.Load<Sprite>(itemImageURL) : null);
                if (sprite != null)
                    this.itemSprite.sprite = sprite;
                else
                    this.itemSprite.sprite = null;
            }

            // Update name
            if (this.itemNameText != null)
                this.itemNameText.text = itemName;

            // If we have bough the item
            if (PlayfabInventory.Instance != null && PlayfabInventory.Instance.Inventory != null)
            {
                // If already possessed,
                if (isPossessed == true)
                {
                    // Hide item price image
                    if (this.itemValueSprite != null)
                        this.itemValueSprite.gameObject.SetActive(false);

                    // Update value
                    if (this.itemValueText != null)
                    {
                        //this.itemValueText.alignment = TextAnchor.MiddleCenter;
                        this.itemValueText.text = "Owned";
                    }
                }
                else
                {
                    // Show item price image
                    if (this.itemValueSprite != null)
                        this.itemValueSprite.gameObject.SetActive(true);

                    // Update value
                    if (this.itemValueText != null)
                    {
                        //this.itemValueText.alignment = TextAnchor.MiddleLeft;
                        this.itemValueText.text = itemPrice.ToString();
                    }
                }
            }
        }
    }

    // UI Interactions
    public void OnBuyButtonClick()
    {
        this.TryBuyItem();
    }

    // Buy item
    public void TryBuyItem()
    {
        // Determine some data from the catalog item itself
        bool isUnique = (this.shopItem.isUnique == true);
        bool isPossessed = false;

        // If already in inventory
        if (PlayfabInventory.Instance.Possess(this.shopItem.itemID) == true)
        {
            // Mark as possessed
            isPossessed = true;
        }

        // If unique & already possessed, prevent buy
        if (isUnique == true && isPossessed == true)
        {
            Debug.LogWarning("ShopEntry.TryBuyItem() - " + this.gameObject.name + ": Prevent buy as it's unique & already possessed");
            return;
        }

        // TODO: Trigger item purchasing
        this.OnPurchaseItemSuccess();
    }

    private void OnPurchaseItemSuccess()
    {
        // Log
        Debug.Log("PlayerStatsView.OnPurchaseItemSuccess()");

        // Update inventory
        if (PlayfabInventory.Instance != null)
            PlayfabInventory.Instance.UpdateInventory();
    }

    private void OnPurchaseItemError()
    {
        // Log
        Debug.LogError("PlayerStatsView.OnUpdateUserAccountInfosError() - Error: TODO");
    }

    // Playfab Inventory events we register to
    private void OnInventoryUpdateSuccess()
    {
        this.UpdateView();
    }

    private void OnInventoryUpdateError()
    {
        this.UpdateView();
    }
}
