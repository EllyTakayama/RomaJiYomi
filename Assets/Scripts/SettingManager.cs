using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//1月4日更新


public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject infoPanel;
    public Toggle tallToggle; //大文字の選択 /A
    public Toggle smallToggle; //小文字の選択 /a
    public Toggle hebonToggle; //ヘボン式の選択 /shi
    public Toggle kunreiToggle; //訓令式の選択 /si
    public Toggle bgmToggle; //BGMオンオフ
    public Toggle seToggle; //SEオンオフ
    public bool canAnswer; //Buttonの不具合を解消するため連続してボタンを押せなないよう制御

    public bool TestfontSize; //テスト用データ

    void Start()
    {
        //GameManager.instance.LoadGfontsize();
        FontTogLoad();
        ShosikiTogLoad();
        SetBGMLoad();
        SetSELoad();
        if (GameManager.instance.SceneCount == 5 || GameManager.instance.SceneCount == 30 ||
            GameManager.instance.SceneCount == 80 || GameManager.instance.SceneCount == 150)
        {
            GameManager.instance.RequestReview();
        }

    }

    //Debug.Log("スタートロード");
    public void SetPanelMove()
    {
        SoundManager.instance.StopSE();
        topPanel.SetActive(false);
        settingPanel.SetActive(true);
        SetSettingPanel();
        SoundManager.instance.PlaySousaSE(2);
    }

    public void TopPanelMove()
    {
        topPanel.SetActive(true);
        settingPanel.SetActive(false);
        infoPanel.SetActive(false);
        SoundManager.instance.PlaySousaSE(2);
    }

    public void InfoPanelMove()
    {
        SoundManager.instance.StopSE();
        topPanel.SetActive(false);
        settingPanel.SetActive(false);
        infoPanel.SetActive(true);
        SoundManager.instance.PlaySousaSE(2);
    }


    public void ReviewButton()
    {
        GameManager.instance.RequestReview();
    }

    //SettingPanelをアクティブにするときにトグルを適切に表示させる
    public void SetSettingPanel()
    {
        //大文字小文字トグルのセット
        if (GameManager.instance.isGfontsize == true)
        {
            tallToggle.isOn = true;
            smallToggle.isOn = false;
        }
        else
        {
            tallToggle.isOn = false;
            smallToggle.isOn = true;
        }

        Debug.Log("ロードtallToggle" + tallToggle.isOn);
        Debug.Log("ロードsmallToggle" + smallToggle.isOn);
        Debug.Log("GameMfontSize" + GameManager.instance.isGfontsize);

        //訓令化ヘボン式かトグルをセットする
        if (GameManager.instance.isGKunrei == true)
        {
            kunreiToggle.isOn = true;
            hebonToggle.isOn = false;

        }
        else
        {
            kunreiToggle.isOn = false;
            hebonToggle.isOn = true;
        }

        Debug.Log("ロードkunreiToggle" + kunreiToggle.isOn);
        Debug.Log("ロードhebonToggle" + hebonToggle.isOn);

        //BGMトグルをセットする
        if (GameManager.instance.isBgmOn == true)
        {

            bgmToggle.isOn = true;
        }
        else
        {
            bgmToggle.isOn = false;
        }

        Debug.Log("ロードBGM" + bgmToggle.isOn);
        Debug.Log("GameKunrei" + GameManager.instance.isBgmOn);

        //SEトグルをセットする
        if (GameManager.instance.isSEOn == true)
        {
            seToggle.isOn = true;
        }
        else
        {
            seToggle.isOn = false;
        }

        Debug.Log("GisSEOn" + GameManager.instance.isSEOn);
        Debug.Log("ロードSE" + seToggle.isOn);

    }

    //Onの時はOnのアイコン、Offの時はOffのアイコンが表示される
    public void FontSelectToggle()
    {
        if (tallToggle.isOn == true)
        {
            //大文字選択
            GameManager.instance.isGfontsize = true;

            GameManager.instance.SaveGfontsize();
            Debug.Log("GameMfontSize" + GameManager.instance.isGfontsize);
        }
        else
        {
            //小文字が選択されているなら
            GameManager.instance.isGfontsize = false;
            GameManager.instance.SaveGfontsize();

        }

        Debug.Log("GameMfontSize" + GameManager.instance.isGfontsize);
    }

    public void FontTogLoad()
    {
        GameManager.instance.LoadGfontsize();

    }

    public void ShosikiSelectToggle()
    {
        if (kunreiToggle.isOn == true)
        {
            //訓令選択はtrue
            GameManager.instance.isGKunrei = true;
            GameManager.instance.SaveGKunrei();

        }
        else
        {
            //ヘボン式が選択されているならfalse
            GameManager.instance.isGKunrei = false;
            GameManager.instance.SaveGKunrei();


        }

        Debug.Log("クリックisGKunrei" + GameManager.instance.isGKunrei);
    }

    public void ShosikiTogLoad()
    {
        GameManager.instance.LoadGKunrei();
    }

    public void OnClickBGMToggle()
    {
        if (bgmToggle.isOn == false)
        {
            SoundManager.instance.BGMmute();
            GameManager.instance.isBgmOn = false;
            GameManager.instance.SaveGbgm();
            //ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
            //Debug.Log("BGM"+bgmToggle.isOn);
        }
        else if (bgmToggle.isOn == true)
        {
            SoundManager.instance.UnmuteBGM();
            SoundManager.instance.PlayBGM("SettingScene");
            GameManager.instance.isBgmOn = true;
            GameManager.instance.SaveGbgm();
            //ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
            //Debug.Log("クリックBGM"+bgmToggle.isOn);
        }

        Debug.Log("クリックBGMtoggle" + bgmToggle.isOn); //toggleのboolの状態をisOnで取得

    }

    public void OnClickSEToggle()
    {
        if (seToggle.isOn == false)
        {
            SoundManager.instance.SEmute();
            GameManager.instance.isSEOn = false;
            GameManager.instance.SaveGse();
            //ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
            Debug.Log("クリックSE" + seToggle.isOn);
            Debug.Log("GisSEon" + GameManager.instance.isSEOn);
        }
        else
        {
            //seToggle.isOn =true;
            SoundManager.instance.UnmuteSE();
            GameManager.instance.isSEOn = true;
            GameManager.instance.SaveGse();
            //ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
            Debug.Log("SESave" + seToggle.isOn);
            Debug.Log("GisSEon" + GameManager.instance.isSEOn);
        }

        Debug.Log("SEtoggle" + seToggle.isOn); //toggleのboolの状態をisOnで取得

    }

    void DelayReset()
    {
        canAnswer = true; //問題に答えた時ボタン押せないブールをtrueに戻しておく
    }

    public void SetBGMLoad()
    {
        StartCoroutine(SetBGMLoadCoroutine());
        /*
        GameManager.instance.LoadGbgm();
        if (GameManager.instance.isBgmOn == true)
        {
            SoundManager.instance.UnmuteBGM();
            SoundManager.instance.PlayBGM("SettingScene");
        }
        else
        {
            SoundManager.instance.BGMmute();
        }

        Debug.Log("ロードBGM" + bgmToggle.isOn);
        Debug.Log("GameKunrei" + GameManager.instance.isBgmOn);*/
    }

    public void SetSELoad()
    {
        GameManager.instance.LoadGse();
        if (GameManager.instance.isSEOn == true)
        {
            SoundManager.instance.UnmuteSE();
        }
        else
        {
            SoundManager.instance.SEmute();
        }

        Debug.Log("GisSEOn" + GameManager.instance.isSEOn);
        Debug.Log("ロードSE" + seToggle.isOn);
    }

//コルーチンでアプリ起動時のBGMを再生することでアプリ起動時の音割れを防ぐ
    private IEnumerator SetBGMLoadCoroutine()
    {
        // SoundManager の初期化を待つ（IsInitialized が true になるまで）
        yield return new WaitUntil(() => SoundManager.instance.IsInitialized);

        yield return null; // 1フレーム待機
        GameManager.instance.LoadGbgm();

        if (GameManager.instance.isBgmOn == true)
        {
            SoundManager.instance.UnmuteBGM();
            SoundManager.instance.PlayBGM("SettingScene");
        }
        else
        {
            SoundManager.instance.BGMmute();
        }
    }
}