using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;//DoTweenを使用する記述


public class PanelManager : MonoBehaviour
{
    public GameObject Panel0;
    [SerializeField] GameObject AdMobManager;
    
    public void TopSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
             GameManager.instance.SceneCount=0;
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
        SoundManager.instance.PlaySousaSE(2);
       if(GameManager.instance.isBgmOn == true){
        SoundManager.instance.PlayBGM("TopScene");}
        SceneManager.LoadScene("TopScene");
    }
    public void KihonSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            GameManager.instance.SceneCount=0;
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
        SoundManager.instance.PlaySousaSE(2);
        if(GameManager.instance.isBgmOn == true){
        SoundManager.instance.PlayBGM("KihonScene");}
        SceneManager.LoadScene("KihonScene");
    }
    public void RenshuuSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            GameManager.instance.SceneCount=0;
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
        SoundManager.instance.PlaySousaSE(2);
        if(GameManager.instance.isBgmOn == true){
        SoundManager.instance.PlayBGM("RenshuuScene");}
        SceneManager.LoadScene("RenshuuScene");
    }
    public void TikaraSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            GameManager.instance.SceneCount=0;
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
        SoundManager.instance.PlaySousaSE(2);
        if(GameManager.instance.isBgmOn == true){
        SoundManager.instance.PlayBGM("TikaraScene");}
        SceneManager.LoadScene("TikaraScene");
    }
    public void GachaSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
        SoundManager.instance.PlaySousaSE(2);
        if(GameManager.instance.isBgmOn == true){
        SoundManager.instance.PlayBGM("GachaScene");}
        SceneManager.LoadScene("GachaScene");
    }

    public void SettingSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
        SceneManager.LoadScene("SettingScene");
    }
     //
    public void TopSettingMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
        Panel0.SetActive(false);
    }

    public void SetPanelMove(){
        SoundManager.instance.StopSE();
        Panel0.SetActive(false);
        
    }
    public void TopPanelMove(){
        Panel0.SetActive(true);
    }
}
