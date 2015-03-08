using UnityEngine;
using System;


public class GUIUtils : MonoBehaviour
{
    private GameObject currentGUI;

    public void OpenGUI(GameObject gui)
    {
        if (currentGUI)
        {
            currentGUI.SetActive(false);
        }
        currentGUI = gui;
        if (currentGUI)
        {
            currentGUI.SetActive(true);
            SendMessages(currentGUI.transform, "OpenedAsGUI");
        }
    }

    private static void SendMessages(Transform transform, string message)
    {
        transform.gameObject.SendMessage(message, SendMessageOptions.DontRequireReceiver);
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            SendMessages(transform.GetChild(i), message);
        }
    }

    public void CloseGUI()
    {
        OpenGUI(null);
    }

    public void LoadGame(string mode)
    {
        LoadGame((GameMode)Enum.Parse(typeof(GameMode), mode));
    }

    public void LoadGame(GameMode mode)
    {
        GameLoader.GameMode = mode;
        Application.LoadLevel("main");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}