using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//5月19日更新

public class AdMobReward : MonoBehaviour
{
    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る
    // Actionコールバック
    private Action onRewardEarned;// 広告が報酬を獲得しが閉じられたあとに実行する処理（必要に応じて代入）
    private Action onAdFailed; // 広告が失敗した際に実行する処理（Spinner非表示用）
    
    public bool IsRewardedAdLoaded { get; private set; } = false;
    
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
    //string adUnitId = "ca-app-pub-7439888210247528/2150238155";//ここにAndroidのリワード広告IDを入力
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
    //string adUnitId = "ca-app-pub-7439888210247528/5351116568";//ここにiOSのリワード広告IDを入力
#else
       string adUnitId = "unexpected_platform";
#endif

    private void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true; //Actionをメインスレッドで実行できるようにする
        #if UNITY_IPHONE
        MobileAds.SetiOSAppPauseOnBackground(true);
        #endif   
        //CreateAndLoadRewardedAd();
    }
    //リワード広告を読み込むのは各Sceneのタイミングで実行されている
    //リワード広告読み込む関数
    public void CreateAndLoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
      if (rewardedAd != null)
      {
            rewardedAd.Destroy();
            rewardedAd = null;
      }
        //リワード広告初期化
        var adRequest = new AdRequest();

      // send the request to load the ad.
      RewardedAd.Load(adUnitId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                /*
                  Debug.LogError("Rewarded読み込み失敗 " +
                                 "with error : " + error);*/
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              rewardedAd = ad;    Debug.Log("リワード広告を読み込み成功");
              RegisterAdEvents(ad);
              
              // 読み込み成功したのでIsRewardAdLoadedをtrueにする
              IsRewardedAdLoaded = true;
          });
    }
    /// <summary>
    /// 広告イベントの登録
    /// </summary>
    private void RegisterAdEvents(RewardedAd ad)
    {
        ad.OnAdPaid += (adValue) =>
        {
            Debug.Log($"広告収益発生: {adValue.Value} {adValue.CurrencyCode}");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("広告クリック");
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("広告インプレッション記録");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("リワード広告全画面表示開始");

        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("リワード広告全画面表示終了");
            CreateAndLoadRewardedAd(); // 次回のために再ロード
        };

        ad.OnAdFullScreenContentFailed += (error) =>
        {
            Debug.LogError($"広告表示失敗: {error}");
            onAdFailed?.Invoke();
            CreateAndLoadRewardedAd(); // 次回のために再ロード
        };
    }
    //リワード広告を表示する関数
    //ボタンに割付けして使用
    public void ShowAdMobReward()
    {
        const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        // Spinner表示（広告表示準備）

    if (rewardedAd != null && rewardedAd.CanShowAd())
    {
        rewardedAd.Show((Reward reward) =>
        {
            Debug.Log($"ユーザーが報酬を獲得: {reward.Type}, {reward.Amount}");

            // ここで報酬処理を呼び出す
            onRewardEarned?.Invoke();
        });
    }
    else{
        onAdFailed?.Invoke();
        //Debug.Log("リワード広告読み込み未完了");
    }
     
    }
  
    // ------------------------------
    // コールバックを登録  メソッドはAdRewardManager.csで準備
    // ------------------------------

    // 広告が失敗した際のコールバック設定
    public void SetOnAdFailedCallback(Action callback)
    {
        onAdFailed = callback;
    }
    public void SetOnRewardEarnedCallback(Action callback)
    {
        onRewardEarned = callback;
    }
    //Rewardの破棄とメモリリリース
    public void DestroyRewardAd()
    {
    if (rewardedAd != null)
    {
        
        //Debug.Log("Destroying rewardedAd ad.");
        rewardedAd.Destroy();
        rewardedAd = null;//リソースの解放
    }
    }
}


