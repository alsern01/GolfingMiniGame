using System;
using UnityEngine;

public class ExampleDataSender : MonoBehaviour
{
    private string _appId = "CHANGE_ME";

    private async void Start()
    {
        RealmDatabaseConnector dbConnector = RealmDatabaseConnector.Instance(_appId);
        bool isConnected = await dbConnector.ConnectAsync();
        if (isConnected)
        {
            string playerId = "PLAYER_ID";
            string gameId = "GAME_ID";
            int score = 100000;

            Action<PlayerData, int> setScoreAction = (playerData, score) =>
            {
                playerData.TotalScore = score;
            };

            dbConnector.InsertData(playerId, gameId, score, setScoreAction);
            Debug.Log("Data sent to database");
        }
        else
        {
            Debug.LogError("No database connection established.");
        }
    }
}
