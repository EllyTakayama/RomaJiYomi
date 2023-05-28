using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.SceneManagement;

public class AdmobStart : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //メインスレッドでMobile Ads SDK イベントを Unity メインスレッドと同期させる
        MobileAds.RaiseAdEventsOnUnityMainThread = true;    
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        //print("Admob初期化");

        
    }

}


