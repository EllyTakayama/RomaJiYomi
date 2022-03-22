using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//3月21日更新

public class ReSettingManager : MonoBehaviour
{
    public Toggle tallToggle;//大文字の選択 /A
    public Toggle smallToggle;//小文字の選択 /a
    public Toggle hebonToggle;//ヘボン式の選択 /shi
    public Toggle kunreiToggle;//訓令式の選択 /si
    public Toggle bgmToggle;//BGMオンオフ
    public Toggle seToggle;//SEオンオフ
    public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御

    public bool TestfontSize;//テスト用データ
    [SerializeField] private GameObject RenshuuPanel;

    void Start()
    {
        //GameManager.instance.LoadGfontsize();
        FontTogLoad();
        ShosikiTogLoad();
        SetBGMLoad();
        SetSELoad();
        Debug.Log("スタートロード");
    }

    public void FontSelectToggle(){
        if(tallToggle.isOn == true){

            GameManager.instance.isGfontsize=true;
            GameManager.instance.SaveGfontsize();
        }
        else{
            //小文字が選択されているなら
            GameManager.instance.isGfontsize= false;
            GameManager.instance.SaveGfontsize();
             Debug.Log("GameMfontSize"+GameManager.instance.isGfontsize);
        }
        Debug.Log("クリックGameMfontSize"+GameManager.instance.isGfontsize);
    }

    public void FontTogLoad(){
        GameManager.instance.LoadGfontsize();
        Debug.Log("RロードGameMfontSize"+GameManager.instance.isGfontsize);
         if(GameManager.instance.isGfontsize == true){
           tallToggle.isOn = true;
            smallToggle.isOn = false;
             }
         else{
            tallToggle.isOn = false;
            smallToggle.isOn = true;
                  }

        Debug.Log("ロードtallToggle"+tallToggle.isOn);
        Debug.Log("ロードsmallToggle"+smallToggle.isOn);
        Debug.Log("ロードGameMfontSize"+GameManager.instance.isGfontsize);
    }
    public void IsKunrei(){
         if(tallToggle.isOn == false){
             Debug.Log("tall");
         }else{
              Debug.Log("small");
         }
    }

    public void ShosikiSelectToggle(){
        if(kunreiToggle.isOn == true){
            //訓令選択はtrue
               
                GameManager.instance.isGKunrei = true;
                GameManager.instance.SaveGKunrei();
        }
        else{
            //ヘボン式が選択されているならfalse
            GameManager.instance.isGKunrei = false;
            GameManager.instance.SaveGKunrei();
            
        }
         Debug.Log("クリックkunreiToggle"+kunreiToggle.isOn);
        Debug.Log("クリックhebonToggle"+hebonToggle.isOn);
         Debug.Log("クリックGameManagerisGKunrei"+GameManager.instance.isGKunrei);

    }

    public void ShosikiTogLoad(){
        GameManager.instance.LoadGKunrei();
        Debug.Log("ロードGameKunrei"+GameManager.instance.isGKunrei);
        if(GameManager.instance.isGKunrei ==true){
            kunreiToggle.isOn = true;
            hebonToggle.isOn = false;
            
        }
        else{
            kunreiToggle.isOn = false;
            hebonToggle.isOn = true;
        }
        Debug.Log("ロードkunreiToggle"+kunreiToggle.isOn);
        Debug.Log("ロードhebonToggle"+hebonToggle.isOn);

    }

    public void OnClickBGMToggle(){
        if (bgmToggle.isOn == false){
            SoundManager.instance.BGMmute();
            GameManager.instance.isBgmOn = false;
            GameManager.instance.SaveGbgm();
            
            } 
        else if(bgmToggle.isOn ==true){
        SoundManager.instance.UnmuteBGM();
        SoundManager.instance.PlayBGM("SettingScene");
        GameManager.instance.isBgmOn = true;
        GameManager.instance.SaveGbgm();
        
        }
        Debug.Log("クリックBGMtoggle"+bgmToggle.isOn);//toggleのboolの状態をisOnで取得
      
    }
    public void OnClickSEToggle(){
        if (seToggle.isOn ==false){
            SoundManager.instance.SEmute(); 
            GameManager.instance.isSEOn = false;
            GameManager.instance.SaveGse();
           
            Debug.Log("クリックSE"+seToggle.isOn);
            Debug.Log("GisSEon"+GameManager.instance.isSEOn);
            } 
        else{
           
            SoundManager.instance.UnmuteSE();
            GameManager.instance.isSEOn = true;
            GameManager.instance.SaveGse();
           
             Debug.Log("SESave"+seToggle.isOn);
              Debug.Log("GisSEon"+GameManager.instance.isSEOn);
        }
        Debug.Log("SEtoggle"+seToggle.isOn);//toggleのboolの状態をisOnで取得
       
    }
      void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }
    public void SetBGMLoad(){
        GameManager.instance.LoadGbgm();
        if(GameManager.instance.isBgmOn == true){
            bgmToggle.isOn = true;
        }
        else{
            bgmToggle.isOn = false;
        }
     Debug.Log("GameKunrei"+GameManager.instance.isBgmOn);
      if (bgmToggle.isOn ==true){
             SoundManager.instance.UnmuteBGM();
             SoundManager.instance.PlayBGM("SettingScene");
             }
        else{
            SoundManager.instance.BGMmute();
        }
        Debug.Log("ロードBGM"+bgmToggle.isOn);
         Debug.Log("GameKunrei"+GameManager.instance.isBgmOn);
    }

    public void SetSELoad(){
         GameManager.instance.LoadGse();
         if(GameManager.instance.isSEOn == true){
            seToggle.isOn = true;
        }
        else{
            seToggle.isOn = false;
        }
         Debug.Log("GisSEOn"+GameManager.instance.isSEOn);
      if (seToggle.isOn ==true){
             SoundManager.instance.UnmuteSE();
             
             Debug.Log("SEunmute");
             }
        else{
            SoundManager.instance.SEmute();
            Debug.Log("SEmute");
        }
        Debug.Log("ロードSE"+seToggle.isOn);

    }

    public void ReSettingMove(){
        RenshuuPanel.SetActive(false);
    }
    public void RenshuuTopMove(){
        RenshuuPanel.SetActive(true);
    }

}
