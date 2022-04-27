using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
//4月28に日更新

public class TikaraQues : MonoBehaviour
{
    public static TikaraQues instance;
    private int locationOfTikaraAnswer;
    public Button[] TikaraAnsButtons;
    [HideInInspector] public string TikaraAnswer;
    public GameObject pipoEnemy;
    [SerializeField] private GameObject TiQuesManager;
    public string tagOfButton;
    public Text TikaraText;
    public Text furiganaText;
    public int TikaraCount;
    public int TcurrentMode;
    public GameObject TikaraPanel;
    public GameObject ShutudaiPanel;
    public GameObject Shutudai2Panel;
    public GameObject TigradePanel;
    public Toggle toggle1;//簡単・難しい分岐
    public bool Select;//デフォルトでは簡単がtrue
    private List<string> yomiageSlice = new List<string>();
    private int t;//シャッフル用の変数

    public int TiQuesCount;//出題数をカウントする
    public Text TiQuesText;//出題の進行数表示
    public Text TiQuesCountText;//問題数を表示
    public int TiMondaisuu;//単語解答の問題数の設定
    public int TiSeikai;//coin枚数を計算するために変数
    public bool isWord;//単語で解答するならtrue 1文字ずつ解凍するならfalse
    public Toggle[] TiMondaiToggle;//問題数を選択するToggle
    public int TiMondai;//トグルでの問題数保存
    public List<int> TikaQuesNum = new List<int>();//出題をシャッフルさせるため

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
    private List<string> shutudaiSlice = new List<string>();//読み上げ用の文字を格納する
    private HiraDictionary cd1;


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

        GameManager.instance.LoadGfontsize();
        GameManager.instance.LoadGKunrei();
        //出題数デバッグ用
        //TiMondaisuu = 5;
        TiQuesCount = 0;
        GameManager.instance.TiTangoCount=0;
        cd1 = GetComponent<HiraDictionary>();
        TiQuesManager.GetComponent<TspriteChange>().TiSChange();
        TiMondaiLoad();
        Debug.Log("TiMondai"+TiMondai);
        //Hebon(kunrei);
        //ShutudaiSlice(hoka);
        //isWord = true;
    }
    // Update is called once per frame
    void Update()
    {
        tagOfButton = locationOfTikaraAnswer.ToString();

    }
    public void TiSprite(){
        TiQuesManager.GetComponent<TspriteChange>().TiSChange();
    }

    public void Kantan(string buttonname)
    {

        switch (buttonname)
        {
            case "Button1":
                if (isWord == true)
                {
                    TcurrentMode = 1;
                    SetList();
                    ShuffleTikaQuesNum();
                    Debug.Log("1");
                    //DebugTable();
                    ShutudaiPanel.SetActive(true);
                    TKantan();
                }
                else
                {
                    Shutudai2Panel.SetActive(true);
                    TiTypingManager.instance.TicurrentMode = 1;
                    TiTypingManager.instance.SetListTi();
                    TiTypingManager.instance.ShuffleQuesNum();
                    TiTypingManager.instance.Output();
                }
                break;

            case "Button2":
                if (isWord == true)
                {
                    TcurrentMode = 2;
                    SetList();
                    ShuffleTikaQuesNum();
                    Debug.Log("2");
                    //DebugTable();
                    ShutudaiPanel.SetActive(true);
                    TKantan();
                }
                else
                {
                    Shutudai2Panel.SetActive(true);
                    TiTypingManager.instance.TicurrentMode = 2;
                    TiTypingManager.instance.SetListTi();
                    TiTypingManager.instance.ShuffleQuesNum();
                    TiTypingManager.instance.Output();
                }
                break;

            case "Button3":
                if (isWord == true)
                {
                    TcurrentMode = 3;
                    SetList();
                    ShuffleTikaQuesNum();
                    Debug.Log("3");
                    //DebugTable();
                    ShutudaiPanel.SetActive(true);
                    TKantan();
                }
                else
                {
                    Shutudai2Panel.SetActive(true);
                    TiTypingManager.instance.TicurrentMode = 3;
                    TiTypingManager.instance.SetListTi();
                    TiTypingManager.instance.ShuffleQuesNum();
                    TiTypingManager.instance.Output();
                }
                break;

            case "Button4":
                if (isWord == true)
                {
                    TcurrentMode = 4;
                    SetList();
                    ShuffleTikaQuesNum();
                    Debug.Log("4");
                    //DebugTable();
                    ShutudaiPanel.SetActive(true);
                    TKantan();
                }
                else
                {
                    Shutudai2Panel.SetActive(true);
                    TiTypingManager.instance.TicurrentMode = 4;
                    TiTypingManager.instance.SetListTi();
                    TiTypingManager.instance.ShuffleQuesNum();
                    TiTypingManager.instance.Output();
                }
                break;
        }



    }
    //string kunrei = "MO";
    //string hoka = "SHOUGUN";

    void ShutudaiSlice(string moji)
    {
        if (cd1.dicT.ContainsKey(moji))
        {
            Debug.Log("key");
        }
        else
        {
            Debug.Log("not key");
        }
        Debug.Log("a" + a);

    }

    //訓令式ローマ字がいるか確認する
    void Hebon(string kunrei)
    {
        if (cd1.dicHebon.ContainsKey(kunrei))
        {
            Debug.Log("key");
        }
        else
        {
            Debug.Log("not key");
        }
    }
    //訓令式ローマ字からヘボン式に変更する
    void ChangeKtoH(string moji)
    {
        Debug.Log("keyは存在します");
        string answer = cd1.dicHebon[moji];
        print(answer);
    }

    //ToLower() 小文字での表示
    public void TKantan()
    {
        pipoEnemy.SetActive(true);
        TiQuesCount++;
        string Mondai = TiMondaisuu.ToString();
        TiQuesText.text = "／"+Mondai+"問";

        if (TiQuesCount > TiMondaisuu)
        {
            TiSeikai = GameManager.instance.TiTangoCount;
            GameManager.instance.LoadCoinGoukei();
            GameManager.instance.TiCoin = TiSeikai * 1;
            GameManager.instance.totalCoin += GameManager.instance.TiCoin;
            GameManager.instance.SaveCoinGoukei();
            pipoEnemy.SetActive(false);
            TigradePanel.SetActive(true);
            TigradePanel.GetComponent<DoTigrade>().TgradePanel();
            Debug.Log("GameManager.totalCoin" + GameManager.instance.totalCoin);
            Debug.Log("GameManager" + GameManager.instance.TiCoin);
            return;
        }
        Debug.Log("問題数" + TiQuesCount);
        TiQuesCountText.text = TiQuesCount.ToString();
        TikaraAnsButtons[0].enabled = true;
        TikaraAnsButtons[1].enabled = true;
        TikaraAnsButtons[2].enabled = true;

        int n = TikaQuesNum[t];//出題される問題
        Debug.Log("n+" + n);
        //正解のstring
        if (GameManager.instance.isGKunrei == true)
        {
            TikaraAnswer = TSTable[n, 2];
        }
        else
        {
            TikaraAnswer = TSTable[n, 6];
        }

        //出題テキスト
        TikaraText.text = TSTable[n, 1];
        //漢字、ふりがな表示
        furiganaText.text = TSTable[n, 0];
        //n++;

        locationOfTikaraAnswer = UnityEngine.Random.Range(0, 3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
        if (GameManager.instance.isGfontsize == true)
        {
            if (locationOfTikaraAnswer == 0)
            {
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n, 3];
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n, 4];
            }
            else if (locationOfTikaraAnswer == 1)
            {
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n, 4];
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n, 5];

            }
            else if (locationOfTikaraAnswer == 2)
            {
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n, 5];
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n, 3];
            }
        }
        else
        {
            if (locationOfTikaraAnswer == 0)
            {
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n, 3].ToLower();
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n, 4].ToLower();
            }
            else if (locationOfTikaraAnswer == 1)
            {
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TSTable[n, 4].ToLower();
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n, 5].ToLower();

            }
            else if (locationOfTikaraAnswer == 2)
            {
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TSTable[n, 5].ToLower();
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TSTable[n, 3].ToLower();
            }

        }
        t++;
        if (t > TikaQuesNum.Count - 1)
        {
            t = 0;
            Debug.Log("リセット+" + t);
        }

    }



    void SetList()
    {
        //押したButtonに応じて分岐
        //textAsset の取得　改行で分ける
        if (TcurrentMode == 1)
        {
            Tromelines = Tfood.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 2)
        {
            Tromelines = Tseikatu.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 3)
        {
            Tromelines = Tdoubutu.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 4)
        {
            Tromelines = Ttabemono.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        // 行数と列数の取得
        yokoNumber = Tromelines[0].Split(',').Length;
        tateNumber = Tromelines.Length;
        //textAssetを二次元配列に代入
        TSTable = new string[tateNumber, yokoNumber];
        for (int i = 0; i < tateNumber; i++)
        {
            string[] tempt = Tromelines[i].Split(new[] { ',' });
            for (int j = 0; j < yokoNumber; j++)
            {
                TSTable[i, j] = tempt[j];
            }
        }
    }
     //出題数の設定
    public void ClickTiMondai(){
        if(TiMondaiToggle[0].isOn == true){
            TiMondai = 10;
            Debug.Log("TiMondai"+TiMondai);
            ES3.Save("TiMondai",TiMondai,"TiMondai.es3");
        }
        else if(TiMondaiToggle[1].isOn == true){
            TiMondai = 15;
            Debug.Log("TiMondai"+TiMondai);
            ES3.Save("TiMondai",TiMondai,"TiMondai.es3");
        }
        else if(TiMondaiToggle[2].isOn == true){
            TiMondai = 20;
            Debug.Log("TiMondai"+TiMondai);
             ES3.Save("TiMondai",TiMondai,"TiMondai.es3");
        }
        
    }
    public void TiMondaiLoad(){
        TiMondai = ES3.Load("TiMondai","TiMondai.es3",10);
        if(TiMondai == 10){
            TiMondaiToggle[0].isOn = true;
            TiMondaisuu = 10;
            TiTypingManager.instance.TyMondaisuu = 10;
            Debug.Log("TiMondai"+TiMondai);
            }
        else if(TiMondai==15){
            TiMondaiToggle[1].isOn = true;
            TiMondaisuu = 15;
            TiTypingManager.instance.TyMondaisuu = 15;
            Debug.Log("TiMondai"+TiMondai);
        }
        else if(TiMondai==20){
            TiMondaisuu = 20;
            TiTypingManager.instance.TyMondaisuu = 20;
            TiMondaiToggle[2].isOn = true;
            Debug.Log("TiMondai"+TiMondai);
        }
    }

    //出題をシャッフルする
    public void ShuffleTikaQuesNum()
    {
        TikaQuesNum.Clear();
        for (int i = 0; i < tateNumber; i++)
        {
            TikaQuesNum.Add(i);
        }
        int m = TikaQuesNum.Count;
        // nが1より小さくなるまで繰り返す
        while (m > 1)
        {
            m--;
            // nは 0 ～ n+1 の間のランダムな値
            int k = UnityEngine.Random.Range(0, m + 1);

            // k番目のカードをtempに代入
            int temp = TikaQuesNum[k];
            TikaQuesNum[k] = TikaQuesNum[m];
            TikaQuesNum[m] = temp;
        }
        for (int j = 0; j < TikaQuesNum.Count; j++)
        {
            Debug.Log("Q+" + TikaQuesNum[j]);
        }
        Debug.Log("問題数＋" + TikaQuesNum.Count);
    }



    //Debugで二次元入れるの中身を確認したいとき用のメソッド
    void DebugTable()
    {
        for (int i = 0; i < tateNumber; i++)
        {
            for (int j = 0; j < yokoNumber; j++)
            {
                Debug.Log(i.ToString() + "," + j.ToString() + TSTable[i, j]);
            }
        }
    }
    public void TiSettingPanel(){
        if(TikaraPanel.activeSelf ==true){
            TikaraPanel.SetActive(false);
            }
        else{
            TikaraPanel.SetActive(true);
        }
        
    }
}