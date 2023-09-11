using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text levelNameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text difficultyText;
    string sceneNameToLoad;
    [SerializeField] Button startButton;
    int myLevelId;

    public void SetInfos(string _levelText, string _levelName
                        , string _leveldescription, string _difficulty
                        , string _sceneToLoad, bool _isUnlocked
                        , int _myLevelId)
    {
        levelText.text = _levelText;
        levelNameText.text = _levelName;
        descriptionText.text = _leveldescription;
        difficultyText.text = _difficulty;
        sceneNameToLoad = _sceneToLoad;
        startButton.interactable = _isUnlocked;
        myLevelId = _myLevelId;
    }

    //Called from the button
    public void LoadLevel()
    {
        SaveLoad.currentPlayedLevel = myLevelId;
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
