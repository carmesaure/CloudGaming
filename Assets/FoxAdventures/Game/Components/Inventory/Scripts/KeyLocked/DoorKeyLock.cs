using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyLock : MonoBehaviour
{
    // Door
    public MovingPlatform doorMovingPlatform = null;

    // Flag
    private bool opened = false;

    // Audio
    public AudioSource openDoorAudioSource = null;

    void Start()
    {
        // Disable door to prevent it from moving
        if (this.doorMovingPlatform != null)
            this.doorMovingPlatform.enabled = false;
    }

    // Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent potential issues
        if (this.opened == true)
            return;

        // Try to find a player with an inventory attached
        FoxCharacterInventory foxCharacterInventory = other.GetComponentInParent<FoxCharacterInventory>();
        if (foxCharacterInventory != null)
        {
            // If enough keys
            if (foxCharacterInventory.keyCount > 0)
            {
                // Remove a key from inventory
                foxCharacterInventory.keyCount -= 1;

                // Enabling door will make it move
                if (this.doorMovingPlatform != null)
                    this.doorMovingPlatform.enabled = true;

                // Unset flag
                this.opened = false;

                // Audio - Play
                if (this.openDoorAudioSource != null)
                    this.openDoorAudioSource.Play();

                // Delete key from scene (and prevent further use)
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
