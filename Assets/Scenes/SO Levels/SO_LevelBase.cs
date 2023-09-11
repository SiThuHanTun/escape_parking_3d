using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level", menuName = "Unparking Game/Levels")]
public class SO_LevelBase : ScriptableObject
{
    public string levelname;
    public string levelDescription;
    public int turnsNeeded;
    public string difficulty;
    public string sceneNameToLoad;

    void OnValidate()
    {
        if (turnsNeeded > 35)
        {
            difficulty = "MASTER";
            return;
        }
        else if (turnsNeeded > 20)
        {
            difficulty = "HARD";
            return;
        }
        else if (turnsNeeded > 15)
        {
            difficulty = "MEDIUM";
            return;
        }
        else if (turnsNeeded > 10)
        {
            difficulty = "EASY";
            return;
        }
        else
        {
            difficulty = "BABY";
        }
    }
}