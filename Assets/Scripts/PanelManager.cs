using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;//DoTweenを使用する記述
using System;//Actionのため

public class PanelManager : MonoBehaviour
{
    //public GameObject Panel0;
    [SerializeField] GameObject AdMobManager;
    [SerializeField] AdMobBanner pAdMobBanner;
    [SerializeField] AdMobInterstitial pAdInterstitial;
    [SerializeField] AdMobReward pAdReward;
    [SerializeField] private GameObject SpinnerPanel;
    GameManager PanelGameManager => GameManager.instance;
    private const int InterstitialAdInterval = 2;//インタースティシャル広告の表示間隔は2回に1回
    private bool isLoadingScene = false;//重複してシーンの読み込みを実行しないためのフラグ
    private bool isShowingAd = false;    //広告表示中かを管理するフラグ
    /// <summary>
    /// スピナーパネルを一度表示してから広告を出す
    /// </summary>
    private void OnInterstitialClosed()
    {
        Debug.Log("PanelManager: インタースティシャル広告が閉じられました");
    }
    
    // スピナーを事前に表示してから広告を見せ、その後の処理を行うコルーチン20250425
    private IEnumerator ShowInterstitialWithSpinner(Action afterAdAction)
    {
        if (SpinnerPanel != null)
        {
            SpinnerPanel.SetActive(true); // Spinnerを表示
            yield return new WaitForSeconds(0.2f); // 少し待つことで見た目上スムーズに
        }
        isShowingAd = true; // ★広告表示中フラグを立てる
        
        // 広告を表示し、閉じたら後処理を呼ぶ
        pAdInterstitial.ShowAdMobInterstitial(() =>
        {
            isShowingAd = false; // ★広告閉じたらフラグを下ろす
            afterAdAction?.Invoke();
        });

    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoadSceneAsync(string sceneName, bool showSpinner)
    {
        if (isLoadingScene)
        {
            yield break; // すでにロード中の場合は処理を中断
        }
        isLoadingScene = true; // ロード開始
        
        // ★広告中なら広告が終わるまで待つ
        while (isShowingAd)
        {
            yield return null;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);

        //SpinnerPanel.SetActive(false);
        asyncLoad.allowSceneActivation = true;
        // シーン遷移完了まで待機
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // 🎵 シーン遷移後にBGMを切り替える！
        SoundManager.instance.PlayBGM(sceneName);
    }

    //各Sceneへ移動する際に2回に一度インタースティシャル広告を呼び出し
    public void TopSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        
        GameManager.instance.SceneCount++;
        PanelGameManager.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        //int RewardCount =IScount%6;
        //Debug.Log("RewardCount,"+RewardCount);
        pAdInterstitial.AdSceneName = "TopScene";
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        
        if(IScount>0 && IScount % InterstitialAdInterval ==0){
            if (!GameManager.instance.AreAdsRemoved())
            {
                StartCoroutine(ShowInterstitialWithSpinner(() =>
                {
                    // 広告終了後、シーン遷移
                    StartCoroutine(LoadSceneAsync("TopScene",true)); // ← 非同期ロードで改善
                }));
                return;
            }
        }
        SoundManager.instance.PlaySousaSE(2);
        pAdInterstitial.DestroyInterstitialAd();
        StartCoroutine(LoadSceneAsync("TopScene", false));
        //SoundManager.instance.PlayBGM("TopScene");
        //SceneManager.LoadScene("TopScene");
    }

    public void KihonSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        //SoundManager.instance.PlayBGM("KihonScene");
        //.GetComponent<AdMobInterstitial>().name = "KihonScene";
        //string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        //int RewardCount =IScount%6;
        //Debug.Log("RewardCount,"+RewardCount);
        pAdInterstitial.AdSceneName = "KihonScene";
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();

        if (IScount > 0 && IScount % InterstitialAdInterval == 0)
        {
            if (!GameManager.instance.AreAdsRemoved())
            {
                StartCoroutine(ShowInterstitialWithSpinner(() =>
                {
                    // 広告終了後、シーン遷移
                    StartCoroutine(LoadSceneAsync("KihonScene",true));
                }));
                return;
            }
        }
        pAdInterstitial.DestroyInterstitialAd();   
        StartCoroutine(LoadSceneAsync("KihonScene",false));
        //SoundManager.instance.PlaySousaSE(2);  
        //SceneManager.LoadScene("KihonScene");
      
            }

    public void RenshuuSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        //SoundManager.instance.PlayBGM("RenshuuScene");
        //AdMobManager.GetComponent<AdMobInterstitial>().name = "RenshuuScene";
        //string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        //int RewardCount =IScount%6;
        //Debug.Log("RewardCount,"+RewardCount);
        pAdInterstitial.AdSceneName = "RenshuuScene";
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        
        if(IScount>0 && IScount % InterstitialAdInterval ==0){
            if (!GameManager.instance.AreAdsRemoved())
            {
                StartCoroutine(ShowInterstitialWithSpinner(() =>
                {
                    // 広告終了後、シーン遷移
                    StartCoroutine(LoadSceneAsync("RenshuuScene",true));
                    //SceneManager.LoadScene("RenshuuScene");
                }));
            }
        }
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlaySousaSE(2);
        //SceneManager.LoadScene("RenshuuScene");
        StartCoroutine(LoadSceneAsync("RenshuuScene",false));
        
    }
    public void TikaraSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        //SoundManager.instance.PlayBGM("TikaraPanel");
        //AdMobManager.GetComponent<AdMobInterstitial>().name = "TikaraScene";
        //string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        //int RewardCount =IScount%6;
        //Debug.Log("RewardCount,"+RewardCount);
        pAdMobBanner.DestroyBannerAd();
        pAdInterstitial.AdSceneName = "TikaraScene";
        pAdReward.DestroyRewardAd();

        if(IScount>0 && IScount% InterstitialAdInterval ==0){
            if (!GameManager.instance.AreAdsRemoved())
            {
                StartCoroutine(ShowInterstitialWithSpinner(() =>
                {
                    // 広告終了後、シーン遷移
                    StartCoroutine(LoadSceneAsync("TikaraScene",true));
                    //SceneManager.LoadScene("TikaraScene");
                }));
                return;
            }
        }
        SoundManager.instance.PlaySousaSE(2);
        pAdInterstitial.DestroyInterstitialAd(); 
        StartCoroutine(LoadSceneAsync("TikaraScene",false));
        //SceneManager.LoadScene("TikaraScene");
    }
    public void GachaSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        //SoundManager.instance.PlayBGM("GachaScene");
        //AdMobManager.GetComponent<AdMobInterstitial>().name = "GachaScene";
        //string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        //int RewardCount =IScount%6;
        //Debug.Log("RewardCount,"+RewardCount);
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        pAdInterstitial.AdSceneName = "GachaScene";

        if(IScount>0 && IScount % InterstitialAdInterval==0){
            if (!GameManager.instance.AreAdsRemoved())
            {
                StartCoroutine(ShowInterstitialWithSpinner(() =>
                {
                    // 広告終了後、シーン遷移
                    SceneManager.LoadScene("GachaScene");
                }));
                return;
            }
        }
        SoundManager.instance.PlaySousaSE(2);
        pAdInterstitial.DestroyInterstitialAd();   
        SceneManager.LoadScene("GachaScene");
    }

    public void SettingSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        /*
        if(IScount>0 && IScount%1 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }*/
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("SettingScene");
    }
    
    public void TopSettingMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        
        SceneManager.LoadScene("TopScene");
        //Panel0.SetActive(false);
    }

    public void SetPanelMove(){
        SoundManager.instance.StopSE();
        //Panel0.SetActive(false);
        
    }
    public void TopPanelMove(){
        //Panel0.SetActive(true);
    }

    //初期画面からの移動は広告にはカウントしない
    public void TopSceneMoveTitle(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayBGM("TopScene");
        //GameManager.instance.SceneCount++;
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
    }
    public void KihonSceneMoveTitle(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayBGM("KihonScene");
        //GameManager.instance.SceneCount++;
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlaySousaSE(2); 
        SceneManager.LoadScene("KihonScene");
            }
    public void RenshuuSceneMoveTitle(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlayBGM("RenshuuScene");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
        
    }
    public void TikaraSceneMoveTitle(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlayBGM("TikaraScene");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TikaraScene");
       
    }
    public void GachaSceneMoveTitle(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlayBGM("GachaScene");
        //GameManager.instance.SceneCount++;
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("GachaScene");
    }
}
