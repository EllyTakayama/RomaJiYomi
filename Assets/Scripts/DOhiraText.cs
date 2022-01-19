using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
//1月18日更新

public class DOhiraText : MonoBehaviour
{
     public Text hiraganaText; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HiraganaText(){
        hiraganaText.DOText("\nひらがな50音の"+
　　　　　　　　"\nアルファベットの組み合わせをおぼえましょう。"
        , 3f)
        //.OnComplete(MoveButtons)
        ;
        Invoke("LateSE",1.1f);
        print("aText");
    }
}
