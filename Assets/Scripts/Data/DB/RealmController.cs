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
    private User _realmUser;

    private string _realmAppId = "tfg_app-gcscafj";
    [SerializeField] private TMPro.TMP_InputField _playerIdField;
    public string PlayerId { private set; get; }

    private async void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
            _realmApp = App.Create(new AppConfiguration(_realmAppId));
            if (_realmApp.CurrentUser == null)
            {
                _realmUser = await _realmApp.LogInAsync(Credentials.Anonymous());
                _realm = await Realm.GetInstanceAsync(new FlexibleSyncConfiguration(_realmUser));
            }
            else
            {
                _realmUser = _realmApp.CurrentUser;
                _realm = Realm.GetInstance(new FlexibleSyncConfiguration(_realmUser));
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
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
            Debug.LogError("Campo de texto de playerId no está asignado.");
            return false;
        }

        PlayerId = _playerIdField.text;

        GameData data = GetPlayerData(PlayerId);

        if (data != null)
        {
            Debug.Log("¡Inicio de sesión exitoso!");
            return true;
        }
        else
        {
            Debug.Log("Inicio de sesión fallido. Nombre de usuario no encontrado.");
            return false;
        }
    }


    private GameData GetPlayerData(string id)
    {
        GameData dataModel = _realm.All<GameData>().Where(data => data.PlayerId == id).FirstOrDefault();
        return dataModel;
    }

    public float GetAngleForPlayer(string id)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            return dataModel.Angle;
        }
        return 0.0f;
    }

    public int GetSeriesForPlayer(string id)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {

            return dataModel.TotalSeries;
        }
        return 1;
    }

    public int GetRepsForPlayer(string id)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {

            return dataModel.TotalReps;
        }
        return 1;
    }

    public void AddRawInput(string id, RawInputData rawInput)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.RawInputList.Add(rawInput);
            });
        }
    }

    public void IncreaseBallHit(string id)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.TotalBallHit += 1;
                //dataModel.GameResults.TotalBallHit += 1;
            });
        }
    }

    public void IncreaseBombHit(string id)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.TotalBombHit += 1;
                //dataModel.GameResults.TotalBombHit += 1;
            });
        }
    }

    public void SetScore(string id, int score)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.TotalScore = score;
                //dataModel.GameResults.TotalScore = score;
            });
        }
    }

    public void SetGameTime(string id, float gameTime)
    {
        GameData dataModel = GetPlayerData(id);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.GameTime = gameTime;
                //dataModel.GameResults.GameTime = gameTime;
            });
        }
    }
}
