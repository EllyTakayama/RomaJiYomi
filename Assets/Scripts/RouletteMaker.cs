using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMaker : MonoBehaviour
{
    [SerializeField] private Transform imageParentTransform;//gameobject Rouletteのことです
    [SerializeField] private GameObject Roulette;//gameobject Rouletteのことです
    public List<string> choices = new List<string>();
    //[SerializeField] private List<Color> rouletteColors;
    [SerializeField] private Image rouletteImage;
    [SerializeField] private Image rouletteImage1;
    [SerializeField] private RouletteController rController;
    [SerializeField] private Button[] hiraganaButtons;
    [SerializeField] private string[] sHiragana = new string[]{"a","k","s","t","n","h","m","y","r","w"};
    [SerializeField] private string[] tHiragana = new string[]{"A","K","S","T","N","H","M","Y","R","W"} ;
    [SerializeField] private string[] sHiragana5 = new string[]{"g","z","d","b","p","v","k","s","t","h"};
    [SerializeField] private string[] tHiragana5 = new string[]{"G","Z","D","B","P","V","K","S","T","H"} ;
    [SerializeField] private string[] sKHiragana51 = new string[]{"ky","sy","ty","ny","hy","my","ry","gy","jy","dy","by","py"} ;
    [SerializeField] private string[] tKHiragana51 = new string[]{"KY","SY","TY","NY","HY","MY","RY","GY","JY","DY","BY","PY"} ;
    [SerializeField] private string[] HebonHiragana51 = new string[]{"KY","SH","CH","NY","HY","MY","RY","GY","J","DY","BY","PY"} ;
    [SerializeField] private Toggle[] RouletteToggle;
    public Image obj;
    private int RTnum;//Rouletteを作成するためのToggleで操作する変数

    //public int RcurrentMode;//currentModeをルーレットの設定に反映
    
    void Start() {
         RTnum = 1;
    }

    public void RMaker(){
        GameManager.instance.LoadGfontsize();
        Debug.Log("大文字"+GameManager.instance.isGfontsize);
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
                 choices.AddRange(tKHiragana51);
            }else{
                choices.AddRange(sKHiragana51);
            }

        }
        for (int i=0; i<hiraganaButtons.Length; i++){
                 hiraganaButtons[i].gameObject.SetActive(false);
                 }
        for (int i=0;i< choices.Count; i++){
          Debug.Log("choices"+choices[i]);}
        float ratePerRoulette = 1 / (float) choices.Count;
        float rotatePerRoulette = 360 / (float) (choices.Count);
        for (int i = 0; i < choices.Count; i++) {
            if(RTnum == 1){
                obj = Instantiate (rouletteImage, imageParentTransform);
                 //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
                 }
            else if(RTnum ==2){
                obj = Instantiate (rouletteImage, imageParentTransform);
                //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
            }
            else {
                obj = Instantiate (rouletteImage1, imageParentTransform);
                //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
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
            Roulette.GetComponentInChildren<Rdestroy>().RoletteDestroy();
            RMaker(); 
        }
        else if(RouletteToggle[1].isOn ==true){
            RTnum = 2;
            Roulette.GetComponentInChildren<Rdestroy>().RoletteDestroy();
            RMaker();
        }
        else if(RouletteToggle[2].isOn ==true){
            RTnum = 3;
            Roulette.GetComponentInChildren<Rdestroy>().RoletteDestroy();
            RMaker();
        }
    }
}
