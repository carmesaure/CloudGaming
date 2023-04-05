using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    // Content root
    public Transform contentRoot = null;

    void OnEnable()
    {
        // Hide by default
        this.HideView();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            this.HideView();
        }
    }


    // Show / Hide content root
    public void ShowView()
    {
        if (this.contentRoot != null)
            this.contentRoot.gameObject.SetActive(true);
    }

    public void HideView()
    {
        if (this.contentRoot != null)
            this.contentRoot.gameObject.SetActive(false);
    }
}
