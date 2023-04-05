using System;
using UnityEngine;

public class PlayerGetHitBehaviour : MonoBehaviour
{
    public float invincibilityDuration = 2f; // duration of invincibility after being hit
    public float knockbackDistance = 2f; // distance to knock the player back
    private float invincibilityTimer; // timer for invincibility
    private bool isInvincible = false; // whether the player is currently invincible
    public bool IsInvincible { get { return isInvincible; } }
    private Rigidbody2D rb; // the player's rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool canStartInvicibilityTimer = false;
    public bool hasTouchedGround = false;
    void Update()
    {
        if (hasTouchedGround == true)
        {
            // decrease invincibility timer
            if (invincibilityTimer > 0)
            {
                invincibilityTimer -= Time.deltaTime;
            }
            // if invincibility timer reaches 0, player is no longer invincible
            else if (isInvincible)
            {
                isInvincible = false;
            }
        }
        else if (canStartInvicibilityTimer == true)
        {
            FoxCharacterController foxCharacterController = this.GetComponent<FoxCharacterController>();
            if (foxCharacterController != null)
            {
                if (foxCharacterController.IsGrounded == true)
                {
                    // has touched ground
                    hasTouchedGround = true;
                    // Restore control of player
                    foxCharacterController.canControl = true;
                }
            }
        }

        UpdateInvincibilityView();
    }

    public float invincibilityInvisibleRefreshRate = 0.1f;
    private float invincibilityInvisibleTime = 0f;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    private void UpdateInvincibilityView()
    {
        if (this.spriteRenderer == null)
            return;

        if (this.IsInvincible == true)
        {
            this.invincibilityInvisibleTime -= Time.deltaTime;
            if (this.invincibilityInvisibleTime < 0f)
            {
                this.invincibilityInvisibleTime += invincibilityInvisibleRefreshRate;
                this.spriteRenderer.enabled = !this.spriteRenderer.enabled;
            }
        }
        else
        {
            if (this.spriteRenderer.enabled == false)
                this.spriteRenderer.enabled = true;
        }
    }

    [SerializeField] private AudioSource getHitAudioSource = null;
    public void GetHit(EnemyDamageZone enemyDamageZone)
    {
        if (this.isInvincible == false && enemyDamageZone != null)
        {
            // Remove control of player
            FoxCharacterController foxCharacterController = this.GetComponent<FoxCharacterController>();
            if (foxCharacterController != null)
                foxCharacterController.canControl = false;

            // Prevent physics issues
            rb.velocity = Vector2.zero;

            // apply knockback
            Vector2 knockbackDirection = (transform.position - enemyDamageZone.transform.position).normalized;
            knockbackDirection.y = 0;
            knockbackDirection = knockbackDirection.normalized;
            knockbackDirection.y = 3f;
            knockbackDirection = knockbackDirection.normalized;
            rb.AddForce(knockbackDirection * knockbackDistance, ForceMode2D.Impulse);

            // start invincibility timer
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            // Reset flag
            hasTouchedGround = false;
            canStartInvicibilityTimer = false;

            // Audio
            if (this.getHitAudioSource != null)
                this.getHitAudioSource.PlayOneShot(this.getHitAudioSource.clip);

            // Wait some timer
            this.Invoke("StartInvicibilityTimer", 0.45f);
        }
    }

    public void StartInvicibilityTimer()
    {
        canStartInvicibilityTimer = true;
    }
}
