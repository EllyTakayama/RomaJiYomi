using UnityEngine;
using GoogleMobileAds.Api;
using System;
using DG.Tweening;
using UnityEngine.UI;
//5月19日更新

public class AdMobReward : MonoBehaviour
{
    //やること
    //1.リワード広告IDの入力
    //2.Update関数のif文内に報酬内容を入力
    //3.リワード起動設定　ShowAdMobReward()を使う


    private bool rewardeFlag=false;//リワード広告の報酬付与用　初期値はfalse
    private bool rewardeFlag1=false;//リワード広告の報酬付与用　初期値はfalse
    private bool SpinnerFlag =false;//Spinnerパネル表示よう　初期値はfalse
    private bool OpenRewardFlag=false;//リワード広告全面表示　初期値はfalse

    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る

    private string adUnitId;
    public GameObject afterAdPanel;
    public GameObject SpinnerPanel;

    private void Start()
    {      
        //AndroidとiOSで広告IDが違うのでプラットフォームで処理を分けます。
        // 参考
        //【Unity】AndroidとiOSで処理を分ける方法
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
        //adUnitId = "ca-app-pub-7439888210247528/2150238155";//ここにAndroidのリワード広告IDを入力
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
        //adUnitId = "ca-app-pub-7439888210247528/5351116568";//ここにiOSのリワード広告IDを入力
#else
        adUnitId = "unexpected_platform";
#endif

        CreateAndLoadRewardedAd();//リワード広告読み込み
        Debug.Log("リワード広告スタート関数");
    }
    private void Update()
    {
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        //報酬をゲットしてリワードをクローズする場合
        if(rewardeFlag1 == true && rewardeFlag == true){
            rewardeFlag1 = false;
            rewardeFlag = false;
            afterAdPanel.SetActive(true);
            GameManager.instance.LoadCoinGoukei();
            GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
            GameManager.instance.totalCoin += 100;
            Debug.Log("リワードcoinGet"+GameManager.instance.totalCoin+"枚");
            GameManager.instance.SaveCoinGoukei();
            //SpinnerPanel.SetActive(false);
            
            afterAdPanel.GetComponent<DOafterRewardPanel>().AfterReward();
            SpinnerPanel.SetActive(false);
            Debug.Log("リワード報酬後SpinPanel,"+SpinnerPanel.activeSelf);
            /*
            if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.UnmuteBGM();
            Debug.Log("リワード報酬後BGMミュート解除");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.UnmuteSE();
            Debug.Log("リワード報酬後SEミュート解除");
        }*/
            //報酬を得ないでクローズする場合
        }else if(rewardeFlag1 == true && rewardeFlag == false){
            rewardeFlag1 = false;
            Debug.Log("報酬なしリワードBGM"+GameManager.instance.isBgmOn);
            Debug.Log("報酬なしリワードSE"+GameManager.instance.isSEOn);
            afterAdPanel.SetActive(false);
            /*
        if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.UnmuteBGM();
            Debug.Log("報酬なしリワードBGMミュート解除");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.UnmuteSE();
            Debug.Log("報酬なしリワードSEミュート解除");
        }*/
        }
        /*
        if( SpinnerFlag == true){
            SpinnerFlag = false;
            SpinnerPanel.SetActive(true);
            Debug.Log("リワードSpinner"+SpinnerFlag);
        }
        if( OpenRewardFlag == true){
            Debug.Log("リワードOpenRewardFlag"+OpenRewardFlag);
            OpenRewardFlag = false;
            if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.BGMmute();
            Debug.Log("OpenリワードBGM一時ミュート");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.SEmute();
            Debug.Log("OpenリワードSE一時ミュート");
        }
        }*/

    }

    //リワード広告読み込む関数
    public void CreateAndLoadRewardedAd()
    {
        //リワード広告初期化
        rewardedAd = new RewardedAd(adUnitId);
        Debug.Log("リワード広告初期化");
        //RewardedAd型の変数 rewardedAdの各種状態 に関数を登録
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;//rewardedAdの状態が リワード広告読み込み完了 となった時に起動する関数(関数名HandleRewardedAdLoaded)を登録
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;//rewardedAdの状態が　リワード広告読み込み失敗 　となった時に起動する関数(関数名HandleRewardedAdFailedToLoad)を登録
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;//rewardedAdの状態が  リワード広告閉じられた　となった時に起動する関数(関数名HandleRewardedAdFailedToLoad)を登録
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;//rewardedAdの状態が ユーザーの報酬処理　となった時に起動する関数(関数名HandleUserEarnedReward)を登録
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        //リクエストを生成
        AdRequest request = new AdRequest.Builder().Build();
        //リクエストと共にリワード広告をロード
        rewardedAd.LoadAd(request);
    }
    //リワード広告を表示する関数
    //ボタンに割付けして使用
    public void ShowAdMobReward()
    {
        //広告の読み込みが完了していたら広告表示
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            Debug.Log("リワード広告表示");
        }
        else
        {
            Debug.Log("リワード広告読み込み未完了");
        }
    }
  

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
        Debug.Log("リワードOpenRewardFlag"+OpenRewardFlag);
        Debug.Log("リワードSpinner"+SpinnerFlag);

    }

    //リワード広告閉じられた時に起動する関数
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
         rewardeFlag1 = true;
         Debug.Log("リワードrewardFlag1"+rewardeFlag1);
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
        Debug.Log("リワードrewardFlag"+rewardeFlag);

    }
    //リワード読み込み完了 となった時に起動する関数
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("リワード広告読み込み完了");
    }

}
