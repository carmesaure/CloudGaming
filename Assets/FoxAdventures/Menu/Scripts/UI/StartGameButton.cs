using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    public Button StartButton
    {
        get
        {
            if (this.startButton == null)
                this.startButton = GetComponent<Button>();

            return this.startButton;
        }
    }

    void OnEnable()
    {
        // Register to events
        if (this.StartButton != null)
            this.StartButton.onClick.AddListener(this.OnStartGameClick);
    }

    void OnDisable()
    {
        // Register to events
        if (this.StartButton != null)
            this.StartButton.onClick.AddListener(this.OnStartGameClick);
    }

    private void OnStartGameClick()
    {
        SceneManager.LoadScene(1);
    }
}
