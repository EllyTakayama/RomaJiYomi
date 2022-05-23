using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//5月23日更新

public class DOShake1 : MonoBehaviour
{
    /*
    1トゥイーン時間 2振動する強さ 3	振動数 
    4手ブレ値 5	スナップフラグ 
    6フェードアウト
    */
    public void ShakeBall(){
        DOTween.Sequence()
        .Append(transform.DOShakeRotation(0.5f,new Vector3(0,0,60f),8,90,true))
        .Append(transform.DOShakeRotation(0.5f,new Vector3(0,0,-60f),8,90,true))
       ;
   }
}
