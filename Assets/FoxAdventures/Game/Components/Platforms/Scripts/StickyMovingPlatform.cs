using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyMovingPlatform : MovingPlatform
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FoxPlayer foxPlayer = collision.gameObject.transform.GetComponentInParent<FoxPlayer>();
            if (foxPlayer != null)
            {
                foxPlayer.gameObject.transform.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FoxPlayer foxPlayer = collision.gameObject.transform.GetComponentInParent<FoxPlayer>();
            if (foxPlayer != null)
            {
                foxPlayer.gameObject.transform.SetParent(null);
            }
        }
    }
}
