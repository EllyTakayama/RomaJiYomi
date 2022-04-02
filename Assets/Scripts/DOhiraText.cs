using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
//1月18日更新

public class DOhiraText : MonoBehaviour
{
    public Text hiraganaText; 
    //public bool isHTall;
    public Toggle hPanelToggle;
    public GameObject hyouImage;
    
    
    public void HiraganaText(){
        //isHTall = GameManager.instance.isGfontsize;
        if(GameManager.instance.isGfontsize ==true){
            hiraganaText.DOText("ローマ字はアルファベットの子音と母音（あ行）で表現します。", 3f)
            .OnComplete(HiraganaHyo);
        }
        else{
            hiraganaText.DOText("ローマ字はアルファベットの子音と母音（あ行）で表現します。"
        ,3f)
        .OnComplete(HiraganaHyo);
        }
        
        Invoke("LatehSE",0.5f);
        print("hText");
    }
    public void LatehSE(){
        SoundManager.instance.PlayAgSE(6);}
    
    public void HiraganaHyo(){
        hyouImage.SetActive(true);
    }
    
    public void HiraganaRoulette(){
        hPanelToggle.isOn = true;
    }
}
