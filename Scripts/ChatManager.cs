using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NativeWebSocket;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI chatDisplay;
    public bool useWebSocket = false;

    // UDP
    UdpClient udpClient;
    Thread receiveThread;

    // WebSocket
    WebSocket websocket;

    void Start()
    {
        // UDP Setup
        udpClient = new UdpClient();
        udpClient.Connect("127.0.0.1", 5000);
        receiveThread = new Thread(ReceiveUDP);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // WebSocket Setup
        SetupWebSocket();
    }

    async void SetupWebSocket()
    {
        websocket = new WebSocket("ws://localhost:3002");

        websocket.OnOpen += () => {
            Debug.Log(" Conectado al servidor WebSocket");
            AppendChat(" Conectado al servidor WebSocket");
        };

        websocket.OnMessage += (bytes) => {
            var message = Encoding.UTF8.GetString(bytes);
            AppendChat("WS: " + message);
        };

        websocket.OnError += (e) => {
            Debug.Log(" WebSocket Error: " + e);
            AppendChat(" Error WebSocket: " + e);
        };

        websocket.OnClose += (e) => {
            Debug.Log(" Conexión WebSocket cerrada");
            AppendChat(" Conexión WebSocket cerrada");
        };

        await websocket.Connect();
    }

    public void ToggleProtocol(bool isWS)
    {
        useWebSocket = isWS;
        AppendChat(isWS ? " Modo WebSocket Activado" : " Modo UDP Activado");
    }

    public async void SendMessageUnified()
    {
        string message = inputField.text;

        if (useWebSocket && websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
            AppendChat("Yo (WS): " + message);
        }
        else
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length);
            AppendChat("Yo (UDP): " + message);
        }

        inputField.text = "";
    }

    void ReceiveUDP()
    {
        UdpClient listener = new UdpClient(5001);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 5001);

        try
        {
            while (true)
            {
                byte[] data = listener.Receive(ref groupEP);
                string received = Encoding.UTF8.GetString(data);
                AppendChat("UDP: " + received);
            }
        }
        catch (SocketException e)
        {
            Debug.Log("UDP Socket closed: " + e);
        }
    }

    void AppendChat(string message)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            chatDisplay.text += message + "\n";
        });
    }

    async void OnApplicationQuit()
    {
        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();

        udpClient?.Close();

        if (websocket != null && websocket.State == WebSocketState.Open)
            await websocket.Close();
    }
}
