using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;//DoTweenを使用する記述


public class PanelManager : MonoBehaviour
{
    public GameObject Panel0;
    
    public void TopSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
    }
    public void KihonSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("KihonScene");
    }
    public void RenshuuSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
    }
    public void TikaraSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TikaraScene");
    }
    public void GachaSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("GachaScene");
    }

    public void SettingSceneMove(){
        SoundManager.instance.StopSE();
        DOTween.KillAll();
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
