using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioSource AudioSource
    {
        get 
        {
            if (this.audioSource == null)
                this.audioSource = GetComponent<AudioSource>();
            return this.audioSource; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.AudioSource != null && this.AudioSource.clip != null)
        {
            // Prevent wrong setup
            this.AudioSource.loop = false;

            // Play sound
            this.AudioSource.Play();

            // Delete at the end of the sound
            GameObject.Destroy(this.gameObject, this.audioSource.clip.length);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
