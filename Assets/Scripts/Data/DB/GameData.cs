using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

public partial class PlayerData : IRealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    [MapTo("angle")]
    public float Angle { get; set; }

    [MapTo("gameTime")]
    public float GameTime { get; set; }

    [MapTo("name")]
    public string Name { get; set; }

    [MapTo("playerId")]
    public string PlayerId { get; set; }

    [MapTo("rawInput")]
    public IList<RawInputData> RawInput { get; }

    [MapTo("totalBallHit")]
    public int? TotalBallHit { get; set; }

    [MapTo("totalBombHit")]
    public int? TotalBombHit { get; set; }

    [MapTo("totalReps")]
    public int TotalReps { get; set; }

    [MapTo("totalScore")]
    public int? TotalScore { get; set; }

    [MapTo("totalSeries")]
    public int TotalSeries { get; set; }

    [MapTo("dateTime")]
    public string DateTime { get; set; }

    [MapTo("completed")]
    public bool Completed { get; set; }
}

public partial class RawInputData : IRealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }
    [MapTo("angle")]
    public float Angle { get; set; }

    [MapTo("playerId")]
    public string PlayerId { get; set; }

    [MapTo("timeStamp")]
    public float TimeStamp { get; set; }
}