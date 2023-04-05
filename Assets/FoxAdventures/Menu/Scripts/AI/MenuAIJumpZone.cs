using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MenuAIJumpZone : MonoBehaviour
{
    // Collider
    private BoxCollider2D boxCollider2D = null;
    public BoxCollider2D BoxCollider2D
    {
        get
        {
            if (this.boxCollider2D == null)
                this.boxCollider2D = this.GetComponent<BoxCollider2D>();
            return this.boxCollider2D;
        }
    }

    // Physics Events
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D != null)
        {
            MenuAIFoxCharacterControllerInput menuAIFoxCharacterControllerInput = collider2D.gameObject.GetComponent<MenuAIFoxCharacterControllerInput>();
            if (menuAIFoxCharacterControllerInput != null)
            {
                menuAIFoxCharacterControllerInput.Jump = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D != null)
        {
            MenuAIFoxCharacterControllerInput menuAIFoxCharacterControllerInput = collider2D.gameObject.GetComponent<MenuAIFoxCharacterControllerInput>();
            if (menuAIFoxCharacterControllerInput != null)
            {
                menuAIFoxCharacterControllerInput.Jump = false;
            }
        }
    }

#if UNITY_EDITOR
    [Header("Editor only")]
    public Color gizmosColor = Color.green;

    void OnDrawGizmos()
    {
        if (this.BoxCollider2D != null)
        {
            Gizmos.color = this.gizmosColor;
            Gizmos.DrawCube(this.transform.position, this.BoxCollider2D.size);
        }
    }
#endif
}
