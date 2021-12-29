using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    public Toggle tallToggle;//大文字の選択 /A
    public Toggle smalltoggle;//小文字の選択 /a
    public Toggle hebonToggle;//ヘボン式の選択 /shi
    public Toggle kunreitoggle;//訓令式の選択 /si
    public Toggle bgmToggle;//BGMオンオフ
    public Toggle seToggle;//SEオンオフ
    public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御



    public bool isfontSize = true;// true なら大文字

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //--シングルトン終わり--
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FontSelectToggle(){
        if(tallToggle.isOn == true){
            //大文字選択ならisfontSizeはtrue
            isfontSize = true;
            ES3.Save<bool>("isfontSize", isfontSize);
            Debug.Log("クリックisfontSize"+isfontSize);
        }
        else{
            //小文字が選択されているなら
            isfontSize = false;
            ES3.Save<bool>("isfontSize", isfontSize);
            //Debug.Log("クリックisfontSize"+isfontSize);
        }
    }

    public void FontTogLoad(){
        isfontSize = ES3.Load<bool>("isfontSize",true);
         //Debug.Log("クリックisfontSize"+isfontSize);
        if(isfontSize ==true){
            tallToggle.isOn = true;
            smalltoggle.isOn = false;
            //Debug.Log("ロードtallToggle"+tallToggle.isOn);
            //Debug.Log("ロードsmallToggle"+smalltoggle.isOn);
        }
        else{
            tallToggle.isOn = false;
            smalltoggle.isOn = true;
        }
    }

    public void OnClickBGMToggle(){
        if (canAnswer == false)
        {
            return;//canAnswerがfalseなら実行しない
        }
        canAnswer = false;//bool canAnswerがfalseの時繰り返し、DelayResetを0.01秒ごに実行
        Invoke("DelayReset", 1f);
        if (bgmToggle.isOn == false){
             SoundManager.instance.BGMmute();
            ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
            Debug.Log("BGMSave");
            } 
        else if(bgmToggle.isOn ==true){
        SoundManager.instance.UnmuteBGM();
        ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
        Debug.Log("BGMSave");
        }
     
        Debug.Log("BGMtoggle"+bgmToggle.isOn);//toggleのboolの状態をisOnで取得
       
    }
    public void OnClickSEToggle(){
        if (seToggle.isOn ==false){
            SoundManager.instance.SEmute(); 
            ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
            Debug.Log("SESave");
            } 
        else{
            seToggle.isOn =true;
            SoundManager.instance.UnmuteSE();
            ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
             Debug.Log("SESave");
        }
     
        Debug.Log("SEtoggle"+seToggle.isOn);//toggleのboolの状態をisOnで取得
       
    }
      void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }
    public void BGMLoad(){
     bgmToggle.isOn = ES3.Load<bool>("BGM_OnOf",true);
      if (bgmToggle.isOn ==true){
             SoundManager.instance.UnmuteBGM();
             }
        else{
            SoundManager.instance.BGMmute();
        }


    }

    public void SELoad(){
      seToggle.isOn  = ES3.Load<bool>("SE_OnOf",true);
      if (seToggle.isOn ==true){
             SoundManager.instance.UnmuteSE();
             }
        else{
            SoundManager.instance.SEmute();
        }
    }



}
