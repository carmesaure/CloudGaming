using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    private AudioSource deathAudioSource = null;
    public AudioSource DeathAudioSource
    {
        get
        {
            if (this.deathAudioSource == null)
                this.deathAudioSource = GetComponentInChildren<AudioSource>();
            return this.deathAudioSource;
        }
    }

    //// Uncomment for autosetup on a new gameobject
    //void Awake()
    //{
    //    BoxCollider2D boxCollider2D = this.GetComponent<BoxCollider2D>();
    //    if (boxCollider2D != null)
    //        boxCollider2D.isTrigger = true;
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Try to find a player with an inventory attached
        FoxCharacterHealth foxCharacterHealth = other.GetComponentInParent<FoxCharacterHealth>();
        if (foxCharacterHealth != null)
        {
            // Attribute key to inventory
            foxCharacterHealth.Kill();

            // Play sound
            if (this.DeathAudioSource != null)
                this.DeathAudioSource.Play();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
        Gizmos.DrawCube(this.transform.position, this.transform.localScale);
    }
}
