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
    
    //各Sceneへ移動する際に2回に一度インタースティシャル広告を呼び出し
    public void TopSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("TopScene");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
        }
    }
    public void KihonSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("KihonScene");
        SoundManager.instance.PlaySousaSE(2); 
        SceneManager.LoadScene("KihonScene");
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        } 
    }
    public void RenshuuSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("RenshuuScene");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        
    }
    public void TikaraSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayPanelBGM("SelectPanel");
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TikaraScene");
       
    }
    public void GachaSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("GachaScene");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("GachaScene");
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        
    }

    public void SettingSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        int IScount = GameManager.instance.SceneCount;
        if(IScount>0 && IScount%2 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("SettingScene");
    }
    
    public void TopSettingMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        
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
