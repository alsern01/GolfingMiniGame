using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Realms;
using System.Collections;
using System.Collections.Generic;

public class RawInputData : RealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    [MapTo("timeStamp")]
    [Required]
    public float TimeStamp { get; set; }

    [MapTo("angle")]
    [Required]
    public float Angle { get; set; }
}

