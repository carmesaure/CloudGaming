using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    // Rigidbody
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

    #region Animations
    // Animator
    private Animator __animator = null;
    public Animator Animator
    {
        get
        {
            if (this.__animator == null)
                this.__animator = this.GetComponent<Animator>();
            return this.__animator;
        }
    }

    // Animation
    void LateUpdate()
    {
        // Animator
        if (this.Rigidbody2D != null && this.Animator != null) 
        {
            this.Animator.SetFloat("Vertical", this.Rigidbody2D.velocity.y);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        this.nextMoveRightDirection = !this.nextMoveRightDirection;

        // Multiply the player's x local scale by -1.
        Vector3 invertedScale = transform.localScale;
        invertedScale.x *= -1;

        // Apply
        transform.localScale = invertedScale;
    }
    #endregion

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

    void FixedUpdate()
    {
        // Update status
        this.UpdateGroundedStatus();

        // Fix velocity
        if (this.Rigidbody2D != null && this.IsGrounded == true)
        {
            this.Rigidbody2D.velocity = Vector2.zero;
        }
    }

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
                // Ignore triggers
                if (colliders[i].isTrigger == true)
                    continue;

                if (colliders[i].gameObject != gameObject)
                    __isGrounded = true;
            }
        }
    }

    [Header("AI Behaviour")]
    // Wait Time
    public float waitTime = 3.0f;
    private float waitTimeLeft = 0.0f;

    // Move Direction
    public bool nextMoveRightDirection = false;

    // Jump vector
    public Vector2 jumpForce = new Vector2(200f, 400f);

    // Update is called once per frame
    void Update()
    {
        // Only update when grounded
        if (this.IsGrounded == true)
        {
            // Wait
            if (this.waitTimeLeft > 0.0f)
            {
                this.waitTimeLeft -= Time.deltaTime;
            }
            // Wait over, jump
            else
            {
                // Jump
                if (this.Rigidbody2D != null)
                {
                    // Add a vertical force to the player.
                    Vector2 jumpVector = this.jumpForce;
                    if (this.nextMoveRightDirection == false)
                        jumpVector.x *= -1.0f;

                    // Jump force
                    this.Rigidbody2D.AddForce(jumpVector);
                }

                // Should flip ?
                {
                    // Wait
                    this.waitTimeLeft = this.waitTime;

                    // Invert direction
                    this.Flip();
                }
            }
        }
    }
}
