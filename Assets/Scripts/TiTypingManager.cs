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
    public bool isKantan;//かんたんかButton6こまで難しい問題Button9こ
    public bool isTallFont;//大文字か小文字かを取得
    //public List<string> TyShutudai = new List<string>();
    public string answerMoji;//Buttonのテキストを一時的に取得する
    public string QuestionAnswer;//正解の文字取得
    public int k=3;//配列でanswerを移動させるため
    public int answerNum=0;//answerを移動させるため


    //テキストデータを格納
    public string[,] TSTable;
    public string[,] TikaraTempt;
    private int tateNumber; // 行 縦
    private int yokoNumber; // 列　横
    public string[] Tromelines;//テキストアセット取得に使う
    public int qCount;//出題のインデックス数を取得して管理

    //テキストデータを読み込む
    [SerializeField] TextAsset Tyfood;
    //[SerializeField] TextAsset _question;
    //private DictionaryChange cd;

    //テキストアセットの何行の問題かの変数
    public int _qNum;

    //あるべき回数正解したら問題を変えるテキストアセットから取得
    public int _aNum;

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
        Output();
        Debug.Log("osita");
    }
    
   //出題する
    void Output(){
       answerNum=0;
       k=3;
        //TyShutudai.Clear();
        _qNum = Random.Range(0,tateNumber);//2次元配列の行の選択
        _aNum = int.Parse(TSTable[_qNum,2]);

        kanjiText.text = TSTable[_qNum,0];
        qText.text = TSTable[_qNum,1];
        aText.text = "";
        //Debug.Log("_aNum"+_aNum);
        Debug.Log("qNum"+_qNum);
        int j =3;
        for(int i =0;i<yokoNumber-3;i++){
                 TiButtons[i].GetComponentInChildren<Text>().text = TSTable[_qNum,j];
                 j++;}
        QuestionAnswer = TSTable[_qNum,k];
        
    }


   /* public void TiCheckAnswer(int num){
       if(TiButtons[num].GetComponentInChildren<Text>().text) 
    }*/


    public void PressButton(int Bnum){
        answerMoji = TiButtons[Bnum].GetComponentInChildren<Text>().text;
        Debug.Log("seikai"+answerMoji);
        Debug.Log("k"+k);
        Debug.Log("aNum"+_aNum);
        Debug.Log("正解数"+answerNum);
        if(QuestionAnswer == answerMoji){
            Correct();
        }
        else{
            Miss();
        }
        if(answerNum>=_aNum){
            Output();
            Debug.Log("output");
        }
    }

    //正解した時の関数
    void Correct(){
        answerNum++;
        //_aNum++;
        k++;
        QuestionAnswer = TSTable[_qNum,k];
        aText.text += answerMoji;
        Debug.Log("correct");
        Debug.Log("aNum"+_aNum);
        Debug.Log("k"+k);
        Debug.Log("answerNum"+answerNum);
        

    }
    //間違えた時の関数
    void Miss(){
        Debug.Log("miss");
    }




    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.LoadGfontsize();
        isTallFont = GameManager.instance.isGfontsize;
        //Output();
        //Debug.Log("_aString"+_aString[_aNum].ToString());
        //print("answer"+_aString);

        SetListTi();
        DebugTable();
        

    }
     

    void SetListTi(){
        //押したButtonに応じて分岐
        //textAsset の取得　改行で分ける
        Tromelines = Tyfood.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
    
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


}
