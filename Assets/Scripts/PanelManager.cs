using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;//DoTweenを使用する記述


public class PanelManager : MonoBehaviour
{
    //public GameObject Panel0;
    [SerializeField] GameObject AdMobManager;
    [SerializeField] AdMobBanner pAdMobBanner;
    [SerializeField] AdMobInterstitial pAdInterstitial;
    [SerializeField] AdMobReward pAdReward;
    [SerializeField] private GameObject SpinnerPanel;
    GameManager PanelGameManager => GameManager.instance;
    
    /// <summary>
    /// スピナーパネルを一度表示してから広告を出す
    /// </summary>
    public void ShowInterstitialWithSpinner()
    {
        StartCoroutine(ShowSpinnerThenAd());
    }

    private IEnumerator ShowSpinnerThenAd()
    {
        if (SpinnerPanel != null)
        {
            yield return new WaitForEndOfFrame();
            SpinnerPanel.SetActive(true);
        }
        pAdInterstitial.ShowAdMobInterstitial(OnInterstitialClosed);
    }

    private void OnInterstitialClosed()
    {
        Debug.Log("PanelManager: インタースティシャル広告が閉じられました");
    }

    public void LoadSceneWithSpinner(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        SpinnerPanel.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        SpinnerPanel.SetActive(false);
        asyncLoad.allowSceneActivation = true;
    }

    //各Sceneへ移動する際に2回に一度インタースティシャル広告を呼び出し
    public void TopSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayBGM("TopScene");
        GameManager.instance.SceneCount++;
        PanelGameManager.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        //int RewardCount =IScount%6;
        //Debug.Log("RewardCount,"+RewardCount);
        pAdInterstitial.AdSceneName = "TopScene";
        pAdMobBanner.DestroyBannerAd();
        pAdReward.DestroyRewardAd();
        
        if(IScount>0 && IScount%2 ==0){
            if (!GameManager.instance.isInterstitialAdsRemoved)
            {
                pAdInterstitial.ShowAdMobInterstitial(() =>
                {
                    // 広告終了後、スピナー付きでシーン読み込み
                    LoadSceneWithSpinner("TopScene");
                });
                return;
            }
        }
        SoundManager.instance.PlaySousaSE(2);
        pAdInterstitial.DestroyInterstitialAd();
        LoadSceneWithSpinner("TopScene");
    }

    public void KihonSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayBGM("KihonScene");
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

        if (IScount > 0 && IScount % 1 == 0)
        {
            if (!GameManager.instance.isInterstitialAdsRemoved)
            {
                pAdInterstitial.ShowAdMobInterstitial(() =>
                {
                    // 広告終了後、スピナー付きでシーン読み込み
                    LoadSceneWithSpinner("KihonScene");
                });
                return;
            }
        }

        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlaySousaSE(2);  
        LoadSceneWithSpinner("KihonScene");
            }

    public void RenshuuSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayBGM("RenshuuScene");
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
        
        if(IScount>0 && IScount%2 ==0){
            if (!GameManager.instance.isInterstitialAdsRemoved)
            {
                pAdInterstitial.ShowAdMobInterstitial();
                return;
            }
            }
        pAdInterstitial.DestroyInterstitialAd();   
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
        
    }
    public void TikaraSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayPanelBGM("SelectPanel");
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

        if(IScount>0 && IScount%2 ==0){
            if (!GameManager.instance.isInterstitialAdsRemoved)
            {
                pAdInterstitial.ShowAdMobInterstitial();
                return;
            }
        }
        SoundManager.instance.PlaySousaSE(2);
        pAdInterstitial.DestroyInterstitialAd();   
        SceneManager.LoadScene("TikaraScene");
       
    }
    public void GachaSceneMove(){
        SoundManager.instance.StopSE();
        //DOTween.KillAll();
        SoundManager.instance.PlayBGM("GachaScene");
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

        if(IScount>0 && IScount%2 ==0){
            if (!GameManager.instance.isInterstitialAdsRemoved)
            {
                pAdInterstitial.ShowAdMobInterstitial();
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
        SoundManager.instance.PlayPanelBGM("SelectPanel");
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
