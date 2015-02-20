using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour
{
    public static string GameMode = "PvP";

    public void LoadGame(string mode)
    {
        GameMode = mode;
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