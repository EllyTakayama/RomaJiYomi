using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

//6月29日更新

public class TopLogin : MonoBehaviour
{
    private enum LOGIN_TYPE
    {
        FIRST_USER_LOGIN, //初回ログイン
        TODAY_LOGIN,      //ログイン
        ALREADY_LOGIN,    //ログイン済
        ERROR_LOGIN       //不正ログイン
    }
    public GameObject loginBonusPanel;
    public Text coinText;//coinAddImageのtext
    private int todayDate = 0;
    private int lastDate;
    private LOGIN_TYPE judge_type;

    // Start is called before the first frame update
    void Start()
    {
        DateTime now = DateTime.Now;//端末の現在時刻の取得        
        todayDate = now.Year * 10000 + now.Month * 100 + now.Day;
        //日付を数値化　2020年9月1日だと20200901になる
        
        lastDate = ES3.Load<int>("lastDate","lastDate.es3",0 );

        //前回と今回の日付データ比較
        
        if (lastDate < todayDate)//日付が進んでいる場合
        {
            judge_type = LOGIN_TYPE.TODAY_LOGIN;
        }        
        else if (lastDate == todayDate)//日付が進んでいない場合
        {
            judge_type = LOGIN_TYPE.ALREADY_LOGIN;
        }
        else if (lastDate > todayDate)//日付が逆転している場合
        {
            judge_type = LOGIN_TYPE.ERROR_LOGIN;
        }
        switch (judge_type)
        {
            //ログインボーナス
            case LOGIN_TYPE.TODAY_LOGIN:
            if (lastDate == 0)
                {
                    print("初回ログイン");
                }else{
                    GameManager.instance.LoadCoinGoukei();
                    GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
                    GameManager.instance.totalCoin += 50;
                    GameManager.instance.SaveCoinGoukei();
                    loginBonusPanel.SetActive(true);
                    coinText.GetComponent<DOCounter>().CountCoin1();
                    print("今日のログボ"+todayDate);
                }
            
                break;

            //すでにログイン済み
            case LOGIN_TYPE.ALREADY_LOGIN:
                //なにもしない
                break;

            //不正ログイン
            case LOGIN_TYPE.ERROR_LOGIN:
                //不正ログイン時の処理
                break;
        }


        ES3.Save<int>("lastDate",todayDate,"lastDate.es3" );
        Debug.Log("lastDate"+lastDate);  
    }
}
