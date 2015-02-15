﻿using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour
{
    public static string GameMode = "PvP";

    public void LoadGame(string mode)
    {
        GameMode = mode;
        Application.LoadLevel("main");
    }
}