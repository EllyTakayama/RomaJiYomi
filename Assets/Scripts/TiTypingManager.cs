using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class TiTypingManager : MonoBehaviour
{
    [SerializeField]Text kanjiText;//出題の漢字表記用テキスト
    [SerializeField]Text qText;//出題内容
    [SerializeField]Text aText;//解答内容を代入
    public Button[] TiButtons;//正誤判定Button
    public GameObject Shutudai2Panel;
    public int TicurrentMode;
    public bool isKantan;//かんたんかButton6こまで難しい問題Button9こ
    public bool isTallFont;//大文字か小文字かを取得
    //public List<string> TyShutudai = new List<string>();
    public string answerMoji;//Buttonのテキストを一時的に取得する
    public string QuestionAnswer;//正解の文字取得
    public int k=3;//配列でanswerを移動させるため
    public int answerNum=0;//answerを移動させるため
    public List<int> ButtonNum = new List<int>();//Buttonをシャッフルさせるため
    private int n;//シャッフル用の変数
    public string textcolor;
    public string pattern = "Ā|Ī|Ū|Ē|Ō|ā|ī|ū|ē|ō";


    //テキストデータを格納
    public string[,] TiTable;
    public string[,] TikaraTempt;
    private int TitateNumber; // 行 縦
    private int TiyokoNumber; // 列　横
    public string[] Tiromelines;//テキストアセット取得に使う
    public int qCount;//出題のインデックス数を取得して管理

    //テキストデータを読み込む
    [SerializeField] TextAsset Tyfood;
    [SerializeField] TextAsset Tyseikatu;
    [SerializeField] TextAsset Tydoubutu;
    [SerializeField] TextAsset Tytabemono;
    //private DictionaryChange cd;

    //テキストアセットの何行の問題かの変数
    public int _qNum;

    //あるべき回数正解したら問題を変えるテキストアセットから取得
    public int _aNum;
    //正解したQuesTextを赤く表示するために設定
    public int _mojiNum;

// Start is called before the first frame update
  void Start(){ 
      ShuffleB();
      Debug.Log("start");
       //cd = GetComponent<DictionaryChange>();
       //ShutudaiPanel.SetActive(false);
       GameManager.instance.LoadGfontsize();
       isTallFont = GameManager.instance.isGfontsize;
       Debug.Log("startfont"+isTallFont);
        //Output();
        //SetListTi();
        //DebugTable();
        
    }
    

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
                DebugTable();
                Shutudai2Panel.SetActive(true);
                TiKantan();
                    break;
                case "Button3":
                TicurrentMode = 3;
                SetListTi();
                Debug.Log("3");
                DebugTable();
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
       ShuffleB();
       answerNum=0;
       k=3;
       _mojiNum=0;
       //ShuffleB();
        //TyShutudai.Clear();
        _qNum = UnityEngine.Random.Range(0,TitateNumber);//2次元配列の行の選択
        //_qNum = 11;//Debug用
        _aNum = int.Parse(TiTable[_qNum,2]);

        if(isTallFont==true){
            kanjiText.text = TiTable[_qNum,0];
        qText.text = TiTable[_qNum,1];
        textcolor = TiTable[_qNum,1];
        aText.text = "";
        
        int j =3;
        for(int i =0;i<TiyokoNumber-3;i++){
                 TiButtons[ButtonNum[i]].GetComponentInChildren<Text>().text = TiTable[_qNum,j];
                 j++;}
        QuestionAnswer = TiTable[_qNum,k];
        //Debug.Log("_aNum"+_aNum);
        Debug.Log("qNum"+_qNum);
        }
        else{
            kanjiText.text = TiTable[_qNum,0].ToLower();
        qText.text = TiTable[_qNum,1].ToLower();
        textcolor = TiTable[_qNum,1].ToLower();
        aText.text = "";
        
        int j =3;
        for(int i =0;i<TiyokoNumber-3;i++){
                 TiButtons[ButtonNum[i]].GetComponentInChildren<Text>().text = TiTable[_qNum,j].ToLower();
                 j++;}
        QuestionAnswer = TiTable[_qNum,k].ToLower();
        //Debug.Log("_aNum"+_aNum);
        Debug.Log("qNum"+_qNum);

        }
    }


   /* public void TiCheckAnswer(int num){
       if(TiButtons[num].GetComponentInChildren<Text>().text) 
    }*/


    public void PressButton(int Bnum){
        answerMoji = TiButtons[Bnum].GetComponentInChildren<Text>().text;
        Debug.Log("seikai"+answerMoji);
        Debug.Log("k"+k);
        Debug.Log("aNum"+_aNum);
        if(QuestionAnswer == answerMoji){
            Correct();
        }
        else{
            Miss();
        }
        
    }

    //正解した時の関数
    void Correct(){

        if(QuestionAnswer.Length==1){
            _mojiNum += QuestionAnswer.Length;
        }
        else{
            if(Regex.IsMatch(QuestionAnswer, pattern)){
                _mojiNum += QuestionAnswer.Length;
                }
            else{
                _mojiNum += QuestionAnswer.Length-1;
                }
        }
        Debug.Log("moji"+_mojiNum);
        qText.text = "<color=#E72929>"+textcolor.Substring(0,_mojiNum)+"</color>"+textcolor.Substring(_mojiNum);
        answerNum++;
        if(answerNum>=_aNum){
            StartCoroutine(TiChangeQues());
            Debug.Log("output");
            return;
        }
        
        //_aNum++;
        k++;
        if(isTallFont==true){
            QuestionAnswer = TiTable[_qNum,k];
            }
        else{
             QuestionAnswer = TiTable[_qNum,k].ToLower();
            }
       
        
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
    IEnumerator TiChangeQues(){
        for(int i=0;i<TiButtons.Length;i++){
        TiButtons[i].enabled =false;
    }
        yield return new WaitForSeconds(0.5f);
        Output();
        for(int i=0;i<TiButtons.Length;i++){
        TiButtons[i].enabled =true;}
    }
   

    void SetListTi(){
        //押したButtonに応じて分岐 TicurrentMode
        
            if(TicurrentMode ==1){
            Tiromelines = Tyfood.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
            }
             else if(TicurrentMode ==2){
             Tiromelines = Tyseikatu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
            else if(TicurrentMode ==3){
             Tiromelines = Tydoubutu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TicurrentMode ==4){
             Tiromelines = Tytabemono.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        //textAsset の取得　改行で分ける
        // 行数と列数の取得
        TiyokoNumber = Tiromelines[0].Split(',').Length;
        TitateNumber = Tiromelines.Length;//問題数
        //textAssetを二次元配列に代入
        TiTable = new string[TitateNumber,TiyokoNumber];
        for (int i =0;i < TitateNumber; i++){
            string[] tempt = Tiromelines[i].Split(new[]{','});
            for(int j = 0; j < TiyokoNumber; j++)
            {
                TiTable[i, j] = tempt[j];
            }
        }
    }
    //Debugで二次元入れるの中身を確認したいとき用のメソッド
    void DebugTable()
    {
        for (int i = 0; i < TitateNumber; i++)
        {
            for (int j = 0; j < TiyokoNumber; j++)
            {
               Debug.Log(i.ToString()+","+j.ToString()+TiTable[i, j]);
            }
        }
    }
    void ShuffleB(){
        ButtonNum.Clear();
        for(int i =0;i < TiButtons.Length; i++){
            ButtonNum.Add(i);
        }
            int n = ButtonNum.Count;
        // nが1より小さくなるまで繰り返す
    while (n > 1)
    {
        n--;
        // nは 0 ～ n+1 の間のランダムな値
        int k = UnityEngine.Random.Range(0, n + 1);
 
        // k番目のカードをtempに代入
        int temp = ButtonNum[k];
        ButtonNum[k] = ButtonNum[n];
        ButtonNum[n] = temp;
        }
            for(int j=0;j<ButtonNum.Count;j++){
                Debug.Log("k"+ButtonNum[j]);
            }
        }
}
