using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string myWebsite = "https:\\www.iamsithuhantun.com";
    GoogleAdMobController googleAdMobController;

    public void Start()
    {
        googleAdMobController = gameObject.AddComponent<GoogleAdMobController>();
        googleAdMobController.RequestBannerAd();
        googleAdMobController.RequestAndLoadAppOpenAd();
        googleAdMobController.ShowAppOpenAd();
        

        // TODO: Request an app open ad.
        //MobileAds.Initialize((initStatus) =>
        //{
        //    AppOpenAdManager.Instance.LoadAd();
        //    //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        //    // TODO: Show an app open ad if available.
        //    AppOpenAdManager.Instance.ShowAdIfAvailable(AppOpenAdManager.Instance.ad
        //                                                , AppOpenAdManager.Instance.request
        //                                                , AppOpenAdManager.Instance.error);
        //});
    }

    void Update()
    {
        //googleAdMobController = gameObject.AddComponent<GoogleAdMobController>();
    }

    //public void OnAppStateChanged(AppState state)
    //{
    //    if (state == AppState.Foreground)
    //    {
    //        // TODO: Show an app open ad if available.
    //        AppOpenAdManager.Instance.ShowAdIfAvailable(AppOpenAdManager.Instance.ad
    //                                                    , AppOpenAdManager.Instance.request
    //                                                    , AppOpenAdManager.Instance.error);
    //    }
    //}

    //For Menu Buttons
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    //Exit
    public void ExitGame()
    {
        Application.Quit();
    }

    //Visit
    public void VisitWebsite()
    {
        Application.OpenURL(myWebsite);
    }
}
