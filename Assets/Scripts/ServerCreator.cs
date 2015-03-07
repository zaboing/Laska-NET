using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using Laska;

public class ServerCreator : MonoBehaviour
{
    public InputField GameName;
    public InputField Port;
    public Toggle WhiteToggle;

    private GUIUtils utils;

    public GameObject WaitingForPlayer;

    void Start()
    {
        utils = GameObject.FindObjectOfType<GUIUtils>();
    }

    public void StartServer()
    {
        bool useNat = !Network.HavePublicAddress();
        Network.InitializeServer(2, int.Parse(Port.text), useNat);
    }


    private void OnServerInitialized()
    {
        Debug.Log("Server initialized");
        var info = new { color = GetDeselectedColor() };
        NetHandler.ClientColor = info.color;
        MasterServer.RegisterHost(NetHandler.GameType, GameName.text, JsonConvert.SerializeObject(info));
        utils.OpenGUI(WaitingForPlayer);
    }

    private Colour GetDeselectedColor()
    {
        return WhiteToggle.isOn ? Colour.Black : Colour.White;
    }
}