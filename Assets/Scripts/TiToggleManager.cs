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
    public bool isKotoba;//boolで切り替えし、TikaraQuesのブールに代入

    // Start is called before the first frame update
    void Start(){
        TiTogLoad();
        //Debug.Log("単語isTango"+Toggle1.isOn);
        //Debug.Log("1文字isTango"+Toggle2.isOn);
    }
    public void TiSelectToggle(){
        if(Toggle1.isOn == true){
            //単語単位での選択
            isTango = true;
            TikaraQues.instance.isWord = true;
            ES3.Save<bool>("isTango",isTango,"isTango.es3");
            //Debug.Log("クリック単語isTango"+isTango);
            Debug.Log("単語isTango"+Toggle1.isOn);
            Debug.Log("1文字isTango"+Toggle2.isOn);
        }
        else{
            isTango = false;
            TikaraQues.instance.isWord = true;
            ES3.Save<bool>("isTango",isTango,"isTango.es3");
             Debug.Log("単語isTango"+Toggle1.isOn);
             Debug.Log("1文字isTango"+Toggle2.isOn);
        }
        
    }
    public void TiTogLoad(){
        isTango = ES3.Load<bool>("isTango","isTango.es3",true);
        if(isTango==true){
            Toggle1.isOn = true;
            Toggle2.isOn = false;
        }
        else{
            Toggle1.isOn = false;
            Toggle2.isOn = true;
        }
        
        Debug.Log("ロード単語Toggle"+Toggle1.isOn);
        Debug.Log("ロード文字Toggle"+Toggle2.isOn);
    }



}
