using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdMobInterstitial : MonoBehaviour
{
    //やること
    //1.インタースティシャル広告IDの入力
    //2.インタースティシャル起動設定　ShowAdMobInterstitial()を使う
    //Button押した時にブール取得　Home false Return trueで行き先のシーンを取得する
    public string name;//ホームへ移動する
    public string Home;
    public string Return;
    public GameObject AdMobManager;//各SceneのアドモブManager
    private InterstitialAd interstitial;//InterstitialAd型の変数interstitialを宣言　この中にインタースティシャル広告の情報が入る

    private void Start()
    {
        string SceneName =SceneManager.GetActiveScene().name;
        print("シーン名"+SceneName);
        RequestInterstitial();
        Debug.Log("読み込み開始");
    }

    //インタースティシャル広告を表示する関数
    //ボタンなどに割付けして使用
    public void ShowAdMobInterstitial()
    {
        //広告の読み込みが完了していたら広告表示
        if (interstitial.IsLoaded() == true)
        {
            interstitial.Show();
            Debug.Log("広告表示");
        }
        else
        {
            if(name == Home){
            SceneManager.LoadScene("TopScene");
            Debug.Log("Home,TopScene");
            }else{
            string SceneName =SceneManager.GetActiveScene().name;
            print("シーン名"+SceneName);
            Debug.Log("Return,もどる");
            SceneManager.LoadScene(SceneName);
            }
            Debug.Log("広告読み込み未完了");

        }
    }


    //インタースティシャル広告を読み込む関数
    private void RequestInterstitial()
    {
        //AndroidとiOSで広告IDが違うのでプラットフォームで処理を分けます。
        // 参考
        //【Unity】AndroidとiOSで処理を分ける方法
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";//TestAndroidのインタースティシャル広告ID
        //string adUnitId = "ca-app-pub-7439888210247528/6016496823";//ここにAndroidのインタースティシャル広告IDを入力

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";//TestiOSのインタースティシャル広告ID
        //string adUnitId = "ca-app-pub-7439888210247528/6549466402";//ここにiOSのインタースティシャル広告IDを入力

#else
        string adUnitId = "unexpected_platform";
#endif

        //インタースティシャル広告初期化
        interstitial = new InterstitialAd(adUnitId);


        //InterstitialAd型の変数 interstitialの各種状態 に関数を登録
        interstitial.OnAdLoaded += HandleOnAdLoaded;//interstitialの状態が　インタースティシャル読み込み完了　となった時に起動する関数(関数名HandleOnAdLoaded)を登録
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;//interstitialの状態が　インタースティシャル読み込み失敗 　となった時に起動する関数(関数名HandleOnAdFailedToLoad)を登録
        interstitial.OnAdClosed += HandleOnAdClosed;//interstitialの状態が  インタースティシャル表示終了　となった時に起動する関数(HandleOnAdClosed)を登録
        interstitial.OnAdOpening += HandleOnAdOpening;//インタースティシャルが表示開始になった時に呼び出し

        //リクエストを生成
        AdRequest request = new AdRequest.Builder().Build();
        //インタースティシャルにリクエストをロード
        interstitial.LoadAd(request);
    }

    //インタースティシャル読み込み完了 となった時に起動する関数
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {        
        Debug.Log("インタースティシャル読み込み完了");
    }

    //インタースティシャル読み込み失敗 となった時に起動する関数
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("インタースティシャル読み込み失敗" + args.LoadAdError);//args.LoadAdError:エラー内容 
    }
    public void HandleOnAdOpening(object sender, EventArgs args){
        if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.BGMmute();
            Debug.Log("BGM一時ミュート");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.SEmute();
            Debug.Log("SE一時ミュート");
        }
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        
    }
    public void HomeOnClick(string Button){
        name = Button;
        print("インター名前,"+name);
    }
    //インタースティシャル表示終了 となった時に起動する関数
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        /*if(name == Home){
            SceneManager.LoadScene("TopScene");
            Debug.Log("Home,TopScene");
        }else{
            string SceneName =SceneManager.GetActiveScene().name;
            print("シーン名"+SceneName);
            Debug.Log("Return,もどる");
            SceneManager.LoadScene(SceneName);
        }*/
        if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.UnmuteBGM();
            Debug.Log("BGMミュート解除");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.UnmuteSE();
            Debug.Log("SEミュート解除");
        }
        Debug.Log("インタースティシャル広告終了");

        //インタースティシャル広告は使い捨てなので一旦破棄
        interstitial.Destroy();

        //インタースティシャル再読み込み開始
        RequestInterstitial();
        Debug.Log("インタースティシャル広告再読み込み");
    }
}
