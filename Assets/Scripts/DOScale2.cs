using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//5月19日更新

public class DOScale2 : MonoBehaviour
{
    /*
    1振動時の最大角度 2	トゥイーン時間 3振動数 4振動する範囲
    */
    public void OpenBall(){
       transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0), 1f, 10, 0.5f)
        ;
   }
}
