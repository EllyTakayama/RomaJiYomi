using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//5月19日更新

public class DOScale : MonoBehaviour
{
   public void BigScale2(){
       transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 2f, 1, 0.5f)
        .SetLoops(10, LoopType.Yoyo)
        .SetId("idBigScale2");
   }
   public void BigScale3(){
       transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 2f, 1, 0.6f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetId("idBigScale3");
   }
}
