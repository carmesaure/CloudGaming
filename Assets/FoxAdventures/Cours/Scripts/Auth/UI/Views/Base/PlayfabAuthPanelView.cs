using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabAuthPanelView : MonoBehaviour
{
    private PlayfabAuthPanel playfabAuthPanel = null;
    public PlayfabAuthPanel PlayfabAuthPanel
    {
        get
        {
            if (this.playfabAuthPanel == null)
                this.playfabAuthPanel = this.GetComponentInParent<PlayfabAuthPanel>();
            return this.playfabAuthPanel;
        }
    }
}
