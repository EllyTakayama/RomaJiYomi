using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class TiTypingManager : MonoBehaviour
{
    [SerializeField]Text kanjiText;//出題の漢字表記用テキスト
    [SerializeField]Text qText;//出題内容
    [SerializeField]Text aText;//答え
    [SerializeField]Button[] TiButtons;//正誤判定Button
    public GameObject Shutudai2Panel;
    public int TicurrentMode;


    //問題を用意しておく
    private string[] _kanji = {"京都","北海道","東京"};
    private string[] _question = {"きょうと","ほっかいどう","とうきょう"};
    private string[] _answer1 = {"KYOTO","HOKKAIDO","TOKYO"};
    private string[] _answer;

    //テキストデータを格納
    public string[,] TSTable;
    public string[,] TikaraTempt;
    private int tateNumber; // 行 縦
    private int yokoNumber; // 列　横
    public string[] Tromelines;//テキストアセット取得に使う
    public int qCount;//出題のインデックス数を取得して管理

    //テキストデータを読み込む
    [SerializeField] TextAsset _Tfood;
    //[SerializeField] TextAsset _question;
    //private DictionaryChange cd;

    //テキストデータを格納するList
    private List<string> _kanjiList = new List<string>();
    private List<string> _questionList = new List<string>();
    private List<string> _answerList = new List<string>();


    //解答しているのが何番目の文字か指定するString
    private string _kString;
    private string _qString;
    public string _aString;

    //テキストアセットの何番目の問題かの変数
    public int _qNum=0;

    //出題の何番目の文字か
    public int _aNum=0;

public void TyKantan(string buttonname){
        
        switch (buttonname)
            {
                case "Button1":
                TicurrentMode = 1;
                SetListTi();
                Debug.Log("1");
                DebugTable();
                Shutudai2Panel.SetActive(true);
                TiKantan();
                    break;
                case "Button2":
                TicurrentMode = 2;
                SetListTi();
                Debug.Log("2");
                Shutudai2Panel.SetActive(true);
                TiKantan();
                    break;
                case "Button4":
                TicurrentMode = 4;
                SetListTi();
                Debug.Log("4");
                DebugTable();
                Shutudai2Panel.SetActive(true);
                TiKantan();
                    break;
           }
    }

    public void  TiKantan(){
        Debug.Log("osita");
    }
    
   //出題する
    void Output(){
        _aNum=0;
        _qNum = Random.Range(0,_question.Length);//2次元配列の行の選択

        _kString = _kanji[_qNum];
        _qString = _question[_qNum];
        _aString = _answer[_qNum];

        kanjiText.text = _kString;
        qText.text = _qString;
        aText.text = _aString;
        Debug.Log("_aNum"+_aNum);
        Debug.Log("qNum"+_qNum);
        Debug.Log("_aString"+_aString[_aNum].ToString());
    }

   /* public void TiCheckAnswer(int num){
       if(TiButtons[num].GetComponentInChildren<Text>().text) 
    }*/




    // Start is called before the first frame update
    void Start()
    {
        //Output();
        //Debug.Log("_aString"+_aString[_aNum].ToString());
        //print("answer"+_aString);
        //SetListTi();
        //DebugTable();

    }
     

    void SetListTi(){
        //押したButtonに応じて分岐
        //textAsset の取得　改行で分ける
        Tromelines = _Tfood.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
    
        // 行数と列数の取得
        yokoNumber = Tromelines[0].Split(',').Length;
        tateNumber = Tromelines.Length;//問題数
        //textAssetを二次元配列に代入
        TSTable = new string[tateNumber,yokoNumber];
        for (int i =0;i < tateNumber; i++){
            string[] tempt = Tromelines[i].Split(new[]{','});
            for(int j = 0; j < yokoNumber; j++)
            {
                TSTable[i, j] = tempt[j];
            }
        }
    }
    //Debugで二次元入れるの中身を確認したいとき用のメソッド
    void DebugTable()
    {
        for (int i = 0; i < tateNumber; i++)
        {
            for (int j = 0; j < yokoNumber; j++)
            {
               Debug.Log(i.ToString()+","+j.ToString()+TSTable[i, j]);
            }
        }
    }


    public void PressButton(int Bnum){
        Debug.Log("b"+Bnum);

    }
    //正解した時の関数
    void Correct(){
        _aNum++;
        Debug.Log("correct");
    }
    //間違えた時の関数
    void Miss(){
        Debug.Log("miss");
    }

}
