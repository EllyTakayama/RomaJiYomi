using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//1月8日更新

public class DOaText : MonoBehaviour
{
    public Text aText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Atext(){
        aText.DOText("\nローマ字は\nアルファベットの組み合わせで表現します。"
        +"\nまず、基本のあ行を覚えましょう。"
        , 5f);
        print("aText");
    }


}
