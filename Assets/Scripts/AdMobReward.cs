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
    private bool rewardeFlag = false;//リワード広告の報酬付与用　初期値はfalse
    private bool rewardeFlag1 = false;//リワード広告の報酬付与用　初期値はfalse
    private bool SpinnerFlag = false;//Spinnerパネル表示用　初期値はfalse
    private bool OpenRewardFlag = false;//リワード広告全面表示　初期値はfalse
    private bool NoShowFlag = false;//リワード広告が読み込めていなかった場合　初期値はfalse

    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る

    private string adUnitId;
    public GameObject afterAdPanel;
    public GameObject SpinnerPanel;
    public Text rewardText;//広告読み込めなかった時にテキスト差し替え

    private void Start()
    {

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
        //adUnitId = "ca-app-pub-7439888210247528/2150238155";//ここにAndroidのリワード広告IDを入力
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
        //adUnitId = "ca-app-pub-7439888210247528/5351116568";//ここにiOSのリワード広告IDを入力
#else
        adUnitId = "unexpected_platform";
#endif

        string SceneName = SceneManager.GetActiveScene().name;
        //CreateAndLoadRewardedAd();//リワード広告読み込み
        MobileAds.SetiOSAppPauseOnBackground(true);
        
    }
    private void Update()
    {
        //広告がダウンロード失敗して表示されない場合
        if (NoShowFlag == true)
        {
            NoShowFlag = false;
            rewardText.text = "広告がダウンロード\nできませんでした";
        }
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        //報酬をゲットしてリワードをクローズする場合
        if (rewardeFlag1 == true && rewardeFlag == true)
        {
            rewardeFlag1 = false;
            rewardeFlag = false;
            /*
            #if UNITY_IPHONE
            MobileAds.SetiOSAppPauseOnBackground(false);
            
        if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.UnmuteBGM();
            Debug.Log("インタースティシャルBGMミュート解除");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.UnmuteSE();
            Debug.Log("インタースティシャルSEミュート解除");
        }
        #endif
        */

            afterAdPanel.SetActive(true);
            GameManager.instance.LoadCoinGoukei();
            GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
            GameManager.instance.totalCoin += 100;
            Debug.Log("リワードcoinGet" + GameManager.instance.totalCoin + "枚");
            GameManager.instance.SaveCoinGoukei();
            //SpinnerPanel.SetActive(false);

            afterAdPanel.GetComponent<DOafterRewardPanel>().AfterReward();
            SpinnerPanel.SetActive(false);
            Debug.Log("リワード報酬後SpinPanel," + SpinnerPanel.activeSelf);

            //報酬を得ないでクローズする場合
        }
        else if (rewardeFlag1 == true && rewardeFlag == false)
        {
            rewardeFlag1 = false;
            /*
            #if UNITY_IPHONE
            MobileAds.SetiOSAppPauseOnBackground(false);
            if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.UnmuteBGM();
            Debug.Log("インタースティシャルBGMミュート解除");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.UnmuteSE();
            Debug.Log("インタースティシャルSEミュート解除");
        }
            #endif
            */
            Debug.Log("報酬なしクローズリワードBGM" + GameManager.instance.isBgmOn);
            Debug.Log("報酬なしクローズリワードSE" + GameManager.instance.isSEOn);
            afterAdPanel.SetActive(false);
            Debug.Log("報酬なしクローズafterAdPanel," + afterAdPanel.activeSelf);

        }

        if (OpenRewardFlag == true)
        {

            OpenRewardFlag = false;
            /*
            #if UNITY_IPHONE
            MobileAds.SetiOSAppPauseOnBackground(true);
            if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.BGMmute();
            Debug.Log("インタースティシャルBGM一時ミュート");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.SEmute();
            Debug.Log("インタースティシャルSE一時ミュート");
        }
            #endif
            */
            Debug.Log("リワードOpenRewardFlag" + OpenRewardFlag);
        }

    }

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
                  Debug.LogError("Rewarded読み込み失敗 " +
                                 "with error : " + error);
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
        Debug.Log("リワードrewardFlag" + rewardeFlag);
        Debug.Log("Rewarded ad was clicked.");
    };
    ad.OnAdImpressionRecorded += () =>
    {
        rewardeFlag = true;
        Debug.Log("リワードrewardFlag" + rewardeFlag);
        Debug.Log("Rewarded ad recorded an impression.");
    };
    // Raised when an ad opened full screen content.
    ad.OnAdFullScreenContentOpened += () =>
    {
        SpinnerFlag = true;
        OpenRewardFlag = true;
        Debug.Log("リワードOpenRewardFlag" + OpenRewardFlag);
        Debug.Log("リワードSpinner" + SpinnerFlag);

        Debug.Log("Rewarded ad full screen content opened.");
    };
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        rewardeFlag1 = true;
        rewardedAd.Destroy();
        Debug.Log("Rewarded ad full screen content closed.");

    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        Debug.LogError("Rewarded ad failed to open full screen content " +
                       "with error : " + error);
        Debug.Log("読み込みエラー");
    };

          });
    }
    //リワード広告を表示する関数
    //ボタンに割付けして使用
    public void ShowAdMobReward()
    {
        const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    if (rewardedAd != null && rewardedAd.CanShowAd())
    {
        rewardedAd.Show((Reward reward) =>
        {
            // TODO: Reward the user.
            Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
        });
    }
    else{
        Debug.Log("リワード広告読み込み未完了");
            NoShowFlag = true;
    }
     
    }
    /*
    //リワード読み込み失敗 となった時に起動する関数
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("リワード広告読み込み失敗" + args.LoadAdError);//args.LoadAdError:エラー内容 
    }
    //リワード広告が始まったときに起動する関数
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        SpinnerFlag = true;
        OpenRewardFlag = true;
        Debug.Log("リワードOpenRewardFlag" + OpenRewardFlag);
        Debug.Log("リワードSpinner" + SpinnerFlag);

    }

    //リワード広告閉じられた時に起動する関数
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        rewardeFlag1 = true;
        Debug.Log("リワードrewardFlag1" + rewardeFlag1);
        Debug.Log("リワード広告閉じられる");
        //広告再読み込み
        CreateAndLoadRewardedAd();
    }

    //ユーザーの報酬処理 となった時に起動する関数
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("リワード報酬受け取り");

        //この関数内ではゲームオブジェクトの操作ができない
        //そのため、ここでは報酬受け取りのフラグをtrueにするだけにする
        //具体的な処理はUpdate関数内で行う。
        rewardeFlag = true;
        Debug.Log("リワードrewardFlag" + rewardeFlag);

    }
    //リワード読み込み完了 となった時に起動する関数
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("リワード広告読み込み完了");
    }
    */
}


