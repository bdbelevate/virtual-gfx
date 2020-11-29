using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using SocketIOClient;

public class SocketScript : MonoBehaviour
{
    public Jumbotron jumbotron;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connection made to socketIO");
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
