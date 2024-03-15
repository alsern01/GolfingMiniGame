using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerData
{
    private static PlayerData instance;

    public List<(float TimeStamp, float Angle)> rawInput; // (timeStamp, angle)
    public int totalBallHit;
    public int totalBombHit;
    public float gameTime;
    public float totalScore;

    private PlayerData()
    {
        rawInput = new List<(float TimeStamp, float Angle)>();
    }
    public static PlayerData Instance()
    {

        if (instance == null)
        {
            instance = new PlayerData();
        }
        return instance;
    }

    private string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public bool SaveData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "player_data.json");

        try
        {
            File.WriteAllText(filePath, ToJson());
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Fallo al escribir en {filePath} -> Exception: {e}");
            return false;
        }
    }
}
