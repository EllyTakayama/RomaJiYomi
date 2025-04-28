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
    //private bool isInterstitialAdsRemoved; // 課金フラグ
    private Action onAdClosed; // 広告終了後に呼び出す処理
    private bool isAdShowing = false; //インタースティシャル広告表示中フラグ
    public bool IsAdShowing { get; private set; } //広告が現在表示中か？を外部参照できるプロパティ
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
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true; //Actionをメインスレッドで実行できるようにする
        MobileAds.SetiOSAppPauseOnBackground(true);
        // 他の初期化処理...
        //isInterstitialAdsRemoved = ES3.KeyExists("isInterstitialAdsRemoved") && ES3.Load<bool>("isInterstitialAdsRemoved");
        if (!GameManager.instance.isInterstitialAdsRemoved)
        {
            // 課金状態の読み込み
            RequestInterstitial(); // インタースティシャル広告の読み込み
        }
        //AdSceneName = SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// 外部から呼び出すインタースティシャル表示関数（引数にコールバックを追加）
    /// </summary>
    public void ShowAdMobInterstitial(Action onClosed = null)
    {
        onAdClosed = onClosed; //追加！
        if (GameManager.instance.isInterstitialAdsRemoved)
        {
            // 課金済みの場合、広告スキップして直接処理を実行
            onClosed?.Invoke();
            return;
        }
        if (IsAdShowing)
        {
            Debug.LogWarning("すでに広告表示中です。ShowAdMobInterstitialを無視します。");
            return;
        }

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            IsAdShowing = true; //広告表示開始フラグをON
            interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("Ad not ready, fallback immediately");
            onAdClosed?.Invoke(); // 広告未準備でもコールバック実行   
        }
    }

    //インタースティシャル広告を表示する関数
    //ボタンなどに割付けして使用
    /*
    public void ShowAdMobInterstitial()
    {
        if (GameManager.instance.isInterstitialAdsRemoved)
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
}*/
       
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

        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial failed to load: " + error);
                return;
            }

            interstitialAd = ad;

            // 🔸イベント設定
            ad.OnAdFullScreenContentOpened += () =>
            {
                OpenInterAdFlag = true;
                SpinnerFlag = true;
                Debug.Log("Interstitial opened");
            };

            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial closed");

                rewardeFlag = true; // Update() 内でシーン遷移をトリガー

                interstitialAd.Destroy(); // 使い捨て
                RequestInterstitial();     // 再読み込み
                onAdClosed?.Invoke(); // 終了後の処理を呼ぶ
                IsAdShowing = false; //広告表示完了 → フラグOFF
            };

            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial failed to open: " + error);
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
        DestroyInterstitialAd(); // 表示中の広告を削除
    }
}


