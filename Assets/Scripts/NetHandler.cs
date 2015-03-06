using UnityEngine;
using System.Collections;

using Laska;

public class NetHandler : MonoBehaviour
{
	
	public const string GameType = "Lambus";
	
	private GameField cachedField;
	
	private string requested;
	
	public GameObject Connecting;
	public GameObject Disconnected;
	public GameObject Waiting;
	
	private GameObject openGui;
	
	public void Start() 
	{
		cachedField = GetComponent<GameField>();
	}
	
	public void InitializeServer() 
	{
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(32, 25003, useNat);
		OpenGUI(Waiting);
	}
	
	public void InitializeClient()
	{
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(GameType);
		cachedField.LockInput = true;
		OpenGUI(Connecting);
	}

	private void OnServerInitialized() 
	{
		Debug.Log("Server initialized");
		MasterServer.RegisterHost(GameType, "Boingy game");
	}
	
	private void OnPlayerConnected(NetworkPlayer player) 
	{
		Debug.Log("Player connected");
		OpenGUI(null);
	}
	
	private void OnPlayerDisconnected(NetworkPlayer player)
	{
		Debug.Log("Player disconnected");
		Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
		OpenGUI(Disconnected);
	}
	
	private void OnFailedToConnectToMasterServer(NetworkConnectionError error) 
	{
		Debug.Log("Failed to connect to master server: " + error);
		OpenGUI(Disconnected);
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
			case MasterServerEvent.HostListReceived:
				Debug.Log("Received host list");
				HostData[] hostData = MasterServer.PollHostList();
				foreach (HostData host in hostData)
				{
					Network.Connect(host);
					break;
				}
				break;
		}
	}
	
	public void OpenGUI(GameObject gui) 
	{
		if (openGui) 
		{
			openGui.SetActive(false);
		}
		openGui = gui;
		if (openGui) {
			openGui.SetActive(true);	
		}
	}
	
	private void OnConnectedToServer()
	{
		Debug.Log("Connected to server.");
		OpenGUI(null);
	}
	
	private void OnFailedToConnect(NetworkConnectionError error)
	{
		Debug.Log("Failed to connect to server: " + error);
		OpenGUI(Disconnected);
	}
	
	private void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log("Disconnected from server: " + info);
		OpenGUI(Disconnected);
	}
	
	public void RequestAcknowledge(Move move)
	{
		cachedField.LockInput = true;
		requested = move.ToString();
		networkView.RPC("RemoteMove", RPCMode.Others, requested);
	}
	
	[RPC]
	public void RemoteMove(string moveString) 
	{
		Move move = new Move(moveString);
		if (cachedField.IsValid(move))
		{
			cachedField.DoMove(move);
			networkView.RPC("AcknowledgeMove", RPCMode.Others, moveString);
			cachedField.LockInput = false;
		}
	}
	
	[RPC]
	public void AcknowledgeMove(string move) 
	{
		if (move == requested) 
		{
			cachedField.DoMove(new Move(move));
		}
	}
}
