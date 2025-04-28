using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//5月19日更新

public class AdMobReward : MonoBehaviour
{//AdMobRewardが報酬をえた時にコールバックを起こせるように変更。受けてはAdReward.cs
    private bool rewardeFlag = false;//リワード広告の報酬付与用　初期値はfalse
    private bool rewardeFlag1 = false;//リワード広告の報酬付与用　初期値はfalse
    private bool SpinnerFlag = false;//Spinnerパネル表示用　初期値はfalse
    private bool OpenRewardFlag = false;//リワード広告全面表示　初期値はfalse
    private bool NoShowFlag = false;//リワード広告が読み込めていなかった場合　初期値はfalse

    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る
    
    // Actionコールバック
    private Action onRewardEarned;// 広告が報酬を獲得しが閉じられたあとに実行する処理（必要に応じて代入）
    private Action onAdFailed; // 広告が失敗した際に実行する処理（Spinner非表示用）
    private Action onAdOpened;//広告が開いた時のコールバック
    
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
    //string adUnitId = "ca-app-pub-7439888210247528/2150238155";//ここにAndroidのリワード広告IDを入力
#elif UNITY_IPHONE
　　　　　
    string adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
    //string adUnitId = "ca-app-pub-7439888210247528/5351116568";//ここにiOSのリワード広告IDを入力
#else
       string adUnitId = "unexpected_platform";
#endif
    public GameObject afterAdPanel;
    public GameObject SpinnerPanel;
    public Text rewardText;//広告読み込めなかった時にテキスト差し替え

    private void Start()
    {
        #if UNITY_IPHONE
        MobileAds.SetiOSAppPauseOnBackground(true);
        #endif   
        //CreateAndLoadRewardedAd();
    }
    /*
    private void Update()
    {
        //広告がダウンロード失敗して表示されない場合
        if (NoShowFlag == true)
        {
            NoShowFlag = false;
            CreateAndLoadRewardedAd();
            //rewardText.text = "広告がダウンロード\nできませんでした";
        }
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        //報酬をゲットしてリワードをクローズする場合
        if (rewardeFlag1 == true && rewardeFlag == true)
        {
            rewardeFlag1 = false;
            rewardeFlag = false;
    
            afterAdPanel.SetActive(true);
            GameManager.instance.LoadCoinGoukei();
            GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
            GameManager.instance.totalCoin += 100;
            //Debug.Log("リワードcoinGet" + GameManager.instance.totalCoin + "枚");
            GameManager.instance.SaveCoinGoukei();
            //SpinnerPanel.SetActive(false);

            afterAdPanel.GetComponent<DOafterRewardPanel>().AfterReward();
            SpinnerPanel.SetActive(false);
            //Debug.Log("リワード報酬後SpinPanel," + SpinnerPanel.activeSelf);
            //報酬を得ないでクローズする場合
        }
        else if (rewardeFlag1 == true && rewardeFlag == false)
        {
            rewardeFlag1 = false;
            afterAdPanel.SetActive(false);
            //Debug.Log("報酬なしクローズafterAdPanel," + afterAdPanel.activeSelf);

        }

        if (OpenRewardFlag == true)
        {

            OpenRewardFlag = false;
  
            //Debug.Log("リワードOpenRewardFlag" + OpenRewardFlag);
        }

    }*/
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
        var adRequest = new AdRequest.Builder().Build();

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

              rewardedAd = ad;

              ad.OnAdPaid += (AdValue adValue) =>
    {
        Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
            adValue.Value,
            adValue.CurrencyCode));
    };
    // Raised when a click is recorded for an ad.
    ad.OnAdClicked += () =>
    {
        rewardeFlag = true;
        //Debug.Log("リワードrewardFlag" + rewardeFlag);
        //Debug.Log("Rewarded ad was clicked.");
    };
    ad.OnAdImpressionRecorded += () =>
    {
        rewardeFlag = true;
        Debug.Log("広告インプレッションが記録されました。");
    };
    // Raised when an ad opened full screen content.
    ad.OnAdFullScreenContentOpened += () =>
    {
        SpinnerFlag = true;
        OpenRewardFlag = true;
        //Debug.Log("リワードOpenRewardFlag" + OpenRewardFlag);
        //Debug.Log("リワードSpinner" + SpinnerFlag);
    };
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        rewardeFlag1 = true;
        rewardedAd.Destroy();
        CreateAndLoadRewardedAd();
        
    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        Debug.LogError("広告表示中にエラーが発生しました: " + error);
        onAdFailed?.Invoke(); // 失敗した場合にSpinnerを非表示にする処理
        CreateAndLoadRewardedAd();
    };
          });
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
        //Debug.Log("リワード広告読み込み未完了");
            NoShowFlag = true;
    }
     
    }
  
    // ------------------------------
    // コールバックを登録  メソッドはAdRewardManager.csで準備
    // ------------------------------
    public void SetOnAdOpenedCallback(Action callback)
    {
        onAdOpened = callback;
    }
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


