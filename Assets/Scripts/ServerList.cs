using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Laska;
using Newtonsoft.Json;

public class ServerList : MonoBehaviour
{
    public GameObject Entry;

    public GameObject Connecting;

    private GUIUtils utils;

    void Start()
    {
        utils = GameObject.FindObjectOfType<GUIUtils>();
    }
	
	// To be called by GUIUtils
    public void OpenedAsGUI()
	{
		StartCoroutine(RefreshLoop());
	}

    public void LoadServers()
    {
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(NetHandler.GameType);
    }

    public void OnMasterServerEvent(MasterServerEvent ev)
    {
        switch (ev)
        {
            case MasterServerEvent.HostListReceived:
                GenerateEntries();
                break;
        }
    }

    private void GenerateEntries()
    {
        ClearChildren();
        HostData[] hosts = MasterServer.PollHostList();
        Vector2 offset = new Vector2(0, -7);
        foreach (HostData host in hosts)
        {
            GameObject entryObject = Instantiate(Entry) as GameObject;
            entryObject.transform.SetParent(transform, false);
            var entry = entryObject.GetComponent<ServerListEntry>();
            if (entry)
            {
                entry.Host = host;
            }
            entryObject.GetComponent<RectTransform>().Translate(offset);
            offset.y -= 30;
        }
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, -offset.y);
    }

    private void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Connect(HostData host)
    {
        utils.OpenGUI(Connecting);
        Debug.Log("Connecting to: " + string.Join(" / ", host.ip) + " at port " + host.port);
        Network.Connect(host);
        var info = new { color = Colour.White };
        info = JsonConvert.DeserializeAnonymousType(host.comment, info);
        NetHandler.ClientColor = info.color;
    }
	
	private IEnumerator RefreshLoop()
	{
		while (true) {
			yield return new WaitForSeconds(2);
			LoadServers();
		}
	}
}
