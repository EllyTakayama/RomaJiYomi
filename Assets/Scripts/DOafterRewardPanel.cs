using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//5月19日更新

public class DOafterRewardPanel : MonoBehaviour
{
    [SerializeField] private Text rewardText;
    [SerializeField] private GameObject RewardPanel;
    [SerializeField] private GameObject RewardButton;
    [SerializeField] private GameObject RewardCoinImage;
    [SerializeField] private GameObject RewardflashImage;
    [SerializeField] private Text coinAddText;
    [SerializeField] private GameObject coinGenerator;//CoinPrefabを生成する場所
    [SerializeField] private GameObject SpinnerPanel;
    [SerializeField] private GameObject RegradePanel;
    [SerializeField] GameObject AdMobManager;
    [SerializeField] private GameObject rewardButton;//リワード広告を見るボタン
    //GachaSceneではhomeButton,Renshuu,TikaraではretopButton
    [SerializeField] private GameObject homeButton;//TopSceneに戻るボタン
    

    public void AfterReward(){
        rewardText.text = "";
        SpinnerPanel.SetActive(false);
        //SoundManager.instance.PlayPanelBGM("GradePanel");
        RewardButton.SetActive(false);
        RewardCoinImage.SetActive(false);
        RewardflashImage.SetActive(false);
        
        //Debug.Log("AfterReward,SpinPanel,"+SpinnerPanel.activeSelf);
        StartCoroutine(DoRewardPanel());
    }
    IEnumerator DoRewardPanel()
    { 
        yield return new WaitForSeconds(0.2f);
        DoRewardText();
        SoundManager.instance.PlaySousaSE(15);
        yield return new WaitForSeconds(0.8f);
    }
    public void DoRewardText(){
        rewardText.DOText("\nやったね!\nコインを150枚\nゲットしたよ"
        , 0.5f)
        .OnComplete(Coinhoka)
        ;
        print("rewardText");
    }
    public void Coinhoka(){
        StartCoroutine(CoinMove());
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.1f);
        RewardCoinImage.SetActive(true);
        RewardflashImage.SetActive(true);
        RewardflashImage.GetComponent<DOflash>().Flash18();
        
        //yield return new WaitForSeconds(1.2f);
        //RewardflashImage.SetActive(false);
       
       yield return new WaitForSeconds(0.2f);
       //coinGenerator.GetComponent<CoinGenerator>().SpawnRewardCoin();
       SoundManager.instance.PlaySousaSE(14);
       coinAddText.GetComponent<DOCounter>().CountCoin1();
       yield return new WaitForSeconds(1.6f);
       SoundManager.instance.StopSE();
       /*
       DOTween.TweensById("idFlash18").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });*/
       yield return new WaitForSeconds(0.1f);
       RewardflashImage.SetActive(false);
       SoundManager.instance.PlaySousaSE(8);
       RewardButton.SetActive(true);
       homeButton.SetActive(true);
       RewardButton.GetComponent<DOScale>().BigScale2();

    }
    public void CloseAdPanel(){
        /*
        if(RewardflashImage.activeSelf){
           
        DOTween.TweensById("idFlash18").ForEach((tween) =>
        {
            tween.Kill();
            //Debug.Log("IDKill");
            });}
        if(RewardButton.activeSelf){
        DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            //Debug.Log("IDBigScale2");
            })};*/
        
        rewardButton.SetActive(false);
        RewardPanel.SetActive(false);
        /*
        string SceneName =SceneManager.GetActiveScene().name;
        print("シーン名"+SceneName);
        Debug.Log("Return,"+SceneName);
        AdMobManager.GetComponent<AdMobInterstitial>().AdSceneName = SceneName;
        AdMobManager.GetComponent<AdMobBanner>().DestroyBannerAd();
        SceneManager.LoadScene(SceneName);*/
    }
    
}
