using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//練習問題画面の出題メソッド
//4月16日更新バグ修正

public class RenshuuQues : MonoBehaviour
{
    public static RenshuuQues instance;
    public string renshuuAnswer1;
    [HideInInspector] public string Reselect1;//選択肢
    [HideInInspector] public string Reselect2;//選択肢
    //[HideInInspector] public string renshuuAnswer2;
    public RenshuuType renshuuType;
    private int locationOfRenshuuAnswer;
    public GameObject[] RenAnsButtons;
    public Button[] RenAnsButton;
    public string tagOfButton;
    public Text RenQuesText;
    public Text RenCountText;//左　出題数を表示
    public Text RenMondaiText;//右　問題数を表示
    public int RenshuuCount;//出題数をカウントする変数
    public int RenMondaisuu;//問題数の設定
    public int RenSeikai;//coin枚数を計算するために変数
    public GameObject RenshuuPanel;
    [SerializeField] private GameObject RegradePanel;
    private HiraDictionary rq;//RenshuuQues用のHiraDictionaryの取得
    public enum RenshuuType
    {
        RenRomaji50,
        RenHoka,
    }
    private int n;
    public int b;
    public int a;
    public int c;
    public List<int> renshuuNum = new List<int>();
    string[] hiraganaR50 = new string[]{
        //0-4
        "あ","い","う","え","お",
        //5-9
        "か","き","く","け","こ",
        //10-14
        "さ","し","す","せ","そ",
        //15-19
        "た","ち","つ","て","と",
        //20-24
        "な","に","ぬ","ね","の",
        //25-29
        "は","ひ","ふ","へ","ほ",
        //30-34
        "ま","み","む","め","も",
        //35-37
        "や","ゆ","よ",
        //38-42
        "ら","り","る","れ","ろ",
        //43-45
        "わ","を","ん",
        
        //46-50
        "が","ぎ","ぐ","げ","ご",
        //51-55
        "ざ","じ","ず","ぜ","ぞ",
        //56-60
        "だ","ぢ","づ","で","ど",
        //61-65
        "ば","び","ぶ","べ","ぼ",
        //66-70
        "ぱ","ぴ","ぷ","ぺ","ぽ",


        //71-73
        "きゃ","きゅ","きょ",
        //74-76
        "しゃ","しゅ","しょ",
        //77-79
        "ちゃ","ちゅ","ちょ",
        //80-82
        "にゃ","にゅ","にょ",
        //83-85
        "ひゃ","ひゅ","ひょ",
        //86-88
        "みゃ","みゅ","みょ",
        //89-91
        "りゃ","りゅ","りょ",
        //92-94
        "ぎゃ","ぎゅ","ぎょ",
        //95-97
        "じゃ","じゅ","じょ",
        //98-100
        "ぢゃ","ぢゅ","ぢょ",
        //101-103
        "びゃ","びゅ","びょ",
        //104-106
        "ぴゃ","ぴゅ","ぴょ"};
    
    string[] romajiR50 = new string[]{
        //0-4
        "a","i","u","e","o",
        //5-9
        "ka","ki","ku","ke","ko",
        //10-14
        "sa","si","su","se","so",
        //15-19
        "ta","ti","tu","te","to",
        //20-24
        "na","ni","nu","ne","no",
        //25-29
        "ha","hi","hu","he","ho",
        //30-34
        "ma","mi","mu","me","mo",
         //35-37
        "ya","yu","yo",
         //38-42
        "ra","ri","ru","re","ro",
        //43-45
        "wa","wo","n",

        //46-50
        "ga","gi","gu","ge","go",
       //51-55
        "za","zi","zu","ze","zo",
        //56-60
        "da","di","du","de","do",
        //61-65
        "ba","bi","bu","be","bo",
         //66-70
        "pa","pi","pu","pe","po",

        //71-73
        "kya","kyu","kyo",
        //74-76
        "sya","syu","syo",
        //77-79
        "tya","tyu","tyo",
        //80-82
        "nya","nyu","nyo",
        //83-85
        "hya","hyu","hyo",
        //86-88
        "mya","myu","myo",
        //89-91
        "rya","ryu","ryo",
        //92-94
        "gya","gyu","gyo",
        //95-97
        "zya","zyu","zyo",
        //98-100
        "dya","dyu","dyo",
        //101-103
        "bya","byu","byo",
        //104-106
        "pya","pyu","pyo"};
    

    string[] RomaJiR50 = new string[]{
         //0-4
        "A","I","U","E","O",
        //5-9
        "KA","KI","KU","KE","KO",
        //10-14
        "SA","SI","SU","SE","SO",
        //15-19
        "TA","TI","TU","TE","TO",
        //20-24
        "NA","NI","NU","NE","NO",
        //25-29
        "HA","HI","HU","HE","HO",
        //30-34
        "MA","MI","MU","ME","MO",
         //35-37
        "YA","YU","YO",
        //38-42
        "RA","RI","RU","RE","RO",
        //43-45
         "WA","WO","N",

        //46-50
        "GA","GI","GU","GE","GO",
       //51-55
        "ZA","ZI","ZU","ZE","ZO",
        //56-60
        "DA","DI","DU","DE","DO",
        //61-65
        "BA","BI","BU","BE","BO",
         //66-70
        "PA","PI","PU","PE","PO",

        //71-73
        "KYA","KYU","KYO",
        //74-76
        "SYA","SYU","SYO",
        //77-79
        "TYA","TYU","TYO",
        //80-82
        "NYA","NYU","NYO",
        //83-85
        "HYA","HYU","HYO",
        //86-88
        "MYA","MYU","MYO",
        //89-91
        "RYA","RYU","RYO",
        //92-94
        "GYA","GYU","GYO",
        //95-97
        "ZYA","ZYU","ZYO",
        //98-100
        "DYA","DYU","DYO",
        //101-103
        "BYA","BYU","BYO",
        //104-106
        "PYA","PYU","PYO"
        };

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
        //出題数ようのデバック記述
        RenMondaisuu = 5;
        RenSeikai = 0;
        GameManager.instance.RcorrectCount = 0;
        GameManager.instance.LoadGfontsize();
        Debug.Log("isGfontSize"+GameManager.instance.isGfontsize);
        GameManager.instance.LoadGKunrei();
        Debug.Log("isGfontSize"+GameManager.instance.isGKunrei);
        rq = GetComponent<HiraDictionary>();
        renshuuNum = new List<int>(ToggleRenshuu.instance.shutsudaiNum);
        for(int i =0; i< renshuuNum.Count; i++){
            Debug.Log("r"+renshuuNum[i]);}
        RenshuuCount=0;
        Renshuu();

    }
    public void Renshuu(){
        switch (renshuuType)
        {
            case (RenshuuType.RenRomaji50):
                RenRomaji50();
                break;
            /*
            case (RenshuuType.RenHoka):
                RenHoka();
                break;*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        tagOfButton = locationOfRenshuuAnswer.ToString();
        
    }
    
    public void RenRomaji50(){
        string Mondai = RenMondaisuu.ToString();
        RenMondaiText.text = "／"+Mondai+"問";
        RenshuuCount++;
       if(RenshuuCount > RenMondaisuu){
           RenSeikai = GameManager.instance.RcorrectCount;
           GameManager.instance.LoadCoinGoukei();
           GameManager.instance.RCoin = RenSeikai*1;
           GameManager.instance.totalCoin += GameManager.instance.RCoin;
           GameManager.instance.SaveCoinGoukei();
           RegradePanel.SetActive(true);
           RegradePanel.GetComponent<DoRegrade>().RgradePanel();
           Debug.Log("GameManager.totalCoin"+GameManager.instance.totalCoin);
           Debug.Log("GameManager.renshuuCoin"+GameManager.instance.RCoin);
            return;
        }

        RenAnsButton[0].enabled = true;
        RenAnsButton[1].enabled = true;
        RenAnsButton[2].enabled = true;
       if(n+1 > renshuuNum.Count){
            Debug.Log("リセット");
            n = 0;
        }
        
        RenCountText.text = RenshuuCount.ToString();
        Debug.Log("RenshuuCount"+RenshuuCount);
        b = renshuuNum[n];
        //3
        if(renshuuNum.Count == 3){
            if(n==0){
            a = renshuuNum[n+1];
            c = renshuuNum[n+2];
            }
            else if(n==1){
            a = renshuuNum[n-1];
            c = renshuuNum[n+1];
            }
            else if(n==2){
            a = renshuuNum[n-2];
            c = renshuuNum[n-1];
            }
        }else{
            
        if(n >2){
            a = renshuuNum[n-1];
            c = renshuuNum[n-2];
        }
        if(n <3) {
            a = renshuuNum[n+1];
            c = renshuuNum[n+2];
        }
        }

        RenQuesText.text = hiraganaR50[b];
            if(GameManager.instance.isGfontsize == true){
                renshuuAnswer1 = RomaJiR50[b];
                Reselect1 = RomaJiR50[a];
                Reselect2 = RomaJiR50[c];
                //大文字でヘボンの分岐
                if(GameManager.instance.isGKunrei == false){
                    string b = renshuuAnswer1;
                    string a = Reselect1;
                    string c = Reselect2;
                    if(rq.dicHebon.ContainsKey(b)){
                        b = rq.dicHebon[b];
                        renshuuAnswer1 = b;
                    }
                    if(rq.dicHebon.ContainsKey(a)){
                        a = rq.dicHebon[a];
                        Reselect1 = a;
                    }
                    if(rq.dicHebon.ContainsKey(c)){
                        c = rq.dicHebon[c];
                        Reselect2 = c;
                    }
                }//大文字でヘボンの分岐オワリ
            }
            else{//小文字の分岐
                renshuuAnswer1 = romajiR50[b]; 
                Reselect1 = romajiR50[a]; 
                Reselect2 = romajiR50[c];
                //小文字でヘボンの分岐 
                 if(GameManager.instance.isGKunrei == false){
                    string b = renshuuAnswer1;
                    string a = Reselect1;
                    string c = Reselect2;
                    if(rq.dicHebon.ContainsKey(b)){
                        b = rq.dicHebon[b];
                        renshuuAnswer1 = b;
                    }
                    if(rq.dicHebon.ContainsKey(a)){
                        a = rq.dicHebon[a];
                        Reselect1 = a;
                    }
                    if(rq.dicHebon.ContainsKey(c)){
                        c = rq.dicHebon[c];
                        Reselect2 = c;
                    }
                }//小文字でヘボンの分岐おわり
        }
        
         Debug.Log("GameManager.instance.isGfontsize"+GameManager.instance.isGfontsize);
        //Debug.Log("b"+b);
        StartCoroutine(PlayRenHiragana());
        n++;
        locationOfRenshuuAnswer = UnityEngine.Random.Range(0,3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfRenshuuAnswer == 0)
       {
        RenAnsButton[0].GetComponentInChildren<Text>().text = renshuuAnswer1; 
        RenAnsButton[1].GetComponentInChildren<Text>().text = Reselect1;
        RenAnsButton[2].GetComponentInChildren<Text>().text = Reselect2;
        }
        else if(locationOfRenshuuAnswer ==1)
        {
        RenAnsButton[1].GetComponentInChildren<Text>().text = renshuuAnswer1;
        RenAnsButton[2].GetComponentInChildren<Text>().text = Reselect1;
        RenAnsButton[0].GetComponentInChildren<Text>().text = Reselect2;
    
        }
        else if(locationOfRenshuuAnswer ==2)
        {
        RenAnsButton[2].GetComponentInChildren<Text>().text = renshuuAnswer1;
        RenAnsButton[1].GetComponentInChildren<Text>().text = Reselect1;
        RenAnsButton[0].GetComponentInChildren<Text>().text = Reselect2;

        }
    }
    public void StopRenYomiage(){
         StopCoroutine(PlayRenHiragana());
    }

    IEnumerator PlayRenHiragana()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.PlaySE(b);
        Debug.Log("b"+b);
        StartCoroutine(PlayDore());
    }
    IEnumerator PlayDore()
    {//1秒停止
        yield return new WaitForSeconds(0.6f);
        SoundManager.instance.PlaySEDore();//どれ
    }

}
