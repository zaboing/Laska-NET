using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using Laska;

public class ServerListEntry : MonoBehaviour
{
    public Text Label;
    public Button Button;

    private HostData host;

    public HostData Host
    {
        get
        {
            return host;
        }
        set
        {
            host = value;
            Label.text = host.gameName + " (" + host.connectedPlayers + " / " + host.playerLimit + ")";
            var info = new { color = Colour.White };
            info = JsonConvert.DeserializeAnonymousType(host.comment, info);
            Label.text += ". You'll be " + info.color.ToString() + ".";
        }
    }

    private ServerList serverList;

    void Start()
    {
        serverList = GetComponentInParent<ServerList>();
    }

    public void Connect()
    {
        serverList.Connect(host);
    }

    
}