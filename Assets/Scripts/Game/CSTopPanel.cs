using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSTopPanel : MonoBehaviour {

    public CSGameStore store;
    public CSExperiencePanel xpPanel;
    public CSBankCoinPanel coinPanel;

    private void Start()
    {
        if (AssetBundleHandler.startFlag)
        {
            AssetBundleHandler.startFlag = false;
        }
        else if(!AssetBundleHandler.startFlag && SceneManager.GetActiveScene().name == "GameLobby")
        {
            store.Appear();
        }

    }

    public void OnBuyCoins()
    {
        store.Appear();
    }

    public void AddXPValue(float value)
    {
        xpPanel.AddValue(value);
    }

    public void AddCoins(float coins)
    {
        coinPanel.Add(coins);
    }

    public virtual void OnLobby(string sceneName)
    {
        CSLFLoading.state = CSLoadingState.Out;
        SceneManager.LoadScene(sceneName);
        CSSoundManager.instance.Stop("reel_spin");
    }
}
