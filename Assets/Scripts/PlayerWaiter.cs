using UnityEngine;
using System.Collections;


public class PlayerWaiter : MonoBehaviour
{

    private GUIUtils utils;

    void Start()
    {
        utils = GameObject.FindObjectOfType<GUIUtils>();
    }

    private void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected");
        utils.LoadGame(GameMode.LOCAL_VS_REMOTE);
    }

    private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
        Debug.Log("Failed to connect to master server: " + error);
    }

    private void OnMasterServerEvent(MasterServerEvent ev)
    {
        switch (ev)
        {
            case MasterServerEvent.RegistrationSucceeded:
                Debug.Log("Successfully registered server");
                break;
            case MasterServerEvent.RegistrationFailedNoServer:
                Debug.Log("Could not register: no server");
                break;
            case MasterServerEvent.RegistrationFailedGameType:
                Debug.Log("Could not register: invalid gametype");
                break;
            case MasterServerEvent.RegistrationFailedGameName:
                Debug.Log("Could not register: invalid game name");
                break;
        }
    }

    public void Cancel()
    {
        MasterServer.UnregisterHost();
        Network.Disconnect();
    }
}