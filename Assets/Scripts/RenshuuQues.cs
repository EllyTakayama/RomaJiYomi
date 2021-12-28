using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//練習問題画面の出題メソッド
//12月22日更新

public class RenshuuQues : MonoBehaviour
{
    public static RenshuuQues instance;
    public string renshuuAnswer1;
    [HideInInspector] public string renshuuAnswer2;
    public RenshuuType renshuuType;
    private int locationOfRenshuuAnswer;
    public GameObject[] RenAnsButtons;
    public string tagOfButton;
    public Text RenQuesText;
    public int RenshuuCount;
    public GameObject RenshuuPanel;
    //public bool isHiragana;
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
        "ぴゃ","ぴゅ","ぴょ"
　　　　　　};

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
         "WA","WO","NN",

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
        "SHA","SHU","SHO",
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
        "JYA","JYU","JYO",
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

        renshuuNum = new List<int>(ToggleRenshuu.instance.shutsudaiNum);
        for(int i =0; i< renshuuNum.Count; i++){
            Debug.Log("r"+renshuuNum[i]);}
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
       if(n+1 > renshuuNum.Count){
            Debug.Log("リセット");
            n = 0;
        }
        RenshuuCount++;
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

        renshuuAnswer1 = RomaJiR50[b];
        RenQuesText.text = hiraganaR50[b];
        Debug.Log("b"+b);
        //StartCoroutine("PlayHiragana");
        SoundManager.instance.PlaySE(b);
        StartCoroutine("PlayDore");

        n++;
        locationOfRenshuuAnswer = UnityEngine.Random.Range(0,3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfRenshuuAnswer == 0)
       {
        RenAnsButtons[0].GetComponentInChildren<Text>().text = renshuuAnswer1; 
        RenAnsButtons[1].GetComponentInChildren<Text>().text = RomaJiR50[a];
        RenAnsButtons[2].GetComponentInChildren<Text>().text = RomaJiR50[c];
        }
        else if(locationOfRenshuuAnswer ==1)
        {
        RenAnsButtons[1].GetComponentInChildren<Text>().text = renshuuAnswer1;
        RenAnsButtons[2].GetComponentInChildren<Text>().text = RomaJiR50[a];
        RenAnsButtons[0].GetComponentInChildren<Text>().text = RomaJiR50[c];
    
        }
        else if(locationOfRenshuuAnswer ==2)
        {
        RenAnsButtons[2].GetComponentInChildren<Text>().text = renshuuAnswer1;
        RenAnsButtons[1].GetComponentInChildren<Text>().text = RomaJiR50[a];
        RenAnsButtons[0].GetComponentInChildren<Text>().text = RomaJiR50[c];

        }
    }

    IEnumerator PlayHiragana()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.PlaySE(b);
        Debug.Log("b"+b);
        StartCoroutine("PlayDore");
    }
    IEnumerator PlayDore()
    {//1秒停止
        yield return new WaitForSeconds(0.6f);
        SoundManager.instance.PlaySEDore();//どれ
    }



}
