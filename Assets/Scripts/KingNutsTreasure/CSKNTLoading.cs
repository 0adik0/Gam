using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSKNTLoading : CSLFLoading
{
    protected override void Load()
    {
        AppodealController.instance.ShowInterstitial();

        switch (state)
        {
            case CSLoadingState.In: Load("KingNutsTreasure"); break;
            case CSLoadingState.Out:
                Load("GameLobby");
                // CSGameManager.instance.GetComponent<CSAdMobManager>().ShowInterstitialAd();
               // AppodealController.instance.ShowInterstitial();

                break;
            default: break;
        }
    }
}
