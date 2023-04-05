using System.Collections.Generic;
using UnityEngine;

public class FoxCharacterWinScreenLeaderboard : FoxCharacterWinScreenAddCrystals
{
    protected override void OnWin()
    {
        // Use player stats to register virtual currency increase
        FoxCharacterInventory foxCharacterInventory = this.FoxPlayer.GetComponentInChildren<FoxCharacterInventory>();
        if (foxCharacterInventory != null)
        {
            // Data that we want to keep for leaderboards
            int crystalsCount = foxCharacterInventory.jewelsCount;
            float levelDuration = Time.timeSinceLevelLoad;

            // TODO: Update our best score on level 1
            // >> Desired name of entry: level1_crystals
            // >> Desired value of entry: crystalsCount

            // TODO: Update our best score on level 1
            // >> Desired name of entry: level1_speedrun
            // >> Desired value of entry: (Mathf.FloorToInt(levelDuration * 100.0f * -1.0f))
            //// Note: We multiplied the time by -1 because leaderboards in Playfab are always ranked as "more is best"
        }

        // Call base function from the class "FoxCharacterWinScreenLeaderboard" that adds crystals we collected to our inventory
        base.OnWin();
    }

    private void OnUpdatePlayerStatisticsRequestSuccess()
    {
        // Log
        Debug.Log("FoxCharacterWinScreenLeaderboard.FoxCharacterWinScreenLeaderboard() - Error: TODO");
    }

    private void OnUpdatePlayerStatisticsRequestError()
    {
        // Log
        Debug.LogError("FoxCharacterWinScreenLeaderboard.OnUpdatePlayerStatisticsRequestError() - Error: TODO");
    }
}
