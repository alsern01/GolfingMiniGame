using System.IO;
using System;
using UnityEngine;

[Serializable]
public class ConfigData
{
    public float angle;
    public int totalSeries;
    public int totalReps;

    private static ConfigData instance;

    private ConfigData()
    {
        ReadData();
    }

    public static ConfigData Instance()
    {
        if (instance == null)
        {
            instance = new ConfigData();
        }

        return instance;
    }

    private void ReadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "config_data.json");

        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Debug.Log(json);

                // Deserializar JSON de config
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
            {
                Debug.LogWarning("Config file not found. Using default values.");
                angle = 0f;
                totalSeries = 1;
                totalReps = 1;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {filePath}. Exception: {e}");
        }
    }
}