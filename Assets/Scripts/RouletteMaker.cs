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
            choices.Clear();
            if(isRouletteTall == true){
                 choices.AddRange(tHiragana);
            }else{
                choices.AddRange(sHiragana);
            }
            for (int i=0; i<hiraganaButtons.Length; i++){
                 hiraganaButtons[i].gameObject.SetActive(false);
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
