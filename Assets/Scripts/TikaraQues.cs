using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TikaraQues : MonoBehaviour
{
    public static TikaraQues instance;
    private int locationOfTikaraAnswer;
    public GameObject[] TikaraAnsButtons;
    [HideInInspector] public string TikaraAnswer;
    public string tagOfButton;
    public Text TikaraText;
    public int TikaraCount;
    public int TcurrentMode;
    public GameObject ShutudaiPanel;
    public GameObject Shutudai2Panel;
    public Toggle toggle1;//簡単・難しい分岐
    public bool Select;//デフォルトでは簡単がtrue
    private List<string> romeSlice = new List<string>();

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

    //テキストデータを格納
    public string[,] TSTable;
    public string[,] TikaraTempt;
    private int tateNumber; // 行 縦
    private int yokoNumber; // 列　横
    public string[] Tromelines;//テキストアセット取得に使う
    private DictionaryChange cd;

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
       cd = GetComponent<DictionaryChange>();
       ShutudaiPanel.SetActive(false);
       Shutudai2Panel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        tagOfButton = locationOfTikaraAnswer.ToString();
        
    }

    public void Kantan(string buttonname){
        if(toggle1.isOn == true){
        switch (buttonname)
            {
                case "Button1":
                TcurrentMode = 1;
                SetList();
                Debug.Log("1");
                ShutudaiPanel.SetActive(true);
                TKantan();
                    break;
                case "Button2":
                TcurrentMode = 2;
                SetList();
                Debug.Log("2");
                ShutudaiPanel.SetActive(true);
                TKantan();
                    break;
                case "Button4":
                TcurrentMode = 4;
                SetList();
                Debug.Log("4");
                ShutudaiPanel.SetActive(true);
                TKantan();
                    break;
           }
           }else{
            switch (buttonname)
            {
                case "Button1":
                Debug.Log("1");
                Shutudai2Panel.SetActive(true);
                    break;
            }
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


        public void  TKantan(){
       
        TikaraAnswer = TSTable[0,1];
        TikaraText.text = TSTable[0,0];
        n++;
        locationOfTikaraAnswer = UnityEngine.Random.Range(0,3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfTikaraAnswer == 0)
       {
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer; 
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[0,2];
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[0,3];
        }
        else if(locationOfTikaraAnswer ==1)
        {
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer;
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[0,4];
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[0,2];
    
        }
        else if(locationOfTikaraAnswer ==2)
        {
        TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer;
        TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[0,2];
        TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[0,3];

        }
    }

    
    void SetList(){
        //押したButtonに応じて分岐
        //textAsset の取得　改行で分ける
        if(TcurrentMode ==1){
        Tromelines = JinmeiRomajiT.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TcurrentMode ==2){
        Tromelines = TChimeiRomaji.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TcurrentMode ==4){
        Tromelines = Tentatei.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
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