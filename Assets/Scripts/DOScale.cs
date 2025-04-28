using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//5月19日更新

public class DOScale : MonoBehaviour
{
   public void BigScale2(){
       // transform に関係するTweenを安全に全部Kill（存在しない場合もOKでクラッシュしません）
       DOTween.Kill(transform);
       // スケールをリセット
       transform.localScale = Vector3.one;
       
       //transform.localScale = new Vector3(1f, 1f, 1f);
       Debug.Log("transform.localScale"+transform.localScale);
       transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 2f, 1, 0.5f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetLink(gameObject)
        ;
  
   }
   public void BigScale3(){
       // transform に関係するTweenを安全に全部Kill（存在しない場合もOKでクラッシュしません）
       DOTween.Kill(transform);
       transform.localScale = new Vector3(1f, 1f, 1f);
       transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 2f, 1, 0.6f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetLink(gameObject)
        ;
   }
   public void BallonScale(){
       transform.DOScale(new Vector3(1.25f, 1.25f, 1f), 0.1f)
       .SetLink(gameObject);
   }
   
}
