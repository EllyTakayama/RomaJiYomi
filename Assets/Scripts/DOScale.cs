using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//5月19日更新

public class DOScale : MonoBehaviour
{
    private Vector3 defaultScale;
    void Start(){
        
    }
   public void BigScale2(){
       
       transform.localScale = new Vector3(1f, 1f, 1f);
       Debug.Log("transform.localScale"+transform.localScale);
       transform.DOPunchScale(Vector3.one * 0.1f, 2f, 1, 0.5f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetId("idBigScale2");
        Debug.Log("BigScale2");
   }
   public void BigScale3(){
       transform.localScale = new Vector3(1f, 1f, 1f);
       transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 2f, 1, 0.6f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetId("idBigScale3");
   }
   public void BallonScale(){
       transform.DOScale(new Vector3(1.25f, 1.25f, 1f), 0.1f);
   }
   
}
