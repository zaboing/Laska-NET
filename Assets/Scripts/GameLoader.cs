using UnityEngine;
using System;

public class GameLoader : MonoBehaviour
{
    public static GameMode GameMode = GameMode.LOCAL_VS_LOCAL;
	
	
    public void LoadGame(GameMode mode)
    {
        GameMode = mode;
        Application.LoadLevel("main");
    }
	
	public void LoadGame(string mode) 
	{
		LoadGame((GameMode)Enum.Parse(typeof(GameMode), mode));
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