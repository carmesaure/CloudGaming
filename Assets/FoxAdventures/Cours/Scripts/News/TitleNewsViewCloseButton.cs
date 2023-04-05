using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleNewsViewCloseButton : MonoBehaviour
{
    [SerializeField] private TitleNewsView titleNewsView = null;
    public TitleNewsView TitleNewsView
    {
        get
        {
            if (this.titleNewsView == null)
                this.titleNewsView = GetComponentInParent<TitleNewsView>();
            return this.titleNewsView;
        }
    }

    [SerializeField] private Button closeButton = null;
    public Button CloseButton
    {
        get
        {
            if (this.closeButton == null)
                this.closeButton = GetComponent<Button>();

            return this.closeButton;
        }
    }

    void OnEnable()
    {
        // Register to events
        if (this.CloseButton != null)
            this.CloseButton.onClick.AddListener(this.OnCloseNewsClick);
    }

    void OnDisable()
    {
        // Register to events
        if (this.CloseButton != null)
            this.CloseButton.onClick.RemoveListener(this.OnCloseNewsClick);
    }

    private void OnCloseNewsClick()
    {
        if (this.TitleNewsView != null)
            this.TitleNewsView.HideView();
    }
}
