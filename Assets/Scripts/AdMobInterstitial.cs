using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Common;

public class AdMobInterstitial : MonoBehaviour
{
    //Button押した時にブール取得　Home false Return trueで行き先のシーンを取得する
    public string AdSceneName;//ホームへ移動する
    private bool rewardeFlag = false;//リワード広告の報酬付与用　初期値はfalse
    private bool SpinnerFlag = false;//Spinnerパネル表示よう　初期値はfalse
    private bool OpenInterAdFlag = false;//リワード広告全面表示　初期値はfalse
    //private bool CloseInterAdFlag = false;//リワード広告全面表示　初期値はfalse
    //public GameObject AdMobManager;//各SceneのアドモブManager
    //public GameObject SpinnerPanel;//シーン移動の間を持たせるようのPanel
    private InterstitialAd interstitialAd;//InterstitialAd型の変数interstitialを宣言　この中にインタースティシャル広告の情報が入る
    private bool isInterstitialAdsRemoved; // 課金フラグ
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/1033173712";//TestAndroidのインタースティシャル広告ID
    //string adUnitId = "ca-app-pub-7439888210247528/6016496823";//ここにAndroidのインタースティシャル広告IDを入力

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";//TestiOSのインタースティシャル広告ID
        //string adUnitId = "ca-app-pub-7439888210247528/6549466402";//ここにiOSのインタースティシャル広告IDを入力

#else
        string adUnitId = "unexpected_platform";
#endif
    private void Start()
    {  MobileAds.RaiseAdEventsOnUnityMainThread = true;
        //RequestInterstitial();//読み込み
        MobileAds.SetiOSAppPauseOnBackground(true);
        // 課金状態の読み込み
        isInterstitialAdsRemoved = ES3.KeyExists("isInterstitialAdsRemoved") && ES3.Load<bool>("isInterstitialAdsRemoved");

        if (!isInterstitialAdsRemoved)
        {
            RequestInterstitial(); // インタースティシャル広告の読み込み
        }
       //AdSceneName = SceneManager.GetActiveScene().name;

    }
    private void Update()
    {
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        if (rewardeFlag == true)
        {
            rewardeFlag = false;

            //Debug.Log("rewardFlag" + rewardeFlag);
            SceneManager.LoadScene(AdSceneName);
           /*
            if (AdSceneName == "TopScene")
            {
                SceneManager.LoadScene("TopScene");
                Debug.Log("Home,TopScene");
            }
            else if (AdSceneName == "KihonScene")
            {
                SceneManager.LoadScene("KihonScene");
                Debug.Log("Inter,KihonScene");
            }
            else if (AdSceneName == "RenshuuScene")
            {
                SceneManager.LoadScene("RenshuuScene");
                Debug.Log("Inter,RenshuuScene");
            }
            else if (AdSceneName == "TikaraScene")
            {
                SceneManager.LoadScene("TikaraScene");
                Debug.Log("Inter,TikaraScene");
            }
            else if (AdSceneName == "GachaScene")
            {
                SceneManager.LoadScene("GachaScene");
                Debug.Log("Inter,GachaScene");
            }
            else
            {
                SceneManager.LoadScene("TopScene");
            }
            //SpinnerPanel.SetActive(false);
            */
        }

        if (OpenInterAdFlag == true)
        {
            //Debug.Log("インタースティシャルOpenInterAdFlag" + OpenInterAdFlag);
            OpenInterAdFlag = false;

        }
    }

    //インタースティシャル広告を表示する関数
    //ボタンなどに割付けして使用
    public void ShowAdMobInterstitial()
    {
        if (isInterstitialAdsRemoved)
        {
            SceneManager.LoadScene(AdSceneName);
            return;
        }
        if (interstitialAd != null && interstitialAd.CanShowAd())
    {
        interstitialAd.Show();
        //Debug.Log("インタースティシャル広告表示");
    }
    else
    {
        SceneManager.LoadScene(AdSceneName);
   
    }
}
       
    //インタースティシャル広告を読み込む関数
    public void RequestInterstitial()
    {

        if (interstitialAd != null)
      {
            interstitialAd.Destroy();
            interstitialAd = null;
      }
      var adRequest = new AdRequest.Builder()
              .AddKeyword("AdmobInterstitial")
              .Build();

      // send the request to load the ad.
      InterstitialAd.Load(adUnitId, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                /*
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);*/
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              interstitialAd = ad;

              ad.OnAdFullScreenContentOpened += () =>
    {
        OpenInterAdFlag = true;
        SpinnerFlag = true;
        Debug.Log("Interstitial ad full screen content opened.");

    };
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        //Debug.Log("インタースティシャル終了name" + AdSceneName);
        rewardeFlag = true;
        //インタースティシャル広告は使い捨てなので一旦破棄
        interstitialAd.Destroy();
        //Debug.Log("インタースティシャル広告破棄");
        RequestInterstitial();

    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        /*
        Debug.LogError("Interstitial ad failed to open full screen content " +
                       "with error : " + error);*/
                       RequestInterstitial();

    };            
            });

    }

    //インタースティシャルの破棄とメモリリリース
    public void DestroyInterstitialAd()
    {
    if (interstitialAd != null)
    {
        
        Debug.Log("Destroying interstitialAd.");
        interstitialAd.Destroy();
        interstitialAd = null;//リソースの解放
    }
    }
    // インタースティシャル広告の課金完了後に非表示にするメソッド
    public void OnInterstitialPurchaseCompleted()
    {
        isInterstitialAdsRemoved = true;
        ES3.Save("isInterstitialAdsRemoved", isInterstitialAdsRemoved); // 課金状態を保存
        DestroyInterstitialAd(); // 表示中の広告を削除
    }
}


