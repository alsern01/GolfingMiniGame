using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

[Serializable]
public class PlayerData
{
    private static PlayerData instance;

    [Serializable]
    public struct RawInputData
    {
        public float TimeStamp;
        public float Angle;
    }

    [SerializeField] private List<RawInputData> rawInput;
    public int totalBallHit;
    public int totalBombHit;
    public float gameTime;
    public int totalScore;

    private PlayerData()
    {
        rawInput = new List<RawInputData>();
        totalBallHit = 0;
        totalBombHit = 0;
        totalScore = 0;
        gameTime = 0;
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
        string json = JsonUtility.ToJson(this);
        // Devuelve un json identado para que sea mas visual
        return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented);
    }

    public bool SaveData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "player_data.json");

        try
        {
            string jsonText = ToJson();
            Debug.Log($"Datos guardados: {jsonText}");
            File.WriteAllText(filePath, jsonText);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Fallo al escribir en {filePath} -> Exception: {e}");
            return false;
        }
    }

    public void AddRawInput(float timeStamp, float angle)
    {
        rawInput.Add(new RawInputData { TimeStamp = timeStamp, Angle = angle });
        Debug.Log($"Array Size: {rawInput.Count}, Ultimo -> {rawInput.LastOrDefault().TimeStamp}, {rawInput.LastOrDefault().Angle}");
    }
}
