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
        AdMobManager.GetComponent<AdMobInterstitial>().name = "Home";
        string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        int RewardCount =IScount%6;
        Debug.Log("RewardCount,"+RewardCount);
        if(IScount>0 && IScount%6 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
    }

    public void KihonSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("KihonScene");
        AdMobManager.GetComponent<AdMobInterstitial>().name = "KihonScene";
        string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        int RewardCount =IScount%6;
        Debug.Log("RewardCount,"+RewardCount);
        if(IScount>0 && IScount%6 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
            }
        SoundManager.instance.PlaySousaSE(2); 
        SceneManager.LoadScene("KihonScene");
            }
    public void RenshuuSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("RenshuuScene");
        AdMobManager.GetComponent<AdMobInterstitial>().name = "RenshuuScene";
        string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        int RewardCount =IScount%6;
        Debug.Log("RewardCount,"+RewardCount);
        if(IScount>0 && IScount%6 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
            }
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
        
    }
    public void TikaraSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayPanelBGM("SelectPanel");
        AdMobManager.GetComponent<AdMobInterstitial>().name = "TikaraScene";
        string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        int RewardCount =IScount%6;
        Debug.Log("RewardCount,"+RewardCount);
        if(IScount>0 && IScount%6 ==0){
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
        AdMobManager.GetComponent<AdMobInterstitial>().name = "GachaScene";
        string name =AdMobManager.GetComponent<AdMobInterstitial>().name;
        print("name,"+name);
        GameManager.instance.SceneCount++;
        
        GameManager.instance.SaveSceneCount();
        int IScount = GameManager.instance.SceneCount;
        Debug.Log("SceneCount,"+GameManager.instance.SceneCount);
        int RewardCount =IScount%6;
        Debug.Log("RewardCount,"+RewardCount);
        if(IScount>0 && IScount%6 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("GachaScene");
    }

    public void SettingSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
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
    public void TopSceneMoveTitle(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("TopScene");
        //GameManager.instance.SceneCount++;
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
    }
    public void KihonSceneMoveTitle(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("KihonScene");
        //GameManager.instance.SceneCount++;
        SoundManager.instance.PlaySousaSE(2); 
        SceneManager.LoadScene("KihonScene");
            }
    public void RenshuuSceneMoveTitle(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayBGM("RenshuuScene");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
        
    }
    public void TikaraSceneMoveTitle(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlayPanelBGM("SelectPanel");
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TikaraScene");
       
    }
}
