using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
// using SocketIOClient;

using HybridWebSocket;

public class SocketScript : MonoBehaviour
{
    public Jumbotron jumbotron;
    public string server;
    public string camera_name;

    private string ws_id;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating connection to websocket server");

        // Create WebSocket instance
        WebSocket ws = WebSocketFactory.CreateInstance(this.server);

        // Add OnOpen event listener
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());

            ws.Send(Encoding.UTF8.GetBytes("/name " + this.camera_name));
            ws.Send(Encoding.UTF8.GetBytes("a message"));
        };

        // Add OnMessage event listener
        ws.OnMessage += (byte[] msg) =>
        {
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));

            // ws.Close();
        };

        // Add OnError event listener
        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };

        // Add OnClose event listener
        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());
        };

        // Connect to the server
        ws.Connect();

        // var client = new SocketIO("http://127.0.0.1:4001");
        // client.On("inLeft", response => {
        //     jumbotron.InLeft();
        // });
        // client.On("outLeft", response => {
        //     jumbotron.OutLeft();
        // });
        // client.On("inRight", response => {
        //     jumbotron.InRight();
        // });
        // client.On("outRight", response => {
        //     jumbotron.OutRight();
        // });
        // client.OnConnected += async (sender, e) => {
        //     await client.EmitAsync("unity");
        // };
        // await client.ConnectAsync();
    }
}
