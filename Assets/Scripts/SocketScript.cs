using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Reflection;

using UnityWSControl;
using HybridWebSocket;
using FormUrlEncoded;

public class SocketScript : MonoBehaviour
{
    public Jumbotron jumbotron;
    public TextField text_object;

    public string server;
    public string camera_name;
    public WsData data;

    // values for control
    private string ws_id;
    public String text { get; set; }
    public String jt_pos { get; set; }

    private static String[] properties = { "text", "jt_pos" };

    // Start is called before the first frame update
    void Start()
    {
        this.Connect();
    }

    private void Connect()
    {
        Debug.Log("Creating connection to websocket server");

        // Create WebSocket instance
        WebSocket ws = WebSocketFactory.CreateInstance(this.server);

        // Add OnOpen event listener
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());

            ws.Send(Encoding.UTF8.GetBytes("/join " + this.camera_name));
        };

        // Add OnMessage event listener
        ws.OnMessage += (byte[] msg) =>
        {
            string string_message = Encoding.UTF8.GetString(msg);
            Debug.Log("WS received message: " + string_message);

            WsData data = Serializer.Deserialize<WsData>(string_message);

            if (data.command == "/join" && data.room == this.camera_name)
            {
                this.ws_id = data.sender_id;
                // get all values
                ws.Send(Encoding.UTF8.GetBytes("/values"));
            }
            else if (data.command == "/values")
            {

            }
            else if (data.command == "/get" || data.command == "/set")
            {
                var changed = this.SetValuesFromData(data);
                if (changed.Contains("jt_pos")) this.SetJumbotron();
                if (changed.Contains("text")) this.SetText();
            }
            else if (data.command == "/message")
            {
                Debug.Log("message received:" + data.message);
            }
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
            this.Connect();
        };

        // Connect to the server
        ws.Connect();
    }

    private void SetJumbotron()
    {
        Debug.Log("Changing jumbotron: " + this.jt_pos);
        if (this.jt_pos == "inLeft")
        {
            jumbotron.InLeft();
        }
        else if (this.jt_pos == "outLeft")
        {
            jumbotron.OutLeft();
        }
        else if (this.jt_pos == "inRight")
        {
            jumbotron.InRight();
        }
        else if (this.jt_pos == "outRight")
        {
            jumbotron.OutRight();
        }
    }

    private void SetText()
    {
        this.text_object.SetText(this.text);
    }

    private List<string> SetValuesFromData(WsData data)
    {
        var changed = new List<string>();

        foreach (string property in SocketScript.properties)
        {
            var dataProp = data.GetProperty(property);
            var selfProp = this.GetProperty(property);
            if (!selfProp.CanRead || !dataProp.CanRead) continue;
            String dataValue = (String)dataProp.GetValue(data);
            String selfValue = (String)selfProp.GetValue(this);
            if (String.IsNullOrEmpty(dataValue)) continue;
            if (dataValue != selfValue)
            {
                changed.Add(property);
                Debug.Log("changed " + property + " to " + dataProp.GetValue(data) + " from " + selfProp.GetValue(this));
                this.SetProperty(property, dataProp.GetValue(data));
            }
        }

        return changed;
    }
}
