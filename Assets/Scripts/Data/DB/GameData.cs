using System.Collections.Generic;
using Realms;
using System;
using MongoDB.Bson;

public class GameData : RealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    [MapTo("playerId")]
    [Required]
    public string PlayerId { get; set; }

    #region PLAYER SCORE
    [MapTo("rawInputList")]
    public IList<RawInputData> RawInputList { get; }

    [MapTo("totalBallHit")]
    public int TotalBallHit { get; set; }

    [MapTo("totalBombHit")]
    public int TotalBombHit { get; set; }

    [MapTo("gameTime")]
    public float GameTime { get; set; }

    [MapTo("totalScore")]
    public int TotalScore { get; set; }

    //public GameResultsData GameResults { get; set; }
    #endregion

    #region CONFIG DATA
    [MapTo("angle")]
    public float Angle { get; set; }
    [MapTo("totalSeries")]
    public int TotalSeries { get; set; }
    [MapTo("totalReps")]
    public int TotalReps { get; set; }
    #endregion

    public GameData()
    {
        PlayerId = "Default";
        RawInputList = new List<RawInputData>();
    }
}
