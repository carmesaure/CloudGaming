using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text usernameText = null;
    public Text valueText = null;

    public void SetValue(string username, string value)
    {
        if (this.usernameText != null)
            this.usernameText.text = username;
        if (this.valueText != null)
            this.valueText.text = value;
    }
}
