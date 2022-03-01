using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//3月1日更新

public class TiToggleManager : MonoBehaviour
{
    public Toggle Toggle1;//単語単位での解答true
    public Toggle Toggle2;//ひらがな1文字ずつでの解答true
    public bool isTango;//boolで切り替えし、TikaraQuesのブールに代入

    // Start is called before the first frame update
    void Start()
    {
        TiTogLoad();
        
    }
    public void TiSelectToggle(){
        if(Toggle1.isOn == true){
            //単語単位での選択
            isTango = true;
            TikaraQues.instance.isWord = isTango;
            ES3.Save<bool>("isTango",isTango,"isTango.es3");
        }
        else{
            //Toggle2 1文字ずつ入力
            isTango = false;
            TikaraQues.instance.isWord = isTango;
            ES3.Save<bool>("isTango", isTango,"isTango.es3");
            
        }
        Debug.Log("クリックisTango"+isTango);
    }
    public void TiTogLoad(){
        isTango = ES3.Load<bool>("isTango", isTango);
        TikaraQues.instance.isWord = isTango;
        Debug.Log("クリックisTango"+isTango);
        if(isTango ==true){
            Toggle1.isOn = true;
            Toggle2.isOn = false;
        }
        else{
            Toggle1.isOn = false;
            Toggle2.isOn = true;
        }
        Debug.Log("単語Toggle"+Toggle1.isOn);
        Debug.Log("1文字Toggle"+Toggle2.isOn);
    }



}
