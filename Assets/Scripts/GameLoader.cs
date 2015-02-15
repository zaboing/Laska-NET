using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour
{
    public static string GameMode;

    public void LoadGame(string mode)
    {
        GameMode = mode;
        Application.LoadLevel("main");
    }
}