using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Linq;
//using Facebook.Unity;
//using Facebook.MiniJSON;

public class CSFacebookManager : MonoBehaviour {

    //public static CSFacebookManager instance = null;
    //[HideInInspector] public string accessToken = null;

    //public bool isLoggedIn
    //{
    //    get { return FB.IsLoggedIn; }
    //}

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        instance = this;
    //        Loaded();
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void Loaded()
    //{
    //    Init();
    //}

    //private void Init(Action callback = null)
    //{
    //    FB.Init(() => {
    //        if (FB.IsInitialized)
    //        {
    //            FB.ActivateApp();
    //            if (callback != null)
    //                callback();
    //        }
    //    }, (show) => {
    //        Time.timeScale = !show ? 0f : 1f;
    //    });
    //}

    //public void Login(Action<bool> callback = null)
    //{
    //    var permissions = new List<string> { "public_profile", "email" };
    //    FB.LogInWithReadPermissions(permissions, (result) => {

    //        accessToken = isLoggedIn ? AccessToken.CurrentAccessToken.UserId : null;

    //        if (callback != null)
    //        {
    //            callback(isLoggedIn);
    //        }
    //    });
    //}

    //public void Logout(Action callback = null)
    //{
    //    FB.LogOut();
    //    if (callback != null)
    //    {
    //        callback();
    //    }
    //}

    //public void ShareLink(string uri, string photolink = null, Action<bool> result = null)
    //{
    //    if (!isLoggedIn)
    //        return;
    //    Uri link = uri != null ? new Uri(uri) : null;
    //    Uri image = photolink != null ? new Uri(photolink) : null;

    //    FB.ShareLink(new Uri(uri), photoURL: image, callback: (r) => {
    //        if (result != null)
    //        {
    //            result(!r.Cancelled && String.IsNullOrEmpty(r.Error));
    //        }
    //    });
    //}

    //public void AppRequest(string title, string message, Action<int> callback = null)
    //{
    //    if (!isLoggedIn)
    //        return;

    //    //List<object> filter = new List<object>() { "app_users" };

    //    //FB.AppRequest(message, null, filter, null, null, null, title, (result) =>
    //    FB.AppRequest(message, null, null, null, null, null, title, (result) =>
    //    {
    //        var dict = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
    //        if (callback != null)
    //        {
    //            callback(((IEnumerable<object>)dict["to"]).Count());
    //        }
    //    });
    //}

    //public void PostScreenShot(Action callback = null)
    //{
    //    if (!isLoggedIn)
    //        return;

    //    StartCoroutine(TakeScreenshot((result) => {
    //        Debug.Log("done");
    //        if (callback != null)
    //            callback();
    //    }));
    //}

    //private IEnumerator TakeScreenshot(FacebookDelegate<IGraphResult> callback)
    //{
    //    yield return new WaitForEndOfFrame();

    //    var width = Screen.width;
    //    var height = Screen.height;
    //    var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

    //    tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //    tex.Apply();
    //    byte[] screenshot = tex.EncodeToPNG();

    //    var wwwForm = new WWWForm();
    //    wwwForm.AddBinaryData("image", screenshot, "screenshot.png");
    //    wwwForm.AddField("message", "message of t post");
    //    FB.API("me/photos", HttpMethod.POST, callback, wwwForm);
    //}

}
