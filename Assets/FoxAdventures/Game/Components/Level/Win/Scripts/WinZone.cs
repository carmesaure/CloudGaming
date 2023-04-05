using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WinZone : MonoBehaviour
{
    private AudioSource winAudioSource = null;
    public AudioSource WinAudioSource
    {
        get
        {
            if (this.winAudioSource == null)
                this.winAudioSource = GetComponentInChildren<AudioSource>();
            return this.winAudioSource;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Try to find a player with an inventory attached
        FoxPlayer foxPlayer = other.GetComponentInParent<FoxPlayer>();
        if (foxPlayer != null)
        {
            // Trigger Win
            foxPlayer.Win();

            // Play sound
            if (this.WinAudioSource != null)
                this.WinAudioSource.Play();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
        Gizmos.DrawCube(this.transform.position, this.transform.localScale);
    }
}
