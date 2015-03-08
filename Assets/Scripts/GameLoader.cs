using UnityEngine;
using System;

using Laska;

public class GameLoader : MonoBehaviour
{
    public static GameMode GameMode = GameMode.LOCAL_VS_LOCAL;

    public static bool IsVsAI
    {
        get
        {
            return GameMode == GameMode.AI_VS_PLAYER || GameMode == GameMode.PLAYER_VS_AI;
        }
    }

    public static bool LockFirstMove
    {
        get
        {
            if (GameMode == GameMode.REMOTE_VS_LOCAL)
            {
                Debug.Log("I AM CLIENT! " + NetHandler.ClientColor);
                return NetHandler.ClientColor == Colour.Black;
            }
            if (GameMode == GameMode.LOCAL_VS_REMOTE)
            {
                Debug.Log("I AM SERVER " + NetHandler.ClientColor);
                return NetHandler.ClientColor == Colour.White;
            }
            return false;
        }
    }

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