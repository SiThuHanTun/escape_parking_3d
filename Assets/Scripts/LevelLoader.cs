using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] SO_LevelBase[] levelList;
    [SerializeField] GameObject levelHolderPrefab;
    [SerializeField] Transform levelGrid;
    int currentUnlockedLevel = 1;
    GoogleAdMobController googleAdMobController;

    // Start is called before the first frame update
    void Start()
    {
        currentUnlockedLevel = SaveLoad.Load();
        CreateLevelList();
        googleAdMobController = gameObject.AddComponent<GoogleAdMobController>();
        googleAdMobController.RequestAndLoadRewardedInterstitialAd();
        googleAdMobController.ShowRewardedInterstitialAd();
    }

    void CreateLevelList()
    {
        int numberOfLevel = 0;
        foreach (var item in levelList)
        {
            numberOfLevel++;
            GameObject newLevel = Instantiate(levelHolderPrefab, levelGrid, false);

            bool isUnlocked = numberOfLevel <= currentUnlockedLevel;
            newLevel.GetComponent<LevelHolder>().SetInfos(
                "Level " + numberOfLevel
                , item.levelname
                , item.levelDescription
                , item.difficulty
                , item.sceneNameToLoad
                , isUnlocked
                , numberOfLevel
            );
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
