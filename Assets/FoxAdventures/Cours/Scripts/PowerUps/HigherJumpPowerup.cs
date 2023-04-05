using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherJumpPowerup : BaseInventoryPowerup
{
    // Controller
    private FoxCharacterController foxCharacterController = null;
    public FoxCharacterController FoxCharacterController
    {
        get
        {
            if (this.foxCharacterController == null)
                this.foxCharacterController = this.GetComponentInChildren<FoxCharacterController>();
            return this.foxCharacterController;
        }
    }

    [Header("Behaviour")]
    public float jumpBoost = 200f;                                          // Applied result boost
    private float foxCharacterControllerDefaultJumpForce = 0.0f;            // Initial status

    protected override void OnEnable()
    {
        // Base
        base.OnEnable();

        // Register to some variables (like initial jump force)
        if (this.FoxCharacterController != null)
        {
            this.foxCharacterControllerDefaultJumpForce = this.FoxCharacterController.jumpForce;
        }
    }

    // Update powerup
    private bool poweredUp = false;
    protected override void UpdatePowerupStatus()
    {
        // Base call
        base.UpdatePowerupStatus();

        // in the fox character controller, we will change the jump force
        if (this.FoxCharacterController != null)
        {
            // If powered up, Apply
            if (this.poweredUp == true)
            {
                this.FoxCharacterController.jumpForce = this.foxCharacterControllerDefaultJumpForce + this.jumpBoost;
            }
            // Else, Revert
            else
            {
                this.FoxCharacterController.jumpForce = this.foxCharacterControllerDefaultJumpForce;
            }
        }
    }
}
