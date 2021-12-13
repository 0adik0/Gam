using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSCSS1Loading : CSLFLoading {

    protected override void Load()
    {
        AppodealController.instance.ShowInterstitial();

        switch (state)
        {
            case CSLoadingState.In: Load("ClassicSevenSlots1"); break;
            case CSLoadingState.Out: Load("GameLobby");
                //  CSGameManager.instance.GetComponent<CSAdMobManager>().ShowInterstitialAd();
               // AppodealController.instance.ShowInterstitial();

                break;
            default: break;
        }
    }

}
