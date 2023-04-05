using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FoxCharacterController : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        // Etape 0 - Update physics status
        this.UpdateGroundedStatus();

        // Etape 1 - Mouvement Horizontal
        this.HandleHorizontalMove();

        // Etape 2 - Flip Texture suivant orientation
        this.HandleFlip();

        // Etape 3 - Saut
        this.HandleJump();
    }

    #region Etape 1 - Mouvement Horizontal
    private Rigidbody2D __rigidbody2D = null;
    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (this.__rigidbody2D == null)
                this.__rigidbody2D = this.GetComponent<Rigidbody2D>();
            return this.__rigidbody2D;
        }
    }

    // Velocity
    private Vector3 velocity = Vector3.zero;

    [Header("Behaviour")]
    public float moveSpeedFactor = 10.0f;
    [Range(0, .3f)]
    public float moveDampFactor = 0.0f;

    [Header("Input")]
    [Range(-1f, 1f)]
    public float horizontalInput = 0f;
    public bool jump = false;
    public bool crouch = false;

    // Can control ?
    public bool canControl = true;

    //
    private void HandleHorizontalMove()
    {
        if (this.canControl == false)
            return;

        //// Etape 4 - Add conditions to the movement
        //if (this.isGrounded == true || this.airControl == true)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(this.horizontalInput * this.moveSpeedFactor, this.Rigidbody2D.velocity.y);

            // Crouch movement
            if (this.IsGrounded == true && this.crouch == true)
                targetVelocity.x *= 0.5f;

            // And then smoothing it out and applying it to the character
            //this.Rigidbody2D.velocity = Vector3.SmoothDamp(this.Rigidbody2D.velocity, targetVelocity, ref velocity, this.moveDampFactor);
            this.Rigidbody2D.velocity = targetVelocity;
        }
    }
    #endregion

    #region Etape 2 - Flip Texture suivant orientation
    private bool __FacingRight = true;
    //
    private SpriteRenderer spriteRenderer = null;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (this.spriteRenderer == null)
                this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            return this.spriteRenderer;
        }
    }

    private void HandleFlip()
    {
        // If the input is moving the player right and the player is facing left...
        if (this.horizontalInput > 0 && __FacingRight == false)
        {
            // Flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (this.horizontalInput < 0 && __FacingRight == true)
        {
            // Flip the player.
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        __FacingRight = !__FacingRight;

        //// Multiply the player's x local scale by -1.
        //Vector3 invertedScale = transform.localScale;
        //invertedScale.x *= -1;

        //// Apply
        //transform.localScale = invertedScale;

        if (this.SpriteRenderer != null)
            this.SpriteRenderer.flipX = (__FacingRight == false);
    }
    #endregion

    #region Etape 3 - Saut
    [Header("Physics")]
    // Origin of the physics check to see if we are grounded
    [SerializeField] 
    private Transform groundCheck = null;
    // Radius of the overlap circle to determine if grounded
    private const float k_GroundedRadius = 0.2f;
    // LayerMask of ground for ground check
    public LayerMask groundCheckLayers;
    // Are we grounded ?
    private bool __isGrounded = false;
    public bool IsGrounded
    {
        get
        {
            return this.__isGrounded;
        }
    }

    [Header("Jump")]
    public float jumpForce = 400.0f;
    
    // Can you change direction in the air ?
    public bool airControl = true;

    [Header("Jump - Audio")]
    public AudioSource jumpAudioSource = null;

    private void UpdateGroundedStatus()
    {
        // Check values
        if (this.groundCheck == null)
            return;

        // Update grounded status
        {
            // Unset flag
            this.__isGrounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.groundCheck.position, k_GroundedRadius, groundCheckLayers);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    __isGrounded = true;
            }
        }
    }

    private void HandleJump()
    {
        // Can control?
        if (this.canControl == false)
            return;

        // If the player should jump
        if (__isGrounded == true)
        {
            // Did play request for a jump ?
            if (jump == true)
            {
                // Unset flag
                this.__isGrounded = false;

                // Add a vertical force to the player.
                this.Rigidbody2D.AddForce(new Vector2(0f, jumpForce));

                // Play audio
                if (this.jumpAudioSource != null)
                    this.jumpAudioSource.Play();

                //// Reset input
                //this.jump = false;
            }
        }

        // Reset input
        this.jump = false;
    }
    #endregion
}
