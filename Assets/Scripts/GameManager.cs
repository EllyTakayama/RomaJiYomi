using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Google.Play.Review;
#endif
//6月7日更新

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public bool isGfontsize;//Setting画面での大文字小文字のbool
   public bool isGKunrei;
   //public bool isGfontSize;
   public bool isSEOn;//SEオンオフ
   public bool isBgmOn;//BGMオンオフ
   public int AcorrectCount;//kihonSceneのあ行スコア
   public int HcorrectCount;//kihonSceneのほかの行のスコア
   public int TiTangoCount;//TikaraSceneの単語ごと解答のスコア
   public int TyHiraganaCount;//TikaraSceneの1文字ずつ解答のスコア
   public int RcorrectCount;//RenshuuSceneスコア
   public int AcorrectTotal;//kihonSceneのあ行スコア保存
   public int HcorrectTotal;//kihonSceneのほかの行のスコア保存
   public int TiTangoTotal;//TikaraSceneの単語ごと解答のスコア保存
   public int TyHiraganaTotal;//TikaraSceneの1文字ずつ解答のスコア保存
   public int RcorrectTotal;//RenshuuSceneスコア保存
   public bool isTiWord;//TikaraSceneの解答分岐のbool
   public int RCoin;//RenshuuSceneの正解数に応じたコイン枚数
   public int TiCoin;//TikaraSceneの単語問題の正解数に応じたコイン枚数
   public int TyCoin;//TikaraSceneの1文字問題の正解数に応じたコイン枚数
   public int beforeTotalCoin;//追加する前のコイン枚数　DOTweenのDOCountに使用
   public int totalCoin;//各Sceneのコイン枚数はこちらに追加していく
   public List<int> RoulletteNum = new List<int>();//ルーレットの風船の変数の保持
   public int SceneCount;//インタースティシャル広告表示のためにScene表示をカウントしていきます
   
   public AdMobBanner adMobBanner; // AdMobBannerスクリプトをアタッチする
   public AdMobInterstitial adMobInterstitial; // AdMobInterstitialスクリプトをアタッチする
   // 課金状態フラグ（広告のサブスク状態を管理）
   public bool IsBannerAdsRemoved
   {
       get => LoadPurchaseState("isBannerAdsRemoved");
       set
       {
           SavePurchaseState("isBannerAdsRemoved", value);
           UpdateAdState();
       }
   }

   public bool IsInterstitialAdsRemoved
   {
       get => LoadPurchaseState("isInterstitialAdsRemoved");
       set
       {
           SavePurchaseState("isInterstitialAdsRemoved", value);
           UpdateAdState();
       }
   }

   public bool IsPermanentAdsRemoved
   {
       get => LoadPurchaseState("isPermanentAdsRemoved");
       set
       {
           SavePurchaseState("isPermanentAdsRemoved", value);
           UpdateAdState();
       }
   }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    //--シングルトン終わり--

    // Start is called before the first frame update
    void Start()
    {
       //LoadGfontsize();
       //LoadGKunrei();
       //LoadGse();
       //LoadGbgm();
       //Debug.Log("start");
       //SceneCount = 0;
       LoadSceneCount();
       //RequestReview();
       //Debug.Log("Sceneカウント"+SceneCount);
       // 課金データを読み込み、広告を非表示に設定
// 課金状態を読み込み、広告を非表示に設定
       if (LoadPurchaseState("isBannerAdsRemoved"))
       {
           adMobBanner?.OnBannerPurchaseCompleted();
       }

       if (LoadPurchaseState("isInterstitialAdsRemoved"))
       {
           adMobInterstitial?.OnInterstitialPurchaseCompleted();
       }

       if (LoadPurchaseState("isPermanentAdsRemoved"))
       {
           // 永久的な広告非表示を設定（全ての広告を非表示）
           adMobBanner?.OnBannerPurchaseCompleted();
           adMobInterstitial?.OnInterstitialPurchaseCompleted();
       }

       Debug.Log("Sceneカウント" + SceneCount);
    }
    
    public void SavePurchaseState(string key, bool value)
    {
        ES3.Save<bool>(key, value, $"{key}.es3");
        Debug.Log($"課金データ保存: {key} = {value}");
    }

    public bool LoadPurchaseState(string key, bool defaultValue = false)
    {
        string filePath = $"{key}.es3"; // 読み込み元のファイル名
        if (ES3.KeyExists(key,filePath))
        {
            bool value = ES3.Load<bool>(key, $"{key}.es3", defaultValue);
            Debug.Log($"課金データ読み込み: {key} = {value}");
            return value;
        }
        else
        {
            Debug.Log($"課金データが存在しない: {key}, デフォルト値: {defaultValue}");
            return defaultValue;
        }
    }
    // 広告状態を更新（広告非表示の処理）
    private void UpdateAdState()
    {
        // 永久広告削除フラグが有効な場合は全ての広告を非表示にする
        if (IsPermanentAdsRemoved)
        {
            adMobBanner?.OnBannerPurchaseCompleted();
            adMobInterstitial?.OnInterstitialPurchaseCompleted();
        }
        else
        {
            if (!IsBannerAdsRemoved)
            {
                adMobBanner?.BannerStart();
            }
            if (!IsInterstitialAdsRemoved)
            {
                adMobInterstitial?.RequestInterstitial();
            }
        }
    }
    public void SaveSceneCount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("SceneCount",SceneCount,"SceneCount.es3" );
        //Debug.Log("セーブSceneCount"+SceneCount);
    }
    
    public void LoadSceneCount(){
         //if(ES3.KeyExists("isfontSize"))
         SceneCount = ES3.Load<int>("SceneCount","SceneCount.es3",0);
         //Debug.Log("ロードSceneCount"+SceneCount);
    }

    public void SaveCoinGoukei(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("totalCoin",totalCoin,"totalCoin.es3" );
        //Debug.Log("セーブtotalCoin"+totalCoin);
    }

    public void LoadCoinGoukei(){
         //if(ES3.KeyExists("isfontSize"))
         totalCoin = ES3.Load<int>("totalCoin","totalCoin.es3",0);
         //Debug.Log("ロードtotalCoin"+totalCoin);
    }
    
    public void SaveGfontsize(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<bool>("isGfontsize",isGfontsize,"isGfontsize.es3" );
        Debug.Log("クリックisGfontsize"+isGfontsize);
    }

    public void LoadGfontsize(){
         //if(ES3.KeyExists("isfontSize"))
         isGfontsize = ES3.Load<bool>("isGfontsize","isGfontsize.es3",true);
         //Debug.Log("クリックisGfontSize"+isGfontsize);
    }
    public void SaveGKunrei(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<bool>("isGKunrei",isGKunrei,"isGKunrei.es3");
        //Debug.Log("クリックisGKunrei"+isGKunrei);
    }
    public void LoadGKunrei(){
         //if(ES3.KeyExists("isGKunrei"))
         isGKunrei = ES3.Load<bool>("isGKunrei","isGKunrei.es3",true);
         //Debug.Log("クリックisGKunrei"+isGKunrei);
    }


    public void SaveGse(){
        ES3.Save<bool>("isSEOn",isSEOn,"isSEOn.es3");
        //Debug.Log("クリックisSEOn"+isSEOn);
    }

    public void LoadGse(){
         //if(ES3.KeyExists("isSEOn"))
         isSEOn = ES3.Load<bool>("isSEOn","isSEOn.es3",true);
         //Debug.Log("クリックisSEOn"+isSEOn);
    }

public void SaveGbgm(){
        ES3.Save<bool>("isBgmOn",isBgmOn,"isBgmOn.es3");
        //Debug.Log("クリックisBgmOn"+isBgmOn);
    }

    public void LoadGbgm(){
         //if(ES3.KeyExists("isBgmOn"))
         isBgmOn = ES3.Load<bool>("isBgmOn","isBgmOn.es3",true);
         //Debug.Log("クリックisBgmOn"+isBgmOn);
    }

    //KihonSceneあ行の正解数保存
    public void SaveACount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("Acorrect",AcorrectCount,"AcorrectCount.es3");
        //Debug.Log("クリックAcorrect"+AcorrectCount);
    }
    public void LoadACount(){
        AcorrectCount = ES3.Load<int>("Acorrect","AcorrectCount.es3",0);
        //Debug.Log("クリックAcorrect"+AcorrectCount);
    }
    //TikaraSceneTangoの累計正解数保存
    public void SaveTiCount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("TiTangoCount",TiTangoCount,"TiTangoCount.es3");
        Debug.Log("クリックTiTangoCount"+TiTangoCount);
    }

    public void LoadTiCount(){
        TiTangoCount = ES3.Load<int>("TiTangoCount","TiTangoCount.es3",0);
        //Debug.Log("クリックTiTangoCount"+TiTangoCount);
    }

    public void RequestReview()
    {
       
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
        //StartCoroutine(RequestReviewAndroid());
#elif UNITY_EDITOR
print("エディターレビュー呼び出し");
#else
        Debug.LogWarning("This platform is not support RequestReview.");
#endif
    Debug.Log("レビューリクエスト呼び出し");
    }
    /*

    private IEnumerator RequestReviewAndroid()
    {
        
        var reviewManager = new Google.Play.Review.ReviewManager();
        var requestFlowOperation = reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        
        if (requestFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        var playReviewInfo = requestFlowOperation.GetResult();
        var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
        yield return launchFlowOperation;
        playReviewInfo = null; // Reset the object
        
        if (launchFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
    }
    */

}
