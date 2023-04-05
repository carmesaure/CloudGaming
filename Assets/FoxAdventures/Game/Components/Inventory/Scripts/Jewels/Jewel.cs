using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    // Flag
    private bool canBeGrabbed = true;

    // Audio - Prefab
    public GameObject grabbedSoundFx = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent potential issues
        if (this.canBeGrabbed == false)
            return;

        // Try to find a player with an inventory attached
        FoxCharacterInventory foxCharacterInventory = other.GetComponentInParent<FoxCharacterInventory>();
        if (foxCharacterInventory != null)
        {
            // Attribute key to inventory
            foxCharacterInventory.jewelsCount += 1;

            // Unset flag
            this.canBeGrabbed = false;

            // Play audio
            if (this.grabbedSoundFx != null)
                GameObject.Instantiate(this.grabbedSoundFx, null);

            // Delete key from scene (and prevent further use)
            GameObject.Destroy(this.gameObject);
        }
    }
}
