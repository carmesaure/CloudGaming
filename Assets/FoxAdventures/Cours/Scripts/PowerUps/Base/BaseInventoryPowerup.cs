using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInventoryPowerup : MonoBehaviour
{

    [Header("Reference in Inventory")]
    [SerializeField] protected string catalogItemID = string.Empty;
    public virtual string CatalogItemID
    {
        get
        {
            return this.catalogItemID;
        }
    }

    protected virtual void OnEnable()
    {
        // Inventory is setup?
        if (PlayfabInventory.Instance != null)
        {
            // Register to events
            PlayfabInventory.Instance.OnInventoryUpdateSuccess.AddListener(this.OnInventoryUpdateSuccess);
            PlayfabInventory.Instance.OnInventoryUpdateError.AddListener(this.OnInventoryUpdateError);
        }

        // Update view to init
        this.UpdatePowerupStatus();
    }

    protected virtual void OnDisable()
    {
        // Inventory is setup?
        if (PlayfabInventory.Instance != null)
        {
            // Unregister to events
            PlayfabInventory.Instance.OnInventoryUpdateSuccess.RemoveListener(this.OnInventoryUpdateSuccess);
            PlayfabInventory.Instance.OnInventoryUpdateError.RemoveListener(this.OnInventoryUpdateError);
        }
    }

    protected virtual void OnInventoryUpdateSuccess()
    {
        this.UpdatePowerupStatus();
    }

    protected virtual void OnInventoryUpdateError()
    {
        this.UpdatePowerupStatus();
    }

    // Update powerup
    private bool poweredUp = false;
    protected virtual void UpdatePowerupStatus()
    {
        // Default flag: false means we don't have the powerup
        this.poweredUp = false;

        // Check the inventory & check that we own the powerup, if yes we change the flag
        if (PlayfabInventory.Instance != null && PlayfabInventory.Instance.Possess(this.catalogItemID) == true)
            this.poweredUp = true;
    }
}
