using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        //  Knockback
        PlayerGetHitBehaviour playerGetHitBehaviour = other.GetComponentInParent<PlayerGetHitBehaviour>();
        if (playerGetHitBehaviour != null && playerGetHitBehaviour.IsInvincible == false)
        {
            // Hit
            playerGetHitBehaviour.GetHit(this);

            // Try to find a player with an inventory attached
            FoxCharacterHealth foxCharacterHealth = other.GetComponentInParent<FoxCharacterHealth>();
            if (foxCharacterHealth != null)
            {
                // Attribute key to inventory
                foxCharacterHealth.Damage(1);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
        Gizmos.DrawCube(this.transform.position, this.transform.localScale);
    }
}
