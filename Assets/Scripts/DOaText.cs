using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//1月8日更新

public class DOaText : MonoBehaviour
{
    public Text aText; 
    [SerializeField] private GameObject ButtonPanel;

    public void Atext(){
        aText.DOText("\nローマ字は\nアルファベットの組み合わせで表現します。"
        +"\nまず、基本のあ行を覚えましょう。"
        , 5f)
        .OnComplete(MoveButtons);
        print("aText");
    }
    
    public void MoveButtons(){
        ButtonPanel.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(-950,0), 1.5f)
        .SetRelative(true)
    .SetEase(Ease.OutBack)
    ;
    }

    public void PlayAlfabetAa(){
        SoundManager.instance.PlaySE(0);
    }
    public void PlayAlfabetIi(){
        SoundManager.instance.PlaySE(1);
    }
    public void PlayAlfabetUu(){
        SoundManager.instance.PlaySE(2);
    }
    public void PlayAlfabetEe(){
        SoundManager.instance.PlaySE(3);
    }
    public void PlayAlfabetOo(){
        SoundManager.instance.PlaySE(4);
    }
}
