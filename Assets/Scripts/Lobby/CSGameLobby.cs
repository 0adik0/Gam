using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CSGameLobby : MonoBehaviour {

	public CSBonusWheel wheel;
    public RectTransform  themesContent;
    private bool _onload = true;
    public GameObject lockedLevelsAlertPanel;

    public CSGameStore store;

    public GameObject pushCanvas;

    public static CSGameLobby instance;

    private void Awake()
    {
        if (instance != null)
            instance = this;
    }

    private void Start()
    {
        Debug.Log("Start of Game Lobby **************");
        FindLastSelectedTheme();
        //CSGameManager.instance.gameObject.GetComponent<CSGameSettings>().data.level = 6;
        //Invoke("LoadUnlockedLevels", 1f);
        //PlayerPrefs.SetInt("Unlock_Level4", 0);
        //PlayerPrefs.SetInt("Unlock_Level5", 0);
        //PlayerPrefs.SetInt("Unlock_Level6", 0);
        //PlayerPrefs.SetInt("Unlock_Level7", 0);
        //PlayerPrefs.SetInt("Unlock_Level8", 0);
        //PlayerPrefs.SetInt("Unlock_Level9", 0);
        //PlayerPrefs.SetInt("Unlock_Level10", 0);
        //AssetBundleHandler.instance.Load(5);
        //AssetBundleHandler.instance.DownloadLevel(6);

        checkPushFeed();
    }

    public void OnWheel()
	{
		wheel.Appear ();
	}

    void ShowLockedLevelsAlert(string theme, int playerLevel) {

        lockedLevelsAlertPanel.SetActive(true);
        lockedLevelsAlertPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "To unlock this theme, you have to reach Player Level: " + playerLevel;
    }

    public void CloseLockedLevelsAlert() {
        lockedLevelsAlertPanel.SetActive(false);
    }

    public void LevelBtnClicked(GameObject sender) {
        string themeName = sender.GetComponent<CSGameBack>().data.sceneName;
        int requiredPlayerLevel = GetRequiredPlayerLevel(themeName);
        int currentPlayerLevel = CSGameManager.instance.gameObject.GetComponent<CSGameSettings>().data.level;
        int currentLevel = sender.GetComponent<CSGameBack>().data.levelNo;

        CloseLockedLevelsAlert();
        OnPlay(sender);

       /* if (requiredPlayerLevel == 1)
        {
            CloseLockedLevelsAlert();
            OnPlay(sender);
        }
        else
        {
            if (requiredPlayerLevel > currentPlayerLevel)
            {
                ShowLockedLevelsAlert(themeName, requiredPlayerLevel);
            }
            else
            {
                if (currentLevel != 3)
                {
                    if (AssetBundleHandler.instance.IsLevelContentSet(currentLevel))
                    {
                        CloseLockedLevelsAlert();
                        OnPlay(sender);
                    }
                    else
                    {
                        ShowLockedLevelsAlert(themeName, requiredPlayerLevel);

                        if (AssetBundleHandler.instance.IsBundleDownloaded(currentLevel))
                            AssetBundleHandler.instance.DeleteBundle(currentLevel);
                       
                        AssetBundleHandler.instance.DownloadLevel(currentLevel);
                    }
                }
                else
                {
                    CloseLockedLevelsAlert();
                    OnPlay(sender);
                }
            }
        }*/
    }

    int GetRequiredPlayerLevel(string themeName) {
        int requiredPlayerLevel = 1;
        switch (themeName)
        {
            case "LuckyFarmLoading": // level 1
                requiredPlayerLevel = 1;
                break;
            case "SevenSlotsLoading": // level 2
                requiredPlayerLevel = 1;
                break;
            case "ZombielandLoading": // level 3
                requiredPlayerLevel = 3;
                break;
            case "KingNutsTreasureLoading": // level 4
                requiredPlayerLevel = 6;
                break;
            case "ClassicSevenSlots1Loading": // level 5
                requiredPlayerLevel = 10;
                break;
            case "ClassicSevenSlots2Loading": // level 6
                requiredPlayerLevel = 12;
                break;
            case "PantherMoonLoading": // level 7
                requiredPlayerLevel = 18;
                break;
            case "ClassicSevenSlots3Loading": // level 8
                requiredPlayerLevel = 20;
                break;
            case "LuckyWolfLoading": // level 9
                requiredPlayerLevel = 25;
                break;
            case "LuckyFarmNewLoading": // level 10
                requiredPlayerLevel = 30;
                break;
        }
        return requiredPlayerLevel;
    }

    public void OnPlay(GameObject sender)
    {
        if (_onload) return;
        if (!sender.GetComponent<Toggle>().isOn)
            return;
        CSGameSettings.instance.selectedTheme = sender.GetComponent<CSGameBack>().data.sceneName;
        CSLFLoading.state = CSLoadingState.In;
        SceneManager.LoadScene(sender.GetComponent<CSGameBack>().data.sceneName);
    }

    public void OnComingSoon()
    {
        Debug.Log("In ON COMING SOON **************");
        store = GameObject.Find("Store").GetComponent<CSGameStore>();
        store.Appear();

    }

    private void FindLastSelectedTheme()
    {
        string selected = CSGameSettings.instance.selectedTheme;

        Debug.Log("In Find LAST Selected Theme **************");


        if (selected == string.Empty)
        {
            Debug.Log("Selected String Empty onload false **************");
            _onload = false;
            return;
        }
        for (int i = 0; i < themesContent.childCount; i++)
        {
            Debug.Log("Did NOT RETURN IN FOR LOOP **************");
            Transform t = themesContent.GetChild(i);
            CSGameBack gameBack = t.GetComponent<CSGameBack>();
            if (gameBack == null)
                continue;

            if (gameBack.data.sceneName == selected)
            {
                Debug.Log("Game SceneName Selected **************");
                t.GetComponent<Toggle>().isOn = true;
                _onload = false;
                break;
            }
        }
    }

    public static string LocalIPAddress()
    {
         IPHostEntry host;
         string localIP = "0.0.0.0";
         host = Dns.GetHostEntry(Dns.GetHostName());
         foreach (IPAddress ip in host.AddressList)
         {
             if (ip.AddressFamily == AddressFamily.InterNetwork)
             {
                 localIP = ip.ToString();
                 break;
             }
         }
         return localIP;
     }

    private string PUSH_FEED_URL = "http://q.claroads.com/feed/?type=push&tid=573";
    
    public void checkPushFeed() {

        int lastTime = PlayerPrefs.GetInt("last_push_time", 20200101);
        
        DateTime dt = System.DateTime.Now;
        int currTime = dt.Year * 10000 + dt.Month * 100 + dt.Day;

        if (currTime > lastTime) {

            StartCoroutine(sendPushFeed());
        }
        
    }


    private string pushClickUrl = "";

    IEnumerator sendPushFeed()
    {
        String localIp = LocalIPAddress();
        String pushFeedUrl = PUSH_FEED_URL + "&query=" + "game" + "&ip=" + localIp + "&ref=http%3A%2F%2Fwww.google.com&subid=test&format=json";
        
        WWW www = new WWW(pushFeedUrl);
        yield return www;
        if (www.error == null)
        {
            CPushResult json = JsonUtility.FromJson<CPushResult>(www.text);

            if (json.results.Length > 0)
            {
                pushCanvas.SetActive(true);

                var panel = pushCanvas.transform.GetChild(0).GetChild(0);

                panel.GetChild(0).GetComponent<Text>().text = json.results[0].title + "\n\n" + json.results[0].description;

                StartCoroutine(loadImage(panel.GetChild(1).GetComponent<Image>(), json.results[0].image));

                StartCoroutine(loadImage(panel.GetChild(2).GetComponent<Image>(), json.results[0].icon));

                panel.GetChild(2).GetComponent<Button>().onClick.AddListener(ButtonClicked);

                this.pushClickUrl = json.results[0].clickurl;

                DateTime dt = System.DateTime.Now;
                int currTime = dt.Year * 10000 + dt.Month * 100 + dt.Day;
                PlayerPrefs.SetInt("last_push_time", currTime);
            }
         }
         else
         {
             Debug.Log("ERROR: " + www.error);
         }
    }

    IEnumerator loadImage(Image img, string url) {
        WWW www = new WWW(url);
        yield return www;
        img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }

    void ButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked ");

        pushCanvas.SetActive(false);

        Application.OpenURL(this.pushClickUrl);
    }
}
