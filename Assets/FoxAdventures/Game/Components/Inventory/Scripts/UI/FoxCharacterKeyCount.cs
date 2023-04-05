using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxCharacterKeyCount : MonoBehaviour
{
    [SerializeField] private FoxCharacterInventory foxCharacterInventory = null;
    public FoxCharacterInventory FoxCharacterInventory
    {
        get
        {
            if (this.foxCharacterInventory == null)
                this.foxCharacterInventory = this.gameObject.GetComponentInParent<FoxCharacterInventory>();
            return this.foxCharacterInventory;
        }
    }

    [Header("UI References")]
    public Image keyImage = null;
    public Text keyText = null;

    void Update()
    {
        if (this.FoxCharacterInventory != null && this.FoxCharacterInventory.keyCount > 0)
        {
            // Show
            this.Show();

            // Update
            if (this.keyText != null)
                this.keyText.text = this.FoxCharacterInventory.keyCount.ToString();
        }
        else
        {
            // Hide view
            this.Hide();
        }
    }

    void Show()
    {
        if (this.keyImage != null)
            this.keyImage.gameObject.SetActive(true);
        if (this.keyText != null)
            this.keyText.gameObject.SetActive(true);
    }

    void Hide()
    {
        if (this.keyImage != null)
            this.keyImage.gameObject.SetActive(false);
        if (this.keyText != null)
            this.keyText.gameObject.SetActive(false);
    }
}
