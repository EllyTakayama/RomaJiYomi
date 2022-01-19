using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMaker : MonoBehaviour
{
    [SerializeField] private Transform imageParentTransform;
    public List<string> choices = new List<string>();
    [SerializeField] private List<Color> rouletteColors;
    [SerializeField] private Image rouletteImage;
    [SerializeField] private RouletteController rController;
    [SerializeField] private Button[] hiraganaButtons;
    [SerializeField] private string[] sHiragana = new string[]{"k","s","t","n","h","m","y","r","w"};
    [SerializeField] private string[] tHiragana = new string[]{"K","S","T","N","H","M","Y","R","W"} ;
    public bool isRouletteTall;//大文字かどうか
    
    void Start () {
        GameManager.instance.LoadGfontsize();
        isRouletteTall = GameManager.instance.isGfontsize;
        Debug.Log("大文字"+isRouletteTall);
        if(isRouletteTall == true){
            choices.Clear();
            choices.AddRange(tHiragana);
            hiraganaButtons[0].GetComponentInChildren<Text> ().text ="A";
            hiraganaButtons[1].GetComponentInChildren<Text> ().text ="I";
            hiraganaButtons[2].GetComponentInChildren<Text> ().text ="U";
            hiraganaButtons[3].GetComponentInChildren<Text> ().text ="E";
            hiraganaButtons[4].GetComponentInChildren<Text> ().text ="O";
        }else{
            choices.Clear();
            choices.AddRange(sHiragana);
            hiraganaButtons[0].GetComponentInChildren<Text> ().text ="a";
            hiraganaButtons[1].GetComponentInChildren<Text> ().text ="i";
            hiraganaButtons[2].GetComponentInChildren<Text> ().text ="u";
            hiraganaButtons[3].GetComponentInChildren<Text> ().text ="e";
            hiraganaButtons[4].GetComponentInChildren<Text> ().text ="o";
        }
        for (int i=0;i< choices.Count; i++){
          Debug.Log("choices"+choices[i]);}
        float ratePerRoulette = 1 / (float) choices.Count;
        float rotatePerRoulette = 360 / (float) (choices.Count);
        for (int i = 0; i < choices.Count; i++) {
            var obj = Instantiate (rouletteImage, imageParentTransform);
            obj.color = rouletteColors[(choices.Count - 1 - i)];
            obj.fillAmount = ratePerRoulette * (choices.Count - i);
            obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
            obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
        }
        rController.SetRoulette();
        rController.rMaker = this;
        rController.rotatePerRoulette = rotatePerRoulette;
        rController.roulette = imageParentTransform.gameObject;
    }
}
