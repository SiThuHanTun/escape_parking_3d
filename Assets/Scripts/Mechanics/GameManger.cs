using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    [SerializeField] GameObject winPanel;
    //[SerializeField] GameObject losePanel;
    [SerializeField] GameObject optionsPanel;

    [SerializeField] TMP_Text turnsText;
    int turns;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateTurnsText();
        winPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void AddTurn()
    {
        turns++;
        UpdateTurnsText();
    }

    void UpdateTurnsText()
    {
        turnsText.text = "Turns: " + turns;
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        //Unlock Next Level
        SaveLoad.Save();
    }

    public void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneToLoad) 
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
