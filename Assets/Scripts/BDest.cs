using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BDest : MonoBehaviour
{
    public bool isAHigh;
    public Image[] bPrefabs;//あ行はabImage
    public Image[] bPrefabs1;
    public List<int> HRoulletteNum = new List<int>();//ルーレットの風船の変数の保持
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
      //isAHigh = GameManager.instance.isGfontsize;

        //3秒後に削除
        Destroy(gameObject, 4.0f);
        //print("baloon");
    }
    
    public void BonClick(int num){
        if(QuesManager.instance.currentMode ==2){
            SoundManager.instance.PlaySE(num);
         if (GameManager.instance.isGfontsize == true){
            int b = 0;
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
        if(QuesManager.instance.currentMode ==2){
            SoundManager.instance.PlaySE(num);
         if (GameManager.instance.isGfontsize == true){
            bPrefabs1[num].GetComponentInChildren<Text>().text = Agyou[num];
            }
        else{
             bPrefabs1[num].GetComponentInChildren<Text>().text = aGyou[num];
             }
        }
        else if(QuesManager.instance.currentMode ==4){
            HRoulletteNum = new List<int>(GameManager.instance.RoulletteNum);
           SoundManager.instance.PlaySE(HRoulletteNum[num]);
            bPrefabs1[num].GetComponentInChildren<Text>().text = hiragana50[HRoulletteNum[num]];
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
        "ぴゃ","ぴゅ","ぴょ"};


}
