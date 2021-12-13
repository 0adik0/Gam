using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
using UnityEditor;

public class AssetBundleHandler : MonoBehaviour
{
	public string url;
    string bundlesPath;
    int levelNo = 4, tempLevelNo = 4;
    public CSSymbolData[] ScriptObjects_Level4, ScriptObjects_Level5, ScriptObjects_Level6, ScriptObjects_Level7, ScriptObjects_Level8, ScriptObjects_Level9, ScriptObjects_Level10;
    public static bool startFlag = true;
    bool loadingFlag = false;
    public GameObject downloadingPromptCanvas;

    public static AssetBundleHandler instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(downloadingPromptCanvas);
        }
    }

    private void Start()
    {
        for (int i = 4; i <= 10; i++) {
            if (IsLevelContentSet(i))
                Load(i);
        }
    }

    IEnumerator DownloadBundle(int levelValue){
		print("start");
        ShowLogs("Download has been started!");
        levelNo = levelValue;
        url = url + levelNo;

        UnityWebRequest www = UnityWebRequest.Get(url);

        DownloadHandler handle = www.downloadHandler;
        
        //Send Request and wait
        yield return www.SendWebRequest();

		if (www.isNetworkError)
		{

			UnityEngine.Debug.Log("Error while Downloading Data: " + www.error);
            //ShowLogs("Error while Downloading Data: " + www.error);
        }
		else
		{
			UnityEngine.Debug.Log("Success");
            //ShowLogs("Downloading Successfull");

            //handle.data

            //Construct path to save it
            string dataFileName = "level_" + levelValue;
            
			string tempPath = Path.Combine(Application.persistentDataPath + "/", dataFileName);
//			tempPath = Path.Combine(tempPath, );
			//Save
			save(handle.data, tempPath);
		}
        //www.Dispose();
    }

	void save(byte[] data, string path)
	{
		//Create the Directory if it does not exist
		if (!Directory.Exists(Path.GetDirectoryName(path)))
		{
			Directory.CreateDirectory(Path.GetDirectoryName(path));
            print("Creating Directory...");
		}

		try
		{
			File.WriteAllBytes(path, data);
			Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
            //ShowLogs("Saved Data to: " + path.Replace("/", "\\"));
            Load(levelNo);
        }
		catch (System.FormatException e)
		{
			Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
			Debug.LogWarning("Error: " + e.Message);
		}
	}

    IEnumerator LoadLevel(CSSymbolData[] ScriptObjects)
    {
        //ShowLogs("Loading Level");
        if (levelNo == 4)
            bundlesPath = Application.persistentDataPath + "/level_4";
        else if (levelNo == 5)
            bundlesPath = Application.persistentDataPath + "/level_5";
        else if (levelNo == 6)
            bundlesPath = Application.persistentDataPath + "/level_6";
        else if (levelNo == 7)
            bundlesPath = Application.persistentDataPath + "/level_7";
        else if (levelNo == 8)
            bundlesPath = Application.persistentDataPath + "/level_8";
        else if (levelNo == 9)
            bundlesPath = Application.persistentDataPath + "/level_9";
        else if (levelNo == 10)
            bundlesPath = Application.persistentDataPath + "/level_10";
        //path = path + levelNo + ".unity3d";
        yield return new WaitForSeconds(2f);
        AssetBundleCreateRequest bundle = AssetBundle.LoadFromFileAsync(bundlesPath);
        yield return bundle;

        AssetBundle myLoadedAssetBundle = bundle.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            //ShowLogs("Failed to load AssetBundle!");
            if (File.Exists(bundlesPath))
                File.Delete(bundlesPath);
            yield break;
        }

        object[] ob = myLoadedAssetBundle.LoadAllAssets<Sprite>();
        for (int m = 0; m < ScriptObjects.Length; m++)
        {
            for (int i = 0; i < ob.Length; i++)
            {
                if ((ob[i] as Sprite).name == ScriptObjects[m].sprite.name)
                {
                    ScriptObjects[m].sprite = ob[i] as Sprite;
                    //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ob[i] as Sprite;
                }
                else
                {
                    for (int n = 0; n < ScriptObjects[m].animationData.frames.Length; n++)
                    {
                        if ((ob[i] as Sprite).name == ScriptObjects[m].animationData.frames[n].name)
                        {
                            ScriptObjects[m].animationData.frames[n] = ob[i] as Sprite;
                            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ob[i] as Sprite;
                        }
                    }
                }
            }
        }
        if (!IsLevelContentSet(levelNo))
        {
            downloadingPromptCanvas.SetActive(true);
            downloadingPromptCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Next theme has been downloaded, please restart the game to load the theme.";
            PlayerPrefs.SetInt("Unlock_Level" + levelNo, 1);
        }
        loadingFlag = true;
        myLoadedAssetBundle.Unload(false);
        //ShowLogs("Loading Complete");
    }

    public void Load(int level)
    {
        levelNo = level;
        if(level == 4)
            StartCoroutine(LoadLevel(ScriptObjects_Level4));
        else if(level == 5)
            StartCoroutine(LoadLevel(ScriptObjects_Level5));
        if (level == 6)
            StartCoroutine(LoadLevel(ScriptObjects_Level6));
        else if (level == 7)
            StartCoroutine(LoadLevel(ScriptObjects_Level7));
        if (level == 8)
            StartCoroutine(LoadLevel(ScriptObjects_Level8));
        else if (level == 9)
            StartCoroutine(LoadLevel(ScriptObjects_Level9));
        if (level == 10)
            StartCoroutine(LoadLevel(ScriptObjects_Level10));
    }

    public bool IsBundleDownloaded(int lvlNo) {
        bool flag = true;
        switch (lvlNo) {
            case 4:
                flag = File.Exists(Application.persistentDataPath + "/level_4");
                break;
            case 5:
                flag = File.Exists(Application.persistentDataPath + "/level_5");
                break;
            case 6:
                flag = File.Exists(Application.persistentDataPath + "/level_6");
                break;
            case 7:
                flag = File.Exists(Application.persistentDataPath + "/level_7");
                break;
            case 8:
                flag = File.Exists(Application.persistentDataPath + "/level_8");
                break;
            case 9:
                flag = File.Exists(Application.persistentDataPath + "/level_9");
                break;
            case 10:
                flag = File.Exists(Application.persistentDataPath + "/level_10");
                break;
        }
        return flag;
    }

    public void DownloadLevel(int level) {
        if (level != 0)
        {
            print("level: " + level);
            if (!IsBundleDownloaded(level))
            {
                StartCoroutine(DownloadBundle(level));
            }
            else
            {
                print("Level is already downloaded");
                if (!IsLevelContentSet(level))
                {
                    File.Delete(GetBundlePath(level));
                    DownloadLevel(level);
                }
                else
                    print("Level is already loaded");
            }
        }
        else {
            print("Level 0 is selected");
        }
    }

    public bool IsLevelContentSet(int level) {
        bool flag = false;
        if (PlayerPrefs.GetInt("Unlock_Level" + level) == 0)
            flag = false;
        else
            flag = true;
        return flag;
    }
    
    public IEnumerator LoadAllLevels() {
        print("LoadingLevel" + tempLevelNo);
        loadingFlag = false;
        int requiredPlayerLevel = 0;
        int playerLevel = CSGameManager.instance.gameObject.GetComponent<CSGameSettings>().level;
        if (tempLevelNo == 4)
            requiredPlayerLevel = 6;
        else if(tempLevelNo == 5)
            requiredPlayerLevel = 10;
        else if (tempLevelNo == 6)
            requiredPlayerLevel = 12;
        else if (tempLevelNo == 7)
            requiredPlayerLevel = 18;
        else if (tempLevelNo == 8)
            requiredPlayerLevel = 20;
        else if (tempLevelNo == 9)
            requiredPlayerLevel = 25;
        else if (tempLevelNo == 10)
            requiredPlayerLevel = 30;
        if (playerLevel >= requiredPlayerLevel) // level 4
        {
            if (IsBundleDownloaded(tempLevelNo))
            {

                if (!IsLevelContentSet(tempLevelNo))
                {
                    Load(tempLevelNo);
                }
                else {
                    loadingFlag = true;
                }
            }
            else
            {
                DownloadLevel(tempLevelNo);
            }
        }
        yield return new WaitUntil(() => loadingFlag == true);
        loadingFlag = false;
        if (tempLevelNo < 10)
        {
            tempLevelNo++;
            StartCoroutine(LoadAllLevels());
        }
        else {
            tempLevelNo = 4;
        }
    }

    public string GetBundlePath(int lvl) {
        string path = "";
        if (lvl == 4)
            path = Application.persistentDataPath + "/level_4";
        else if (lvl == 5)
            path = Application.persistentDataPath + "/level_5";
        else if (lvl == 6)
            path = Application.persistentDataPath + "/level_6";
        else if (lvl == 7)
            path = Application.persistentDataPath + "/level_7";
        else if (lvl == 8)
            path = Application.persistentDataPath + "/level_8";
        else if (lvl == 9)
            path = Application.persistentDataPath + "/level_9";
        else if (lvl == 10)
            path = Application.persistentDataPath + "/level_10";
        return path;
    }

    public void DeleteBundle(int lvl) {
        string path = "";
        if (lvl == 4)
            path = Application.persistentDataPath + "/Level_4";
        else if (lvl == 5)
            path = Application.persistentDataPath + "/Level_5";
        else if (lvl == 6)
            path = Application.persistentDataPath + "/Level_6";
        else if (lvl == 7)
            path = Application.persistentDataPath + "/Level_7";
        else if (lvl == 8)
            path = Application.persistentDataPath + "/Level_8";
        else if (lvl == 9)
            path = Application.persistentDataPath + "/Level_9";
        else if (lvl == 10)
            path = Application.persistentDataPath + "/Level_10";
        File.Delete(path);
    }

    public void RestartGame() {
        Application.Quit();
    }

    IEnumerator WaitForLogs(string msg)
    {
        downloadingPromptCanvas.SetActive(true);
        downloadingPromptCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = msg;
        downloadingPromptCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        downloadingPromptCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        downloadingPromptCanvas.SetActive(false);
    }

    public void ShowLogs(string msg)
    {
        StartCoroutine(WaitForLogs(msg));
    }
}
