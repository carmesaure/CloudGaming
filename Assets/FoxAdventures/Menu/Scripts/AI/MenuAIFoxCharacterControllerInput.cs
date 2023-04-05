using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAIFoxCharacterControllerInput : FoxCharacterControllerInput
{
    [Header("Input")]
    [Range(-1f, 1f)]
    [SerializeField] private float horizontalInput = 0f;
    public float HorizontalInput
    {
        get
        {
            return horizontalInput;
        }
        set
        {
            horizontalInput = value;

            if (this.FoxCharacterController != null)
                this.FoxCharacterController.horizontalInput = horizontalInput;
        }
    }

    [SerializeField] private bool jump = false;
    public bool Jump
    {
        get
        {
            return jump;
        }
        set
        {
            jump = value;

            if (this.FoxCharacterController != null)
                this.FoxCharacterController.jump = jump;
        }
    }

    [SerializeField] private bool crouch = false;
    public bool Crouch
    {
        get
        {
            return crouch;
        }
        set
        {
            crouch = value;

            if (this.FoxCharacterController != null)
                this.FoxCharacterController.crouch = crouch;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Input for horizontal movement
        this.FoxCharacterController.horizontalInput = this.HorizontalInput;

        //// Jump
        //this.FoxCharacterController.jump = this.Jump;

        // Crouch
        this.FoxCharacterController.crouch = this.Crouch;
    }
}
