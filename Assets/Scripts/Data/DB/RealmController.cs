using Realms;
using Realms.Sync;
using UnityEngine;
using System.Linq;
using System;
using MongoDB.Bson;
using System.Threading.Tasks;

public class RealmController : MonoBehaviour
{
    private static RealmController _instance;
    public static RealmController Instance { get { return _instance; } }

    private Realm _realm;
    private App _realmApp;
    private string _realmAppId = "tfg_app-gcscafj";

    [SerializeField] private TMPro.TMP_InputField _playerIdField;

    public string PlayerId { private set; get; }

    private async void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
            _realmApp = App.Create(_realmAppId);

            try
            {
                await _realmApp.LogInAsync(Credentials.Anonymous());
                _realm = await Realm.GetInstanceAsync(new FlexibleSyncConfiguration(_realmApp.CurrentUser));

                _realm.Subscriptions.Update(() =>
                {
                    var playerData = _realm.All<PlayerData>();
                    _realm.Subscriptions.Add(playerData);

                    var rawInputData = _realm.All<RawInputData>();
                    _realm.Subscriptions.Add(rawInputData);
                });
                await _realm.Subscriptions.WaitForSynchronizationAsync();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _playerIdField = FindAnyObjectByType<TMPro.TMP_InputField>();
    }

    private void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }

    public bool UserLogin()
    {
        if (_playerIdField == null)
        {
            Debug.LogWarning("Campo de texto de playerId no está asignado.");
            _playerIdField = FindAnyObjectByType<TMPro.TMP_InputField>();
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

        var dataModel = _realm.All<PlayerData>().ToList();
        PlayerData playerData = _realm.All<PlayerData>().FirstOrDefault(data => data.PlayerId == id);

        return playerData;
    }

    public float GetAngleForPlayer(string id)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            return dataModel.Angle;
        }
        return 0.0f;
    }

    public int GetSeriesForPlayer(string id)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {

            return dataModel.TotalSeries;
        }
        return 1;
    }

    public int GetRepsForPlayer(string id)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {

            return dataModel.TotalReps;
        }
        return 1;
    }

    private void ClearRawInput(string id)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.RawInput.Clear();
            });
        }
    }

    public void AddRawInput(string id, RawInputData rawInput)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.RawInput.Add(rawInput);
            });
        }
    }

    public void SetBallHit(string id, int ballHit)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.TotalBallHit = ballHit;
            });
        }
    }

    public void SetBombHit(string id, int bombHit)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.TotalBombHit = bombHit;
            });
        }
    }

    public void SetScore(string id, int score)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.TotalScore = score;
            });
        }
    }

    public void SetGameTime(string id, float gameTime)
    {
        PlayerData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.GameTime = gameTime;
            });
        }
    }
}
