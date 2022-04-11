using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BDest : MonoBehaviour
{
    public bool isAHigh;
    public Image[] bPrefabs;//あ行はabImage
    public Image[] bPrefabs1;
    private int b;
    public List<int> HRoulletteNum = new List<int>();//ルーレットの風船の変数の保持
    private HiraDictionary cd;
    string[] Agyou = new string[]{
         "A",
         "I",
         "U",
         "E",
         "O"
     };
    //小文字
     string[] aGyou = new string[]{
         "a",
         "i",
         "u",
         "e",
         "o"
     };

    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<HiraDictionary>();
      //isAHigh = GameManager.instance.isGfontsize;

        //3秒後に削除
        b = 0;
        Destroy(gameObject, 4.0f);
        //print("baloon");
    }
    
    public void BonClick(int num){
        //あ行の分岐
        if(QuesManager.instance.currentMode ==2){
            SoundManager.instance.PlaySE(num);
        //あ行で大文字だった場合の分岐
         if (GameManager.instance.isGfontsize == true){
            if(b == 0){
                 bPrefabs[num].GetComponentInChildren<Text>().text = Agyou[num];
                 b++;
                 }
            else if(b >0){
                bPrefabs1[num].GetComponentInChildren<Text>().text = hiragana50[num];
                b = 0;
            }
           
            }
        else{
             bPrefabs[num].GetComponentInChildren<Text>().text = aGyou[num];
             }
        }
         
    }
    public void ABClick(int num){
         //あ行の分岐
        if(QuesManager.instance.currentMode ==2){
            SoundManager.instance.PlaySE(num);
            //あ行で大文字だった場合の分岐
            if (GameManager.instance.isGfontsize == true){
                if(b == 0){
                     bPrefabs1[num].GetComponentInChildren<Text>().text = Agyou[num];
                     b++;}
                else if(b > 0){
                    bPrefabs1[num].GetComponentInChildren<Text>().text = hiragana50[num];
                    b = 0;
                    }
                }
             //あ行で小文字だった場合の分岐
            else{
                if(b==0){
                     bPrefabs1[num].GetComponentInChildren<Text>().text = aGyou[num];
                     b++;}
                else if(b >0){
                    bPrefabs1[num].GetComponentInChildren<Text>().text = hiragana50[num];
                    b = 0;
                }    
             }
             //ルーレットシーンのバルーンで文字を取得する時の分岐
        }
        else{
            HRoulletteNum = new List<int>(GameManager.instance.RoulletteNum);
           SoundManager.instance.PlaySE(HRoulletteNum[num]);
           if(b == 0){
               bPrefabs1[num].GetComponentInChildren<Text>().text = hiragana50[HRoulletteNum[num]];
               b++;}
            else if(b>0){
                if(GameManager.instance.isGfontsize==true){
                //大文字でヘボン式の分岐
                if(GameManager.instance.isGKunrei == false){
                    string c = RomaJi50[HRoulletteNum[num]];
                    if(cd.dicHebon.ContainsKey(c)){
                        c = cd.dicHebon[c];
                        bPrefabs1[num].GetComponentInChildren<Text>().text = c;
                        }
                    }else{//大文字で訓令式の分岐
                        bPrefabs1[num].GetComponentInChildren<Text>().text = RomaJi50[HRoulletteNum[num]];
                        }
                b=0;}
                else{//小文字の場合の分岐
                    //小文字でヘボン式の場合の分岐
                    if(GameManager.instance.isGKunrei == false){
                    string c = RomaJi50[HRoulletteNum[num]].ToLower();
                    if(cd.dicHebon.ContainsKey(c)){
                        c = cd.dicHebon[c];
                        bPrefabs1[num].GetComponentInChildren<Text>().text = c;
                        }
                    }else{//小文字で訓令式の分岐
                        bPrefabs1[num].GetComponentInChildren<Text>().text = RomaJi50[HRoulletteNum[num]].ToLower();
                        }
                    b=0;
                }
            }
            
            Debug.Log("hiragana");
            }
    }
        


    
    string[] hiragana50 = new string[]{
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
        "ぴゃ","ぴゅ","ぴょ",
       //107-111
        "ヴァ","ヴィ","ヴ","ヴェ","ヴォ" };

    string[] RomaJi50 = new string[]{
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
        "PYA","PYU","PYO",
        //107-111
        "VA","VI","VU","VE","VO"
        };

}
