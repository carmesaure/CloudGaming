using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoxPlayer : MonoBehaviour
{
    void Awake()
    {
        // Init Position
        this.InitPositions();

        // Register to health events
        this.RegisterToHealthEvents();
    }

    #region Save Positions / Spawning
    // Vars
    private FoxCharacterController foxCharacterController = null;

    public FoxCharacterController FoxCharacterController
    {
        get
        {
            if (this.foxCharacterController == null)
                this.foxCharacterController = this.gameObject.GetComponentInChildren<FoxCharacterController>();
            return this.foxCharacterController;
        }
    }

    // Spawn / Respawn position
    private Vector2 spawnPosition = Vector2.zero;
    void InitPositions()
    {
        if (this.FoxCharacterController != null)
        {
            this.spawnPosition = this.FoxCharacterController.transform.position;
        }
    }
    #endregion

    #region Health
    // Vars
    private FoxCharacterHealth foxCharacterHealth = null;
    public FoxCharacterHealth FoxCharacterHealth
    {
        get
        {
            if (this.foxCharacterHealth == null)
                this.foxCharacterHealth = this.gameObject.GetComponentInChildren<FoxCharacterHealth>();
            return this.foxCharacterHealth;
        }
    }

    // Called in Awake / OnEnable / OnDisable
    void RegisterToHealthEvents()
    {
        // Register to health events
        if (this.FoxCharacterHealth != null)
        {
            this.FoxCharacterHealth.OnDead.AddListener(this.OnDead);
            this.FoxCharacterHealth.OnRevive.AddListener(this.OnRevive);
        }
    }

    // Health - Events
    private void OnRevive()
    {
        if (this.FoxCharacterController != null)
        {
            // Set back to respawn position
            this.FoxCharacterController.transform.position = this.spawnPosition;

            // Enable back the various components
            {
                // Controller
                this.FoxCharacterController.enabled = true;

                // Camera
                Camera camera = this.GetComponentInChildren<Camera>();
                if (camera != null)
                {
                    FollowTransform cameraFollowTransform = camera.GetComponent<FollowTransform>();
                    if (cameraFollowTransform != null)
                    {
                        // Enable back
                        cameraFollowTransform.enabled = true;

                        // Reset camera position
                        cameraFollowTransform.transform.position = new Vector3(this.spawnPosition.x, this.spawnPosition.y, cameraFollowTransform.transform.position.z);
                    }
                }

                // Box collider
                BoxCollider2D boxCollider2D = this.FoxCharacterController.GetComponent<BoxCollider2D>();
                if (boxCollider2D != null)
                    boxCollider2D.enabled = true;

                // Rigidbody
                Rigidbody2D rigidbody2D = this.FoxCharacterController.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    // Enable rigidbody to fall again
                    rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    
                    // Reset the velocity
                    rigidbody2D.velocity = Vector2.zero;
                }
            }
        }
    }

    private void OnDead()
    {
        if (this.FoxCharacterController != null)
        {
            // Disable components
            {
                // Controller
                this.FoxCharacterController.enabled = false;

                // Camera
                Camera camera = this.GetComponentInChildren<Camera>();
                if (camera != null)
                {
                    FollowTransform cameraFollowTransform = camera.GetComponent<FollowTransform>();
                    if (cameraFollowTransform != null)
                    {
                        cameraFollowTransform.enabled = false;
                    }
                }

                // Box collider
                BoxCollider2D boxCollider2D = this.FoxCharacterController.GetComponent<BoxCollider2D>();
                if (boxCollider2D != null)
                    boxCollider2D.enabled = false;

                // Rigidbody
                Rigidbody2D rigidbody2D = this.FoxCharacterController.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    //// Do not be affected by physics anymore
                    //rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

                    // Reset the velocity
                    rigidbody2D.velocity = Vector2.zero;
                    rigidbody2D.AddForce(Vector2.up * 18f, ForceMode2D.Impulse);
                }
            }
        }
    }
    #endregion

    #region Win Level
    public UnityEvent OnWin = new UnityEvent();
    public virtual void Win()
    {
        // Invoke events
        if (this.OnWin != null)
            this.OnWin.Invoke();

        // TODO: Move this somewhere else
        {
            // Deactivate fox character input
            FoxCharacterControllerInput foxCharacterControllerInput = this.GetComponentInChildren<FoxCharacterControllerInput>();
            if (foxCharacterControllerInput != null)
            {
                foxCharacterControllerInput.enabled = false;
            }

            // Reset data of Character Controller
            FoxCharacterController foxCharacterController = this.GetComponentInChildren<FoxCharacterController>();
            if (foxCharacterController != null)
            {
                foxCharacterController.horizontalInput = 0.0f;
                foxCharacterController.jump = false;
                foxCharacterController.crouch = false;
            }
        }
    }
    #endregion
}
