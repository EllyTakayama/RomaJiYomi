using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMaker : MonoBehaviour
{
    [SerializeField] private Transform imageParentTransform;
    public List<string> choices = new List<string>();
    //[SerializeField] private List<Color> rouletteColors;
    [SerializeField] private Image rouletteImage;
    [SerializeField] private Image rouletteImage1;
    [SerializeField] private RouletteController rController;
    [SerializeField] private Button[] hiraganaButtons;
    [SerializeField] private string[] sHiragana = new string[]{"a","k","s","t","n","h","m","y","r","w"};
    [SerializeField] private string[] tHiragana = new string[]{"A","K","S","T","N","H","M","Y","R","W"} ;
    [SerializeField] private string[] sHiragana5 = new string[]{"g","z","d","b","p","v"};
    [SerializeField] private string[] tHiragana5 = new string[]{"G","Z","D","B","P","V"} ;
    [SerializeField] private string[] sHiragana51 = new string[]{"ky\nsh","ty\nny","hy\nmy","ry\ngy","jy\ndy","by\npy"};
    [SerializeField] private string[] tHiragana51 = new string[]{"KY\nSH","TY\nNY","HY\nMY","RY\nGY","JY\nDY","BY\nPY"} ;

    //public int RcurrentMode;//currentModeをルーレットの設定に反映
    
    void Start() {
        /*
        GameManager.instance.LoadGfontsize();
        RcurrentMode = QuesManager.instance.currentMode;
        Debug.Log("大文字"+GameManager.instance.isGfontsize);
        Debug.Log("mode"+QuesManager.instance.currentMode);
            choices.Clear();
        if(QuesManager.instance.currentMode == 4){
            if(GameManager.instance.isGfontsize == true){
                 choices.AddRange(tHiragana);
                 Debug.Log(tHiragana.Length);
            }else{
                choices.AddRange(sHiragana);
                 Debug.Log(tHiragana.Length);
            }
        }else if(QuesManager.instance.currentMode == 5) {
            if(GameManager.instance.isGfontsize == true){
                 choices.AddRange(sHiragana5);
            }else{
                choices.AddRange(sHiragana5);
            }

        }
        if(QuesManager.instance.currentMode ==4){
             for (int i=0; i<hiraganaButtons.Length-1; i++){
                 hiraganaButtons[i].gameObject.SetActive(false);
                 }
        }else{
            for (int i=0; i<hiraganaButtons.Length; i++){
                 hiraganaButtons[i].gameObject.SetActive(false);
                 }
        }
        for (int i=0;i< choices.Count; i++){
          Debug.Log("choices"+choices[i]);}
        float ratePerRoulette = 1 / (float) choices.Count;
        float rotatePerRoulette = 360 / (float) (choices.Count);
        for (int i = 0; i < choices.Count; i++) {
            var obj = Instantiate (rouletteImage, imageParentTransform);
            //obj.color = rouletteColors[(choices.Count - 1 - i)];
            obj.fillAmount = ratePerRoulette * (choices.Count - i);
            obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
            obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
        }
        rController.SetRoulette();
        rController.rMaker = this;
        rController.rotatePerRoulette = rotatePerRoulette;
        rController.roulette = imageParentTransform.gameObject;*/
    }
    public void RMaker(){

        GameManager.instance.LoadGfontsize();
        //RcurrentMode = QuesManager.instance.currentMode;
        Debug.Log("大文字"+GameManager.instance.isGfontsize);
        Debug.Log("mode"+QuesManager.instance.currentMode);
            choices.Clear();
        if(QuesManager.instance.currentMode == 4){
            if(GameManager.instance.isGfontsize == true){
                 choices.AddRange(tHiragana);
            }else{
                choices.AddRange(sHiragana);
            }
        }else if(QuesManager.instance.currentMode == 5) {
            if(GameManager.instance.isGfontsize == true){
                 choices.AddRange(sHiragana5);
            }else{
                choices.AddRange(sHiragana5);
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
            if(QuesManager.instance.currentMode == 4){
                 var obj = Instantiate (rouletteImage, imageParentTransform);
                 //obj.color = rouletteColors[(choices.Count - 1 - i)];
                obj.fillAmount = ratePerRoulette * (choices.Count - i);
                obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
                obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
                 }
            else{
                var obj = Instantiate (rouletteImage1, imageParentTransform);
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
}
