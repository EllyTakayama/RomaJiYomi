using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RouletteMaker : MonoBehaviour
{
    [SerializeField] private Transform imageParentTransform;//gameobject Rouletteのことです
    [SerializeField] private GameObject Roulette;//gameobject Rouletteのことです
    [SerializeField] private GameObject RDestroy;//ルーレットDestroy用GameObject
    public List<string> choices = new List<string>();
    //[SerializeField] private List<Color> rouletteColors;
    [SerializeField] private Image rouletteImage;//ルーレットプレハブ1（toggle1,2で10分割で生成される）
    [SerializeField] private Image rouletteImage1;//ルーレットプレハブ2（toggle3で12分割で生成される）
    [SerializeField] private RouletteController rController;
    [SerializeField] private Button[] hiraganaButtons;
    [SerializeField] private string[] sHiragana = new string[]{"a","k","s","t","n","h","m","y","r","w"};
    [SerializeField] private string[] tHiragana = new string[]{"A","K","S","T","N","H","M","Y","R","W"} ;
    [SerializeField] private string[] sHiragana5 = new string[]{"g","z","d","b","p","v","k","s","t","h"};
    [SerializeField] private string[] tHiragana5 = new string[]{"G","Z","D","B","P","V","K","S","T","H"} ;
    [SerializeField] private string[] sKHiragana51 = new string[]{"ky","sy","ty","ny","hy","my","ry","gy","zy","dy","by","py"} ;
    [SerializeField] private string[] tKHiragana51 = new string[]{"KY","SY","TY","NY","HY","MY","RY","GY","ZY","DY","BY","PY"} ;
    [SerializeField] private string[] HebonHiragana51 = new string[]{"KY","SH","CH","NY","HY","MY","RY","GY","J","DY","BY","PY"} ;
    [SerializeField] private string[] sHebonHiragana51 = new string[]{"ky","sh","ch","ny","hy","my","ry","gy","j","dy","by","py"} ;
    [SerializeField] private Toggle[] RouletteToggle;
    //public Image obj;
    public List<GameObject> prefabs = new List<GameObject>();//削除するルーレットクローンを代入するための配列
    private int RTnum;//Rouletteを作成するためのToggleで操作する変数

    //public int RcurrentMode;//currentModeをルーレットの設定に反映
    
    void Start() {
         RTnum = 1;
    }

    public void RMaker(){
        imageParentTransform.eulerAngles = new Vector3(0, 0, 0);
        GameManager.instance.LoadGfontsize();
        //Debug.Log("大文字"+GameManager.instance.isGfontsize);
            choices.Clear();
        if(RTnum == 1){
            if(GameManager.instance.isGfontsize == true){
                 choices.AddRange(tHiragana);
            }else{
                choices.AddRange(sHiragana);
            }
        }else if(RTnum == 2) {
            if(GameManager.instance.isGfontsize == true){
                 choices.AddRange(tHiragana5);
            }else{
                choices.AddRange(sHiragana5);
            }

        }else if(RTnum == 3) {
            if(GameManager.instance.isGfontsize == true){
                if(GameManager.instance.isGKunrei == false){
                     choices.AddRange(HebonHiragana51);
                     }
                else{
                     choices.AddRange(tKHiragana51);
                     }
                
            }else{
                if(GameManager.instance.isGKunrei == false){
                     choices.AddRange(sHebonHiragana51);
                     }
            else{
                     choices.AddRange(sKHiragana51);
                }
               }

        }
        for (int i=0; i<hiraganaButtons.Length; i++){
                 hiraganaButtons[i].gameObject.SetActive(false);
                 }
        for (int i=0;i< choices.Count; i++){
          Debug.Log("choices"+choices[i]);}
        float ratePerRoulette = 1 / (float) choices.Count;
        //Debug.Log("ratePerRoulette"+ratePerRoulette);
        float rotatePerRoulette = 360 / (float) (choices.Count);
        //Debug.Log("rotatePerRoulette"+rotatePerRoulette);
        for (int i = 0; i < choices.Count; i++) {
            if(RTnum == 1){
                var obj = Instantiate (rouletteImage, imageParentTransform);
                 //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                //Debug.Log("比率:ratePerRoulette1,"+ratePerRoulette);
                //Debug.Log("角度:rotatePerRoulette1,"+rotatePerRoulette);
                //Debug.Log("textの要素数:choices1,"+choices.Count);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
                //Debug.Log("child:rotation1,"+obj.transform.GetChild (0).transform.rotation);
                //Debug.Log("text1,"+obj.GetComponentInChildren<Text> ().text);
                 }
            else if(RTnum ==2){
                var obj = Instantiate (rouletteImage, imageParentTransform);
                //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                //Debug.Log("比率:ratePerRoulette2,"+ratePerRoulette);
                //Debug.Log("角度:rotatePerRoulette2,"+rotatePerRoulette);
                //Debug.Log("textの要素数:choices2,"+choices.Count);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
                //Debug.Log("child:rotation2,"+obj.transform.GetChild (0).transform.rotation);
                //Debug.Log("text2,"+obj.GetComponentInChildren<Text> ().text);
            }
            else {
                var obj = Instantiate (rouletteImage1, imageParentTransform);
                //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                //Debug.Log("比率:ratePerRoulette3,"+ratePerRoulette);
                //Debug.Log("角度:rotatePerRoulette3,"+rotatePerRoulette);
                //Debug.Log("textの要素数:choices3,"+choices.Count);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
                //Debug.Log("child:rotation3,"+obj.transform.GetChild (0).transform.rotation);
                //Debug.Log("text3,"+obj.GetComponentInChildren<Text> ().text);
            }
        }
        rController.SetRoulette();
        rController.rMaker = this;
        rController.rotatePerRoulette = rotatePerRoulette;
        rController.roulette = imageParentTransform.gameObject;
    }

    public void RchoiceToggle(){
        if(RouletteToggle[0].isOn ==true){
            RTnum = 1;
           Rdestroy[] tmpArray = Roulette.GetComponentsInChildren<Rdestroy>();
            for (int i=0; i< tmpArray.Length; i++)
            {
                tmpArray[i].RoletteDestroy();
            }
            //Debug.Log("tmpArray1"+tmpArray);
            RMaker(); 
        }
        else if(RouletteToggle[1].isOn ==true){
            RTnum = 2;
            Rdestroy[] tmpArray = Roulette.GetComponentsInChildren<Rdestroy>();
            for (int i=0; i< tmpArray.Length; i++)
            {
                tmpArray[i].RoletteDestroy();
            }
            //Debug.Log("tmpArra2"+tmpArray);
            RMaker();
        }
        else if(RouletteToggle[2].isOn ==true){
            RTnum = 3;
            Rdestroy[] tmpArray = Roulette.GetComponentsInChildren<Rdestroy>();
            for (int i=0; i< tmpArray.Length; i++)
            {
                tmpArray[i].RoletteDestroy();
            }
            //Debug.Log("tmpArra3"+tmpArray);
            SoundManager.instance.StopSE();
            RMaker();
        }
    }
    public void ToggleClick(){
        SoundManager.instance.PlaySousaSE(6);
    }
}
