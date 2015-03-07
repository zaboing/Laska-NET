using UnityEngine;


public class ConnectionWaiter : MonoBehaviour
{
    private GUIUtils utils;

    public GameObject ConnectionFailure;
    public GameObject LoadingGame;

    void Start()
    {
        Debug.Log("START");
        utils = GameObject.FindObjectOfType<GUIUtils>();
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
        utils.OpenGUI(ConnectionFailure);
        Debug.Log("Failed to connect to server: " + error.ToString());
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Preparing to start...");
        utils.OpenGUI(LoadingGame);
        utils.LoadGame(GameMode.REMOTE_VS_LOCAL);
    }
}