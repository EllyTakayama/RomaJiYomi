using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StoreLink : MonoBehaviour
{
    string url;
    // Start is called before the first frame update
    void Start()
    {
        //https://play.google.com/store/apps/details?id=<パッケージ名>
        //urlの作成
        #if UNITY_ANDROID
        url = "https://play.google.com/store/apps/details?id=com.RieTakayama.RomajiYomi";

        #endif 
        #if UNITY_UNITY_IOS
        url = "";
        #endif 

        #if UNITY_EDITOR
        //http://apps.apple.com/<国>/app/<アプリ名>/id<ストア ID>
        url = "https://itunes.apple.com/jp/app/id1636226525?mt=8";
        #endif 
    }
    public void StoreLinkButton()
    { 
        //Twitter投稿画面の起動
        Application.OpenURL(url);
        print("StoreLink");
    }
}
