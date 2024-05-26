using Riptide;
using Riptide.Utils;
using UnityEngine;
using System.Net;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public enum MessageID
{
    orientation = 1,
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance
    {

        get { return instance; }
        private set
        {
            if (instance == null)
            {
                instance = value;
            }
            else if (instance != value)
            {
                Destroy(value);
            }
        }
    }

    public Server Server { get; private set; }

    public string ipAddress { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    private void Awake()
    {
        Instance = this;

        ipAddress = GetLocalIPAddressV2();
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();



#if UNITY_EDITOR
        GameManager.Instance.clientConnected = true;
        GameManager.Instance.gameStartTime = Time.time;
        UIManager.Instance.StartCountdown(2f);
#else
                Server.ClientConnected += OnClientConnected;
                Server.ClientDisconnected += OnClientDisconnected;
#endif

        //Server.ClientConnected += OnClientConnected;
        //Server.ClientDisconnected += OnClientDisconnected;

        Server.Start(port, maxClientCount);

        UIManager.Instance.SetIPText();
    }

    private void FixedUpdate()
    {
        Server.Update();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    [MessageHandler((ushort)MessageID.orientation)]
    private static void ReceiveMesageFromDevice(ushort fromClientId, Message message)
    {
        Vector3 orientation = message.GetVector3();
        InputManager.Instance.SetAccelVector(orientation);
    }

    private string GetLocalIPAddress()
    {
        string ipAddress = "";
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ipAddress = ip.ToString();
                break;
            }
        }

        return ipAddress;
    }

    private string GetLocalIPAddressV2()
    {
        string ipAddress = "";

        try
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip.Address))
                        {
                            ipAddress = ip.Address.ToString();
                            return ipAddress;
                        }
                    }
                }
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError("SocketException caught! Unable to get local IP address: " + ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception caught! Unable to get local IP address: " + ex.Message);
        }

        if (string.IsNullOrEmpty(ipAddress))
        {
            Debug.LogError("No IPv4 address found for the local machine.");
        }

        return ipAddress;
    }

    private void OnClientConnected(object sender, EventArgs e)
    {
        GameManager.Instance.clientConnected = true;
        GameManager.Instance.gameStartTime = Time.time;
        UIManager.Instance.StartCountdown(10.0f);
    }

    private void OnClientDisconnected(object sender, EventArgs e)
    {
        UIManager.Instance.EnableConnectionPanel();
        UIManager.Instance.StopCountdown();
        GameManager.Instance.StopGame();
    }
}
