using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
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

    [Header("Hearts")]
    [SerializeField] private List<Image> hearts = new List<Image>();

    [Header("Sprites of hearts")]
    public Sprite heartFull = null;
    public Sprite heartHalf = null;
    public Sprite heartEmpty = null;

    void Update()
    {
        if (this.FoxCharacterHealth != null && this.FoxCharacterHealth.currentHealthPoints > 0 && this.FoxCharacterHealth.maxHealthPoints > 0)
        {
            // Show
            this.Show();

            // Update
            if (this.hearts != null && this.hearts.Count > 0)
            {
                for (int i = 0; i < this.hearts.Count; i++)
                {
                    // Count hearts depending on i
                    int healthEmpty = (i * 2);
                    int healthHalf = healthEmpty + 1;
                    int healthFull = healthEmpty + 2;

                    // Full
                    if (healthFull <= this.FoxCharacterHealth.currentHealthPoints)
                    {
                        this.hearts[i].sprite = this.heartFull;
                    }
                    else
                    {
                        if (healthHalf == this.FoxCharacterHealth.currentHealthPoints)
                        {
                            this.hearts[i].sprite = this.heartHalf;
                        }
                        else
                        {
                            this.hearts[i].sprite = this.heartEmpty;
                        }
                    }
                }
            }
        }
        else
        {
            // Hide view
            this.Hide();
        }
    }

    void Show()
    {
        if (this.hearts != null && this.hearts.Count > 0)
        {
            for (int i=0; i<this.hearts.Count; i++)
            {
                this.hearts[i].gameObject.SetActive(true);
            }
        }
    }

    void Hide()
    {
        if (this.hearts != null && this.hearts.Count > 0)
        {
            for (int i = 0; i < this.hearts.Count; i++)
            {
                this.hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
