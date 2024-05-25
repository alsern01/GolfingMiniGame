using Realms;
using Realms.Sync;
using System;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Singleton class for handling connection and data manipulation in a Realm database.
/// </summary>
public class RealmDatabaseConnector
{
    private static RealmDatabaseConnector _instance; // Singleton

    private Realm _realm;
    private App _realmApp;
    private string _realmAppId;
    private bool _isConnected = false;

    // Private constructor, Singleton pattern
    private RealmDatabaseConnector(string realmAppId)
    {
        _realmAppId = realmAppId;
    }

    /// <summary>
    /// Gets the singleton instance of RealmDatabaseConnector.
    /// <param name="realmAppId">string: The Realm app identifier.</param>
    /// </summary>
    public static RealmDatabaseConnector Instance(string realmAppId)
    {
        if (_instance == null)
        {
            _instance = new RealmDatabaseConnector(realmAppId);
        }
        return _instance;
    }

    /// <summary>
    /// Establishes an asynchronous connection to the Realm database.
    /// </summary>
    /// <returns>Task<bool>: true if the connection is successfully established, false otherwise.</returns>
    public async Task<bool> ConnectAsync()
    {
        if (!_isConnected)
        {
            _realmApp = App.Create(_realmAppId);

            try
            {
                var user = await _realmApp.LogInAsync(Credentials.Anonymous());
                _realm = await Realm.GetInstanceAsync(new FlexibleSyncConfiguration(user));

                _realm.Subscriptions.Update(() =>
                {
                    var playerData = _realm.All<PlayerData>();
                    _realm.Subscriptions.Add(playerData);

                    var rawInputData = _realm.All<RawInputData>();
                    _realm.Subscriptions.Add(rawInputData);
                });
                await _realm.Subscriptions.WaitForSynchronizationAsync();

                _isConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Disconnects from the Realm database.
    /// </summary>
    public void Disconnect()
    {
        if (_isConnected)
        {
            _realm.Dispose();
            _isConnected = false;
            Console.WriteLine("Desconectado de la base de datos de Realm.");
        }
        else
        {
            Console.WriteLine("No hay conexión establecida para desconectar.");
        }
    }

    /// <summary>
    /// Retrieves player data with the provided identifier.
    /// </summary>
    /// <param name="id">string: The player identifier.</param>
    /// <param name="gameId">string: The game identifier.</param>
    /// <returns>PlayerData: The player's data.</returns>
    public PlayerData GetPlayerData(string id, string gameId)
    {
        return _realm.All<PlayerData>().FirstOrDefault(
            data => data.PlayerId == id && data.Name == gameId);
    }

    /// <summary>
    /// Retrieves the angle for the player with the provided identifier.
    /// </summary>
    /// <param name="id">string: The player identifier.</param>
    /// <param name="gameId">string: The game identifier.</param>
    /// <returns>float: The player's angle.</returns>
    public float GetAngleForPlayer(string id, string gameId)
    {
        PlayerData dataModel = GetPlayerData(id, gameId);
        return dataModel != null ? dataModel.Angle : 0.0f;
    }

    /// <summary>
    /// Retrieves the series count for the player with the provided identifier.
    /// </summary>
    /// <param name="id">string: The player identifier.</param>
    /// <param name="gameId">string: The game identifier.</param>
    /// <returns>int: The player's series count.</returns>
    public int GetSeriesForPlayer(string id, string gameId)
    {
        PlayerData dataModel = GetPlayerData(id, gameId);
        return dataModel != null ? dataModel.TotalSeries : 1;
    }

    /// <summary>
    /// Retrieves the repetitions count for the player with the provided identifier.
    /// </summary>
    /// <param name="id">string: The player identifier.</param>
    /// <param name="gameId">string: The game identifier.</param>
    /// <returns>int: The player's repetitions count.</returns>
    public int GetRepsForPlayer(string id, string gameId)
    {
        PlayerData dataModel = GetPlayerData(id, gameId);
        return dataModel != null ? dataModel.TotalReps : 1;
    }


    /// <summary>
    /// Clears the raw input data for the player with the provided identifier.
    /// </summary>
    /// <param name="gameId">string: The game identifier.</param>
    /// <param name="id">string: The player identifier.</param>
    public void ClearRawInput(string id, string gameId)
    {
        PlayerData dataModel = GetPlayerData(id, gameId);
        if (dataModel != null)
        {
            _realm.Write(() =>
            {
                dataModel.RawInput.Clear();
            });
        }
    }

    /// <summary>
    /// Inserts or updates generic data into the PlayerData model.
    /// </summary>
    /// <typeparam name="T">The type of data to insert.</typeparam>
    /// <param name="playerId">string: The player identifier.</param>
    /// <param name="gameId">string: The game identifier.</param>
    /// <param name="obj">T: The value to insert.</param>
    /// <param name="insertionAction">Action<PlayerData, T>: The action defining how to update the model.</param>
    public void InsertData<T>(string playerId, string gameId, T obj, Action<PlayerData, T> insertionAction)
    {
        if (_isConnected)
        {
            PlayerData playerData = GetPlayerData(playerId, gameId);
            if (playerData != null)
            {
                _realm.Write(() =>
                {
                    insertionAction(playerData, obj);
                });
            }
            else
            {
                Console.WriteLine("No se encontró ningún jugador con el ID proporcionado.");
            }
        }
        else
        {
            Console.WriteLine("No se puede realizar la operación. No hay conexión establecida.");
        }
    }
}
