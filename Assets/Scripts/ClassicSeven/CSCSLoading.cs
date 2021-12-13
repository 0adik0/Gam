using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSCSLoading : CSLFLoading {

    protected override void Load()
    {
        AppodealController.instance.ShowInterstitial();

        switch (state)
        {
            case CSLoadingState.In: Load("SevenSlots"); break;
            case CSLoadingState.Out: Load("GameLobby");
                //  CSGameManager.instance.GetComponent<CSAdMobManager>().ShowInterstitialAd();
               // AppodealController.instance.ShowInterstitial();
                break;
            default: break;
        }
    }

}
