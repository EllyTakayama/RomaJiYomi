using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//1月4日更新


public class SettingManager : MonoBehaviour
{
    public Toggle tallToggle;//大文字の選択 /A
    public Toggle smallToggle;//小文字の選択 /a
    public Toggle hebonToggle;//ヘボン式の選択 /shi
    public Toggle kunreiToggle;//訓令式の選択 /si
    public Toggle bgmToggle;//BGMオンオフ
    public Toggle seToggle;//SEオンオフ
    public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御

    public bool isfontSize = true;// true なら大文字
    public bool isKunrei = true;// true なら訓令式書式
    public bool TestfontSize;//テスト用データ

    void Start()
    {
        FontTogLoad();
        ShosikiTogLoad();
        SetBGMLoad();
        SetSELoad();
        Debug.Log("スタートロード");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FontSelectToggle(){
        if(tallToggle.isOn == true){
            //大文字選択ならisfontSizeはtrue
            isfontSize = true;
            GameManager.instance.isGfontsize = isfontSize;
            ES3.Save<bool>("isfontSize", isfontSize);
            GameManager.instance.SaveGfontsize();
            Debug.Log("クリックisfontSize"+isfontSize);
        }
        else{
            //小文字が選択されているなら
            isfontSize = false;
            GameManager.instance.isGfontsize = isfontSize;
            ES3.Save<bool>("isfontSize", isfontSize);
            GameManager.instance.SaveGfontsize();
        }
        Debug.Log("クリックisfontSize"+isfontSize);
    }

    public void FontTogLoad(){
        ES3.Load<bool>("isfontSize", isfontSize);
         isfontSize = ES3.Load<bool>("isfontSize", isfontSize);
         GameManager.instance.isGfontsize = isfontSize;
         Debug.Log("isfontSize"+isfontSize);
        if(isfontSize ==true){
            tallToggle.isOn = true;
            smallToggle.isOn = false;
        }
        else{
            tallToggle.isOn = false;
            smallToggle.isOn = true;
        }
        Debug.Log("ロードtallToggle"+tallToggle.isOn);
        Debug.Log("ロードsmallToggle"+smallToggle.isOn);
    }

    public void ShosikiSelectToggle(){
        if(kunreiToggle.isOn == true){
            //訓令選択はtrue
                isKunrei = true;
            ES3.Save<bool>("isKunrei",isKunrei);
            //Debug.Log("クリックisKunrei"+isKunrei);
        }
        else{
            //ヘボン式が選択されているならfalse
            isKunrei = false;
            ES3.Save<bool>("isKunrei",isKunrei);
          
        }
          Debug.Log("クリックisKunrei"+isKunrei);
    }

    public void ShosikiTogLoad(){
        //if(ES3.KeyExists("isKunrei"))
        isKunrei = ES3.Load<bool>("isKunrei",true);
        Debug.Log("ロードisKunrei"+isKunrei);
        if(isKunrei ==true){
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
            ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
            //Debug.Log("BGM"+bgmToggle.isOn);
            } 
        else if(bgmToggle.isOn ==true){
        SoundManager.instance.UnmuteBGM();
        SoundManager.instance.PlayBGM("SettingScene");
        ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
        //Debug.Log("クリックBGM"+bgmToggle.isOn);
        }
        Debug.Log("クリックBGMtoggle"+bgmToggle.isOn);//toggleのboolの状態をisOnで取得
       
    }
    public void OnClickSEToggle(){
        if (seToggle.isOn ==false){
            SoundManager.instance.SEmute(); 
            ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
            Debug.Log("クリックSE"+seToggle.isOn);
            } 
        else{
            seToggle.isOn =true;
            SoundManager.instance.UnmuteSE();
            ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
             Debug.Log("SESave"+seToggle.isOn);
        }
        Debug.Log("SEtoggle"+seToggle.isOn);//toggleのboolの状態をisOnで取得
       
    }
      void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }
    public void SetBGMLoad(){
     bgmToggle.isOn = ES3.Load<bool>("BGM_OnOf",true);
      if (bgmToggle.isOn ==true){
             SoundManager.instance.UnmuteBGM();
             SoundManager.instance.PlayBGM("SettingScene");
             }
        else{
            SoundManager.instance.BGMmute();
        }
        Debug.Log("ロードBGM"+bgmToggle.isOn);
    }

    public void SetSELoad(){
      seToggle.isOn  = ES3.Load<bool>("SE_OnOf",true);
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



}