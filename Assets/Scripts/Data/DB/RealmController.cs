using UnityEngine;
using TMPro;
using System;

public class RealmController : MonoBehaviour
{
    private static RealmController _instance;
    public static RealmController Instance { get { return _instance; } }

    private RealmDatabaseConnector _dbConnector;
    [SerializeField] private TMP_InputField _playerIdField;

    public string PlayerId { private set; get; }
    private string gameId = "hombros - minigolf";

    private bool isConnected;

    private async void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;

            // Obtener la instancia del RealmDatabaseConnector
            _dbConnector = RealmDatabaseConnector.Instance("tfg_app-gcscafj");

            // Intentar conectar a la base de datos
            isConnected = await _dbConnector.ConnectAsync();
            if (!isConnected)
            {
                Debug.LogError("No se pudo establecer la conexión con la base de datos.");
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _playerIdField = FindAnyObjectByType<TMP_InputField>();
    }

    private void OnDisable()
    {
        _dbConnector.Disconnect();
    }

    public bool UserLogin()
    {
        if (_playerIdField == null)
        {
            Debug.LogWarning("Campo de texto de playerId no está asignado.");
            _playerIdField = FindAnyObjectByType<TMP_InputField>();
        }

        PlayerId = _playerIdField.text;

        PlayerData data = GetPlayerData(PlayerId);

        if (data != null)
        {
            Debug.Log("¡Inicio de sesión exitoso!");
            ClearRawInput(PlayerId);
            return true;
        }
        else
        {
            Debug.Log($"Inicio de sesión fallido. Nombre de usuario no encontrado. {_playerIdField.text}");
            return false;
        }
    }

    private PlayerData GetPlayerData(string id)
    {
        return isConnected ? _dbConnector.GetPlayerData(id, gameId) : null;
    }

    public float GetAngleForPlayer(string id)
    {
        return isConnected ? _dbConnector.GetAngleForPlayer(id, gameId) : 0.0f;
    }

    public int GetSeriesForPlayer(string id)
    {
        return isConnected ? _dbConnector.GetSeriesForPlayer(id, gameId) : 1;
    }

    public int GetRepsForPlayer(string id)
    {
        return isConnected ? _dbConnector.GetRepsForPlayer(id, gameId) : 1;
    }

    private void ClearRawInput(string id)
    {
        if (isConnected)
        {
            _dbConnector.ClearRawInput(id, gameId);
        }
    }

    public void AddRawInput(string id, RawInputData rawInput)
    {
        if (isConnected)
        {
            Action<PlayerData, RawInputData> setRawInput = (playerData, hit) =>
            {
                playerData.RawInput.Add(rawInput);
            };

            _dbConnector.InsertData(id, gameId, rawInput, setRawInput);
        }
    }

    public void SetBallHit(string id, int ballHit)
    {
        if (isConnected)
        {
            _dbConnector.InsertData(id, gameId, ballHit, (playerData, hit) => playerData.TotalBallHit = hit);
        }
    }

    public void SetBombHit(string id, int bombHit)
    {
        if (isConnected)
        {
            _dbConnector.InsertData(id, gameId, bombHit, (playerData, hit) => playerData.TotalBombHit = hit);
        }
    }

    public void SetScore(string id, int score)
    {
        if (isConnected)
        {
            _dbConnector.InsertData(id, gameId, score, (playerData, scr) => playerData.TotalScore = scr);
        }
    }

    public void SetGameTime(string id, float gameTime)
    {
        if (isConnected)
        {
            _dbConnector.InsertData(id, gameId, gameTime, (playerData, time) => playerData.GameTime = time);
        }
    }

    public void SetDateTime(string id, string dateTime)
    {
        if (isConnected)
        {
            _dbConnector.InsertData(id, gameId, dateTime, (playerData, date) => playerData.DateTime = date);
        }
    }

    public void SetGameCompleted(string id, bool completed)
    {
        if (isConnected)
        {
            _dbConnector.InsertData(id, gameId, completed, (playerData, completed) => playerData.Completed = completed);
        }
    }
}

