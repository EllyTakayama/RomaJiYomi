using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class TikaraQues : MonoBehaviour
{
    public static TikaraQues instance;
    private int locationOfTikaraAnswer;
    public GameObject[] TikaraAnsButtons;
    [HideInInspector] public string TikaraAnswer;
    public string tagOfButton;
    public Text TikaraText;
    public Text furiganaText;
    public int TikaraCount;
    public int TcurrentMode;
    public GameObject ShutudaiPanel;
    //public GameObject Shutudai2Panel;
    public Toggle toggle1;//簡単・難しい分岐
    public bool Select;//デフォルトでは簡単がtrue
    private List<string> romeSlice = new List<string>();
    public bool isFontTall;

    public enum TikaraType
    {
        Kantan,
        Mutukasi,
    }
    private int n;
    public int b;
    public int a;
    public int c;

    [SerializeField] TextAsset JinmeiRomajiT;
    [SerializeField] TextAsset TChimeiRomaji;
    [SerializeField] TextAsset Tentatei;
    [SerializeField] TextAsset Tfood;
    [SerializeField] TextAsset Tdoubutu;
    [SerializeField] TextAsset Ttabemono;
    [SerializeField] TextAsset Tseikatu;

    //テキストデータを格納
    public string[,] TSTable;
    public string[,] TikaraTempt;
    private int tateNumber; // 行 縦
    private int yokoNumber; // 列　横
    public string[] Tromelines;//テキストアセット取得に使う
    //private DictionaryChange cd;

    // Start is called before the first frame update
     void Awake()
    {
       MakeInstance();
    }

     void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       //cd = GetComponent<DictionaryChange>();
       //ShutudaiPanel.SetActive(false);
       GameManager.instance.LoadGfontsize();
       isFontTall = GameManager.instance.isGfontsize;
       
    }
    // Update is called once per frame
    void Update()
    {
        tagOfButton = locationOfTikaraAnswer.ToString();
        
    }

    public void Kantan(string buttonname){
        
        switch (buttonname)
            {
                case "Button1":
                TcurrentMode = 1;
                SetList();
                Debug.Log("1");
                DebugTable();
                ShutudaiPanel.SetActive(true);
                TKantan();
                    break;
                case "Button2":
                TcurrentMode = 2;
                SetList();
                Debug.Log("2");
                DebugTable();
                ShutudaiPanel.SetActive(true);
                TKantan();
                    break;
                case "Button3":
                TcurrentMode = 3;
                SetList();
                Debug.Log("3");
                DebugTable();
                ShutudaiPanel.SetActive(true);
                TKantan();
                    break;
                case "Button4":
                TcurrentMode = 4;
                SetList();
                Debug.Log("4");
                DebugTable();
                ShutudaiPanel.SetActive(true);

                TKantan();
                    break;
           }
          
        /*
        if(n+1 > TikaraTempt[0].Length){
            Debug.Log("5問目");
            n = 0;
            }
            RenshuuCount++;
            //Debug.Log("RenshuuCount"+RenshuuCount);
            b = renshuuNum[n];
            //4 
           if(n >2){
            a = renshuuNum[n-1];
            c = renshuuNum[n-2];
            }
            else if(n <3) {
            a = renshuuNum[n+1];
            c = renshuuNum[n+2];.Count){
            Debug.Log("5問目");
            n = 0;
            }
            RenshuuCount++;
            Debug.Log("RenshuuCount"+RenshuuCount);
            b = renshuuNum[n];
            //4 
           if(n >2){
            a = renshuuNum[n-1];
            c = renshuuNum[n-2];
            }
            else if(n <3) {
            a = renshuuNum[n+1];
            c = renshuuNum[n+2];*/
       
    }
    //ToLower() 小文字での表示
        public void  TKantan(){
        int n = Random.Range(0,tateNumber);
        //正解のstring
        TikaraAnswer = TSTable[n,2];
        //出題テキスト
        TikaraText.text = TSTable[n,1];
        //漢字、ふりがな表示
        furiganaText.text = TSTable[n,0];
        //n++;
        locationOfTikaraAnswer = UnityEngine.Random.Range(0,3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
        if(isFontTall==true){
        if(locationOfTikaraAnswer == 0)
       {
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer; 
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n,3];
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n,4];
        }
        else if(locationOfTikaraAnswer ==1)
        {
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer;
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n,4];
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n,5];
    
        }
        else if(locationOfTikaraAnswer ==2)
        {
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer;
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n,5];
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n,3];
        }
        }
        else{
            if(locationOfTikaraAnswer == 0)
       {
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower(); 
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n,3].ToLower();
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n,4].ToLower();
        }
        else if(locationOfTikaraAnswer ==1)
        {
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n,4].ToLower();
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n,5].ToLower();
    
        }
        else if(locationOfTikaraAnswer ==2)
        {
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n,5].ToLower();
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n,3].ToLower();
        }

        }
        
    }

    
    void SetList(){
        //押したButtonに応じて分岐
        //textAsset の取得　改行で分ける
        if(TcurrentMode ==1){
        Tromelines = Tfood.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TcurrentMode ==2){
        Tromelines = Tseikatu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TcurrentMode ==3){
        Tromelines = Tdoubutu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TcurrentMode ==4){
        Tromelines = Ttabemono.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        // 行数と列数の取得
        yokoNumber = Tromelines[0].Split(',').Length;
        tateNumber = Tromelines.Length;
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