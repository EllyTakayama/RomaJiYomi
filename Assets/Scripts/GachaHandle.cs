using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0223更新

public class GachaHandle : MonoBehaviour
{
    public void HandleRotate(){
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.0f,
        RotateMode.FastBeyond360)
        .SetDelay(0.1f);  
    }

}
