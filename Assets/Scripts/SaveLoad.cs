using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoad
{
    public static int currentPlayedLevel;
    public static void Save()
    {
        if (PlayerPrefs.HasKey("bestLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("bestLevel");
            if (savedLevel < currentPlayedLevel + 1)
            {
                PlayerPrefs.SetInt("bestLevel", currentPlayedLevel + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("bestLevel", 1);
        }
    }

    public static void ResetSave()
    {
        PlayerPrefs.SetInt("bestLevel", 1);
    }

    public static int Load()
    {
        if (!PlayerPrefs.HasKey("bestLevel"))
        {
            Save();
        }
        return PlayerPrefs.GetInt("bestLevel");
    }
}
