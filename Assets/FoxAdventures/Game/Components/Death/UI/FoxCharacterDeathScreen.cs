using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCharacterDeathScreen : MonoBehaviour
{
    // Health component of player
    [SerializeField] private FoxCharacterHealth foxCharacterHealth = null;
    public FoxCharacterHealth FoxCharacterHealth
    {
        get
        {
            if (this.foxCharacterHealth == null)
                this.foxCharacterHealth = this.gameObject.GetComponentInParent<FoxCharacterHealth>();
            return this.foxCharacterHealth;
        }
    }

    // Root of the view
    [SerializeField] private Transform deathScreenRoot = null;

    void Awake()
    {
        // Register to health events
        if (this.FoxCharacterHealth != null)
        {
            this.FoxCharacterHealth.OnDead.AddListener(this.OnDead);
            this.FoxCharacterHealth.OnRevive.AddListener(this.OnRevive);
        }

        // Hide
        if (this.deathScreenRoot != null)
            this.deathScreenRoot.gameObject.SetActive(false);
    }

    // UI Events
    public void OnRespawnButtonClicked()
    {
        if (this.FoxCharacterHealth != null)
            this.FoxCharacterHealth.Revive();
    }

    // Events
    public void OnDead()
    {
        // Call later
        this.Invoke("ShowDeadScreen", 1.2f);
    }
    public void OnRevive()
    {
        if (this.deathScreenRoot != null)
            this.deathScreenRoot.gameObject.SetActive(false);
    }

    // Events - Callback
    public void ShowDeadScreen()
    {
        if (this.deathScreenRoot != null)
            this.deathScreenRoot.gameObject.SetActive(true);
    }
}
