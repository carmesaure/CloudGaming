using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoxCharacterHealth : MonoBehaviour
{
    [Header("HPs")]
    public int maxHealthPoints = 3;
    public int currentHealthPoints = 3;
    
    // Getters
    public bool IsDead
    {
        get
        {
            return (this.currentHealthPoints <= 0);
        }
    }

    [Header("Events")]
    public UnityEvent OnDead = new UnityEvent();
    public UnityEvent OnRevive = new UnityEvent();


    // Heal / Damage
    public void Heal(int hp, bool _revive = false)
    {
        // Check input
        if (hp <= 0)
            return;

        // If we aren't dead yet, we can heal
        if (this.IsDead == false)
        {
            // Heal
            this.currentHealthPoints = Mathf.Clamp(this.currentHealthPoints + hp, 0, this.maxHealthPoints);
        }
        else if (_revive == true)
        {
            // Heal
            this.currentHealthPoints = Mathf.Clamp(this.currentHealthPoints + hp, 0, this.maxHealthPoints);

            // Event
            if (this.OnRevive != null)
                this.OnRevive.Invoke();
        }
    }

    public void Damage(int hp)
    {
        // Check input
        if (hp <= 0)
            return;

        // If we aren't dead yey,
        if (this.IsDead == false)
        {
            // Damage
            this.currentHealthPoints = Mathf.Clamp(this.currentHealthPoints - hp, 0, this.maxHealthPoints);

            // If we are dead now,
            if (this.IsDead == true)
            {
                // Notify event that we are
                if (this.OnDead != null)
                    this.OnDead.Invoke();
            }
        }
    }

    // Kill / Respawn
    public void Kill()
    {
        // Instant death
        this.Damage(this.maxHealthPoints);
    }

    public void Revive()
    {
        // Revive
        this.Heal(this.maxHealthPoints, true);
    }


    // Editor debugging
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) == true)
            this.Kill();
    }
#endif
}
