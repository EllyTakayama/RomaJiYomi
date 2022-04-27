using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//4月27日更新

public class ReMondaiToggle : MonoBehaviour
{
    public Toggle Toggle10;//10問
    public Toggle Toggle15;//15問
    public Toggle Toggle20;//20問
    public Toggle ToggleChoice;//入力した分の問題
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TiSelectToggle(){
        if(Toggle10.isOn == true){
           RenshuuQues.instance.RenMondaisuu = 10;
           ES3.Save<int>("RenMondaisuu",RenshuuQues.instance.RenMondaisuu,"RenMondaisuu.es3");
            //Debug.Log("クリック単語isTango"+isTango);
            Debug.Log("10"+Toggle10.isOn);
            Debug.Log("15"+Toggle15.isOn);
            Debug.Log("20"+Toggle20.isOn);
            Debug.Log("choice"+ToggleChoice.isOn);
        }
        else if(Toggle15.isOn == true){
           RenshuuQues.instance.RenMondaisuu = 15;
           ES3.Save<int>("RenMondaisuu",RenshuuQues.instance.RenMondaisuu,"RenMondaisuu.es3");
           Debug.Log("10"+Toggle10.isOn);
            Debug.Log("15"+Toggle15.isOn);
            Debug.Log("20"+Toggle20.isOn);
            Debug.Log("choice"+ToggleChoice.isOn);
        }
        else if(Toggle20.isOn == true){
            RenshuuQues.instance.RenMondaisuu = 20;
            ES3.Save<int>("RenMondaisuu",RenshuuQues.instance.RenMondaisuu,"RenMondaisuu.es3");
            Debug.Log("10"+Toggle10.isOn);
            Debug.Log("15"+Toggle15.isOn);
            Debug.Log("20"+Toggle20.isOn);
            Debug.Log("choice"+ToggleChoice.isOn);
        }
        else{
           
           ES3.Save<int>("RenMondaisuu",RenshuuQues.instance.RenMondaisuu,"RenMondaisuu.es3");
           Debug.Log("10"+Toggle10.isOn);
            Debug.Log("15"+Toggle15.isOn);
            Debug.Log("20"+Toggle20.isOn);
            Debug.Log("choice"+ToggleChoice.isOn);

        }
        
    }
    
}
