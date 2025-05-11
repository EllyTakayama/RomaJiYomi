using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.SceneManagement;
//20250429日更新_10.0ver

public class AdMobBanner : MonoBehaviour
{

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";//テストAndroidのバナーID
        //string adUnitId = "ca-app-pub-7439888210247528/7402564833";//ここにAndroidのバナーIDを入力

#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-3940256099942544/2934735716";//テストiOSのバナーID
    //string adUnitId = "ca-app-pub-7439888210247528/1668674814";//ここにiOSのバナーIDを入力

#else
        string adUnitId = "unexpected_platform";
#endif
    
    private BannerView _bannerView;//BannerView型の変数bannerViewを宣言この中にバナー広告の情報が入る

    //シーン読み込み時からバナーを表示する
    //最初からバナーを表示したくない場合はこの関数を消してください。
    private IEnumerator WaitForGameManager()
    {
        // GameManager が生成されるまで待機
        while (GameManager.instance == null)
        {
            yield return null;
        }

        // 課金状態が読み込まれるまで待機
        while (!GameManager.instance.isPurchaseStateLoaded)
        {
            yield return null;
        }
        Debug.Log("[AdMobBanner] GameManagerがロードされました。課金状態をチェックします。");
        InitializeAdMobBanner();
    }
    //GameManagerからisBannerAdsRemovedを取得してからバナーの表示を呼び出す
    private void InitializeAdMobBanner()
    {
// GameManagerのフラグを直接使用
        if (GameManager.instance.AreAdsRemoved())
        {
            Debug.Log("[AdMobBanner] バナー広告は非表示状態です。");
        }
        else
        {
            Debug.Log("[AdMobBanner] バナー広告を表示します。");
            RequestBanner();
        }
    }
    private void Start()
    {
        StartCoroutine(WaitForGameManager());
    }
    
    //ボタン等に割り付けて使用
    //バナーを表示する関数
    public void BannerStart()
    {
        // 課金されていない場合のみバナー広告を表示
        if (!GameManager.instance.AreAdsRemoved())
        {
            RequestBanner();
        }      
    }
    //ボタン等に割り付けて使用
    //バナーを削除する関数
    public void BannerDestroy()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy(); // バナー削除
            _bannerView = null;    // リソースの解放
        }
        else
        {
            Debug.LogWarning("BannerDestroy called, but _bannerView is already null.");
        }
    }

    //アダプティブバナーを表示する関数
    public void RequestBanner()
    { 
        if (_bannerView != null)
        {
            _bannerView.Destroy(); // バナー削除
            _bannerView = null;    // リソースの解放
        }

        // 現在の画面の向き横幅を取得しバナーサイズを決定
        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // バナーを生成 new BannerView(バナーID,バナーサイズ,バナー表示位置)
        _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // BannerView型の変数 bannerViewの各種状態に関数を登録
        ListenToAdEvents();

        // リクエストを生成
        var adRequest = new AdRequest();

        // 広告表示
        _bannerView.LoadAd(adRequest);
    }
    private void ListenToAdEvents()
{
    // Raised when an ad is loaded into the banner view.
    _bannerView.OnBannerAdLoaded += () =>
    {
        /*
        Debug.Log("Banner view loaded an ad with response : "
            + _bannerView.GetResponseInfo());
            Debug.Log("バナー表示完了");*/
    };
    // Raised when an ad fails to load into the banner view.
    _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
    {
        /*
        Debug.LogError("バナー読み込み失敗 : "
            + error);*/
    };
    }
    
    //Bannerの破棄とメモリリリース
    public void DestroyBannerAd()
    {
    if (_bannerView != null)
    {
        //Debug.Log("Destroying banner ad.");
        _bannerView.Destroy();
        _bannerView = null;//リソースの解放
    }
    }
    // バナー広告の課金完了後に非表示にするメソッド
    public void OnBannerPurchaseCompleted()
    {
        DestroyBannerAd(); // 表示中のバナーを削除
    }

}

