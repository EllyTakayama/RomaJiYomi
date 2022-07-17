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
    public string KihonScene;
    public string RenshuuScene;
    public string TikaraScene;
    public string GachaScene;
    private bool rewardeFlag=false;//リワード広告の報酬付与用　初期値はfalse
    private bool SpinnerFlag =false;//Spinnerパネル表示よう　初期値はfalse
    private bool OpenInterAdFlag=false;//リワード広告全面表示　初期値はfalse
    private bool CloseInterAdFlag=false;//リワード広告全面表示　初期値はfalse
    public GameObject AdMobManager;//各SceneのアドモブManager
    public GameObject SpinnerPanel;//シーン移動の間を持たせるようのPanel
    private InterstitialAd interstitial;//InterstitialAd型の変数interstitialを宣言　この中にインタースティシャル広告の情報が入る

    private void Start()
    {
        string SceneName =SceneManager.GetActiveScene().name;
        print("シーン名"+SceneName);
        RequestInterstitial();//読み込み
        
        Debug.Log(SceneName+",インタースティシャル読み込み開始");
    }
    private void Update()
    {
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        if(rewardeFlag == true){
            Debug.Log("インタースティシャルBGM"+GameManager.instance.isBgmOn);
            Debug.Log("インタースティシャルSE"+GameManager.instance.isSEOn);
            rewardeFlag = false;
            Debug.Log("rewardFlag"+rewardeFlag);
            if(name == "Home"){
            SceneManager.LoadScene("TopScene");
            Debug.Log("Home,TopScene");
            Debug.Log("Inter,TopScene");
            }else if(name == "KihonScene"){
            SceneManager.LoadScene("KihonScene");
            Debug.Log("Inter,KihonScene");
            }
            else if(name == "RenshuuScene"){
            SceneManager.LoadScene("RenshuuScene"); 
            Debug.Log("Inter,RenshuuScene");   
            }
            else if(name == "TikaraScene"){
                SceneManager.LoadScene("TikaraScene");  
                Debug.Log("Inter,TikaraScene");     
            }else if(name == "GachaScene"){
                SceneManager.LoadScene("GachaScene"); 
                Debug.Log("Inter,GachaScene");        
            }
            else {
                SceneManager.LoadScene("TopScene");
            }
        if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.UnmuteBGM();
            Debug.Log("インタースティシャルBGMミュート解除");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.UnmuteSE();
            Debug.Log("インタースティシャルSEミュート解除");
        }
        SpinnerPanel.SetActive(false);
           }
        /*
        if( SpinnerFlag == true){
            SpinnerFlag = false;
            SpinnerPanel.SetActive(true);
            Debug.Log("インタースティシャルOpenSpinner"+SpinnerFlag);
        }
        
        if(OpenInterAdFlag== true){
            Debug.Log("インタースティシャルOpenInterAdFlag"+OpenInterAdFlag);
            OpenInterAdFlag= false;
            if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.BGMmute();
            Debug.Log("インタースティシャルBGM一時ミュート");
        }
        if(GameManager.instance.isSEOn == true){
            SoundManager.instance.SEmute();
            Debug.Log("インタースティシャルSE一時ミュート");
        }
        }*/
    }

    //インタースティシャル広告を表示する関数
    //ボタンなどに割付けして使用
    public void ShowAdMobInterstitial()
    {
        //広告の読み込みが完了していたら広告表示
        if (interstitial.IsLoaded() == true)
        {
            interstitial.Show();
            Debug.Log("インタースティシャル広告表示");
        }
        else
        {
            if(name == Home){
            SceneManager.LoadScene("TopScene");
            Debug.Log("Home,TopScene");
            }else if(name == KihonScene){
            SceneManager.LoadScene("KihonScene");
            Debug.Log("KihonScene");
            }
            else if(name == RenshuuScene){
            SceneManager.LoadScene("RenshuuScene"); 
            Debug.Log("RenshuuScene");   
            }
            else if(name == TikaraScene){
                SceneManager.LoadScene("TikaraScene"); 
                Debug.Log("TikaraScene");      
            }
            else if(name == GachaScene) {
            SceneManager.LoadScene("GachaScene"); 
             Debug.Log("GachaScene");       
            }else{
                SceneManager.LoadScene("TopScene");
               
            }
            Debug.Log("インタースティシャル広告読み込み未完了");

        }
    }


    //インタースティシャル広告を読み込む関数
    public void RequestInterstitial()
    {
        /*
        //AndroidとiOSで広告IDが違うのでプラットフォームで処理を分けます。
        // 参考
        //【Unity】AndroidとiOSで処理を分ける方法
        //iOS では、InterstitialAd オブジェクトは使い捨てオブジェクトです。
        つまり、インタースティシャル広告を一度表示すると、同じ InterstitialAd オブジェクトを使って他の広告を読み込むことはできません。
        別のインタースティシャルをリクエストするには、新しい InterstitialAd オブジェクトを作成する必要があります。
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru
        */

#if UNITY_ANDROID
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";//TestAndroidのインタースティシャル広告ID
        string adUnitId = "ca-app-pub-7439888210247528/6016496823";//ここにAndroidのインタースティシャル広告IDを入力

#elif UNITY_IPHONE
        //string adUnitId = "ca-app-pub-3940256099942544/4411468910";//TestiOSのインタースティシャル広告ID
        string adUnitId = "ca-app-pub-7439888210247528/6549466402";//ここにiOSのインタースティシャル広告IDを入力

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
     //インタースティシャル全面表示になった時に起動する関数
    public void HandleOnAdOpening(object sender, EventArgs args){
        
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        OpenInterAdFlag = true;
        SpinnerFlag = true;
        Debug.Log("全面インタースティシャルSpinner"+SpinnerFlag);
    }
    public void HomeOnClick(string Button){
        name = Button;
        print("インタースティシャル名前,"+name);
    }
    //インタースティシャル表示終了 となった時に起動する関数
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("インタースティシャル終了name"+name);
        rewardeFlag = true;
        
        Debug.Log("インタースティシャル広告終了");

        //インタースティシャル広告は使い捨てなので一旦破棄
        interstitial.Destroy();
        Debug.Log("インタースティシャル広告破棄");

        //インタースティシャル再読み込み開始
        RequestInterstitial();
        Debug.Log("インタースティシャル広告再読み込み");
    }
}
