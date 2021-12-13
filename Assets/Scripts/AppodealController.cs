using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using Facebook.Unity;


public class AppodealController : MonoBehaviour, IInterstitialAdListener
{


    public static AppodealController instance;

    void Start()
    {
        instance = this;

            bool cons = true;
        Appodeal.setTriggerOnLoadedOnPrecache(Appodeal.INTERSTITIAL, true);

        Debug.Log("b4 init");
        Appodeal.initialize("1afb29f263587ba169fe9501827a57401dc5c0221ed190e2", Appodeal.INTERSTITIAL, cons );
        Appodeal.setInterstitialCallbacks(this);
        Debug.Log("after init");

        Start_Init_FB();

    }


    void Start_Init_FB()
    {
        FB.Init(this.OnInitComplete, this.OnHideUnity);
    }

    private void OnInitComplete()
    {



        if (FB.IsInitialized)
        {
            FB.ActivateApp();

            FB.LogAppEvent("startApp");



            // FB.GetAppLink(DeepLinkCallback);
           // FB.Mobile.FetchDeferredAppLinkData(DeepLinkCallback);

        }
        else
        {

        }

    }

    private void OnHideUnity(bool isGameShown)
    {
    }





    public void ShowInterstitial()
    {
        Debug.Log("SHow ads function");


        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else
        {
            Appodeal.cache(Appodeal.INTERSTITIAL);
        }
    }




    #region Interstitial callback handlers

    public void onInterstitialLoaded(bool isPrecache)
    {
        print("Appodeal. Interstitial loaded");
    }

    public void onInterstitialFailedToLoad()
    {
        print("Appodeal. Interstitial failed");
    }

    public void onInterstitialShowFailed()
    {
        print("Appodeal. Interstitial show failed");
    }

    public void onInterstitialShown()
    {
        print("Appodeal. Interstitial opened");
    }

    public void onInterstitialClosed()
    {
        print("Appodeal. Interstitial closed");
    }

    public void onInterstitialClicked()
    {
        print("Appodeal. Interstitial clicked");
    }

    public void onInterstitialExpired()
    {
        print("Appodeal. Interstitial expired");
    }

    #endregion









}
