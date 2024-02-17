using Riptide;
using Riptide.Utils;
using UnityEngine;
using System.Net;
using Riptide.Transports;
using System;

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

    public static Vector3 orientation { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    private void Awake()
    {
        Instance = this;

        ipAddress = GetLocalIPAddress();
        Debug.Log("Device local IP address is: " + ipAddress);
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.ClientConnected += OnClientConnected;
        Server.ClientDisconnected += OnClientDisconnected;

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
        orientation = message.GetVector3();
        InputManager.Instance.SetAccelVector(orientation);
    }

    public Vector3 getDeviceOrientation()
    {
        return orientation;
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

    private void OnClientConnected(object sender, EventArgs e)
    {
        Debug.Log("Client connected");
        GameManager.Instance.ClientConnected();
        UIManager.Instance.StartCountdown();
    }

    private void OnClientDisconnected(object sender, EventArgs e)
    {
        Debug.Log("Client disconnected");
        UIManager.Instance.EnableConnectionPanel();
        UIManager.Instance.StopCountdown();
        GameManager.Instance.StopGame();
    }
}
